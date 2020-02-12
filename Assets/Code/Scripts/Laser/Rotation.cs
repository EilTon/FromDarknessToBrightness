using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	public float _limitLeft;
	public float _limitRight;
	public float _speed;
	public float _direction;
	private float _rotationStart;
	// Start is called before the first frame update
	void Start()
	{
		_rotationStart = transform.localEulerAngles.z;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 movement;
		if (transform.localEulerAngles.z > _limitRight + _rotationStart && _direction != -1)
		{
			_direction = -1;
		}
		else if (transform.localEulerAngles.z < _limitLeft + _rotationStart && _direction != 1)
		{
			_direction = 1;
		}
		movement = new Vector3(0, 0, 1 * _direction * _speed * Time.deltaTime); //Vector2.right * _direction * _speed * Time.deltaTime;
		transform.Rotate(movement);
	}
}
