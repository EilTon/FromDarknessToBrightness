using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
	#region Declarations public
	public GameObject _player1;
	#endregion

	#region Declarations private
	private ShieldScript _shield;
	private float _rightX;
	private int _cursor = 0;
	private bool _trigger = false;
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
		_shield = _player1.GetComponent<ShieldScript>();
		_cursor = _shield._positions.Count;
		#endregion
	}

	private void Update()
	{
		#region Movement
		MoveShield();
		#endregion

		#region Actions

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
	void MoveShield()
	{
		if (_trigger)
		{
			_rightX = Input.GetAxis("RightJoystickX");
			List<Vector2> positions = _shield._positions;
			positions.Reverse();
			if (_rightX > 0 && _cursor < positions.Count)
			{
				_cursor++;
				transform.position = positions[_cursor];
			}
			if (_rightX < 0 && _cursor > -1)
			{
				_cursor--;
				transform.position = positions[_cursor];
			}
		}
	}

	public void SetTrigger()
	{
		_trigger = !_trigger;
	}
	#endregion

	#region Coroutine

	#endregion
}
