using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
	public GameObject _player1;
	private TestShield _shiedl;
	private float _rightX;
	private float _rightY;
	private int _cursor = 0;
    void Start()
    {
		_shiedl = _player1.GetComponent<TestShield>();
    }

    // Update is called once per frame
    void Update()
    {
		_rightX = Input.GetAxis("RightJoystickX");
		_rightY = Input.GetAxis("RightJoystickY");
		Debug.Log(_cursor);
		List<Vector2> positions = _shiedl._positions;
		if (_rightX > 0)
		{
			_cursor--;
			transform.position = positions[_cursor];
		}
		if (_rightX < 0)
		{
			_cursor++;
			transform.position = positions[_cursor];
		}
	}
}
