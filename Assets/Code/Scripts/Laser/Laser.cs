using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	#region Declarations public
	public float _rayDistance;
	public LayerMask _layer;
	#endregion

	#region Declarations private
	private LineRenderer _lineRenderer;
	private Ray2D _ray;
	private RaycastHit2D _hit;
	private Vector3[] _positions;
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

		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		_ray = new Ray2D(transform.position, transform.up/*(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*/);
		_positions = CastLaser(_ray, ref _hit, _rayDistance);
		_lineRenderer.positionCount = _positions.Length;
		_lineRenderer.SetPositions(_positions);
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
						_hit.collider.GetComponent<PlayerController>().ResetPlayer();
						break;

					case "Reflect":
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
						_hit.collider.GetComponent<ActionEnable>()._Action.Invoke();
						break;

					case "Burn":
						_hit.collider.GetComponent<BurnObject>().SetIsHit();
						break;

					default:
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
	#endregion

	#region Coroutine

	#endregion

}
