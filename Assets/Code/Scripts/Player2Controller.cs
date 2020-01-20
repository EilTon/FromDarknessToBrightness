using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
	public GameObject _player1;
	private ShieldScript _shield;
	private float _rightX;
	private int _cursor = 0;
	private bool _trigger = false;
	void Start()
	{
		_shield = _player1.GetComponent<ShieldScript>();
		_cursor = _shield._positions.Count;
	}

	// Update is called once per frame
	void Update()
	{
		if(_trigger)
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
}
