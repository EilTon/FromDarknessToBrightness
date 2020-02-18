using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	#region Declarations public
	public float _rayDistance;
	public LayerMask _layer;
	public bool _isContinue = true;
	public float _timeTocast = 2;
	public float _timeToHold = 100f;
	#endregion

	#region Declarations private
	private float _timer = 0;
	private float _storeCast;
	private LineRenderer _lineRenderer;
	private Ray2D _ray;
	private RaycastHit2D _hit;
	private Vector3[] _positions;
	private ActionEnable _holding;
	private BurnObject _burn;
	private float _timerHold = 0f;
	private MirrorParticle _mirror;
	private List<MirrorParticle> _mirrors;
	#endregion

	#region Declarations Event Args

	#endregion

	#region Declarations Event Handler

	#endregion

	#region Declarations Event Call

	#endregion

	#region Functions Unity
	private void Awake()
	{
		#region Initialize
		_lineRenderer = GetComponent<LineRenderer>();
		#endregion
	}

	private void Start()
	{
		#region Initialize
		_storeCast = _timeTocast;
		_mirrors = new List<MirrorParticle>();
		#endregion
	}


	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isContinue)
		{
			_ray = new Ray2D(transform.position, transform.up/*(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*/);
			_positions = CastLaser(_ray, ref _hit, _rayDistance);
			_lineRenderer.positionCount = _positions.Length;
			_lineRenderer.SetPositions(_positions);
		}
		else
		{

			if (_timer < _timeTocast)
			{
				_positions = null;
				_lineRenderer.enabled = false;
				_timer += Time.deltaTime;
			}
			else if (_timeTocast > 0)
			{
				_lineRenderer.enabled = true;
				_ray = new Ray2D(transform.position, transform.up/*(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*/);
				_positions = CastLaser(_ray, ref _hit, _rayDistance);
				_lineRenderer.positionCount = _positions.Length;
				_lineRenderer.SetPositions(_positions);
				_timeTocast -= Time.deltaTime;

			}
			else
			{
				_timer = 0;
				_timeTocast = _storeCast;
			}
		}
;
		if(_hit)
		{
			
			if (_mirrors.Count > 0)
			{
				foreach (var mirror in _mirrors)
				{
					mirror.SetIsHit(false);
				}
			}
		}
		


		#endregion

		#region Timer

		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}
	#endregion

	#region Helper
	Vector3[] CastLaser(Ray2D ray, ref RaycastHit2D _hit, float distance = 10)
	{
		List<Vector3> positions = new List<Vector3>();
		Vector3 lastPosition = ray.origin;
		Vector3 lastDirection = ray.direction;
		float laserLength = 0;
		positions.Add(lastPosition);

		while (positions.Count < 100)
		{
			_hit = Physics2D.Raycast(lastPosition, lastDirection, Mathf.Clamp(distance - laserLength, 0, distance), _layer);
			
			if (_hit)
			{
				positions.Add(_hit.point);
				switch (_hit.collider.tag)
				{
					case "Player":
						FindObjectOfType<PlayerController>().ResetPlayer("ray");
						break;

					case "Reflect":
						_mirror = _hit.collider.GetComponent<MirrorParticle>();
						if (_mirror != null)
						{
							_mirror.SetIsHit(true);
							_mirror.MaxIntensity();
							if(!_mirrors.Contains(_mirror))
							{
								_mirrors.Add(_mirror);
							}
						}
						laserLength += _hit.distance;
						lastPosition = _hit.point + _hit.normal * 0.01f;
						lastDirection = Vector2.Reflect(lastDirection, _hit.normal);
						break;

					case "ReflectPlayer":
						//FindObjectOfType<ShieldParticle>().SetEmissionParticle(true);
						laserLength += _hit.distance;
						lastPosition = _hit.point + _hit.normal * 0.01f;
						lastDirection = Vector2.Reflect(lastDirection, _hit.normal);
						break;

					case "Fear":
						Debug.Log("Test Fear");
						break;

					case "Enable":
						_hit.collider.GetComponent<Enable>()._Action.Invoke();
						break;

					case "ActionEnable":
						HoldCast(_hit);
						if (_timerHold > _timeToHold)
						{
							_holding = _hit.collider.GetComponent<ActionEnable>();
							_holding._Action.Invoke();
							_holding.SetIsStreching(true);
						}
						break;

					case "Burn":
						HoldCast(_hit);
						_burn = _hit.collider.GetComponent<BurnObject>();
						if (_timerHold < _timeToHold)
						{
							_burn.SetIsHit(true);
						}
						else if (_timerHold > _timeToHold)
						{
							_burn.SetIsBurn();
						}
						else
						{
							_burn.SetIsHit(false);
						}
						break;

					default:
						HoldCast(_hit);
						if (_holding != null)
						{
							_holding.SetIsStreching(false);
						}
						else if (_burn != null)
						{
							_burn.SetIsHit(false);
						}
						//FindObjectOfType<ShieldParticle>().SetIsHit(false);
						break;
				}
			}
			else
			{
				positions.Add(lastPosition + lastDirection * (distance - laserLength));
				break;
			}
		}
		return positions.ToArray();
	}

	void HoldCast(RaycastHit2D hit)
	{
		try
		{
			if (hit.collider.tag == "ActionEnable" || hit.collider.tag == "Burn")
			{
				_timerHold += Time.deltaTime;
			}
			else
			{
				_timerHold = 0f;
			}
		}

		catch(Exception ex)
		{

		}
		

	}
	#endregion

	#region Coroutine

	#endregion

}
