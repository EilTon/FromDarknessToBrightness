using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public FreeParallax _parallax;
	public float _speedParallax;

	private float _horizontal;
	private Camera _camera;

	private void Start()
	{
		_camera = Camera.main;
	}

	private void Update()
	{
		//Debug.Log(_camera.velocity.x);
		MoveParallax();
	}

	void MoveParallax()
	{
		if (_parallax != null)
		{
			if (_camera.velocity.x < -3)
			{
				_parallax.Speed = _speedParallax;
			}
			else if (_camera.velocity.x > 3)
			{
				_parallax.Speed = -_speedParallax;
			}
			else
			{
				_parallax.Speed = 0.0f;
			}
		}
	}
}
