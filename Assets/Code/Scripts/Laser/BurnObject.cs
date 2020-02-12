using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BurnObject : MonoBehaviour
{
	#region Declarations public
	public float _timeToBurn;
	public Rigidbody2D _rigidbody2D;
	#endregion

	#region Declarations private
	private float _timerBurn;
	private bool _isHit = false;
	private bool _isBurn = false;
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

		#endregion
	}

	private void Start()
	{
		#region Initialize
		GetComponent<ParticleSystem>().enableEmission = false;
		GetComponent<ParticleSystem>().startRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isHit)
		{
			GetComponent<ParticleSystem>().enableEmission = true;

		}
		else
		{
			GetComponent<ParticleSystem>().enableEmission = false;
		}
		if (_isBurn)
		{
			_timerBurn += Time.deltaTime;
			if (_timerBurn > _timeToBurn)
			{
				if (_rigidbody2D != null)
				{
					_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				}
				Destroy(gameObject);
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
	public void SetIsHit(bool boo)
	{
		_isHit = boo;
	}

	public void SetIsBurn()
	{
		_isBurn = true;
	}
	#endregion

	#region Coroutine

	#endregion
}
