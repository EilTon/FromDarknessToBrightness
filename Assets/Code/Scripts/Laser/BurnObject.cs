using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnObject : MonoBehaviour
{
	#region Declarations public
	public float _timeToBurn;
	#endregion

	#region Declarations private
	private float _timerBurn;
	private bool _isHit = false;
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

		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isHit)
		{
			_timerBurn += Time.deltaTime;
			if (_timerBurn > _timeToBurn)
			{
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
	public void SetIsHit()
	{
		_isHit = true;
	}
	#endregion

	#region Coroutine

	#endregion
}
