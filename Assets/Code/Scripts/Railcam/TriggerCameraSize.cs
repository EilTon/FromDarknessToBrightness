using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoreOrLessSize
{
	More,
	Less
}

public class TriggerCameraSize : MonoBehaviour
{
	public  MoreOrLessSize _moreOrLess;
	public  float _newSizeCamera;
	public  float _speedCamera;
	private Camera _camera;
	private  bool _isTrigger = false;
	private static float _sizeCamera;
	private static  float _speedCam;
	void Start()
	{
		_camera = Camera.main;
		_sizeCamera = _newSizeCamera;
		_speedCam = _speedCamera;
	}
	private void Update()
	{
		if (_isTrigger)
		{
			switch(_moreOrLess)
			{
				case MoreOrLessSize.More:
					if (_camera.orthographicSize < _newSizeCamera)
					{
						_camera.orthographicSize += Time.deltaTime * _speedCam;
					}
					else
					{
						_isTrigger = false;
					}
					break;

				case MoreOrLessSize.Less:
					if (_camera.orthographicSize > _newSizeCamera)
					{
						_camera.orthographicSize -= Time.deltaTime * _speedCam;
					}
					else
					{
						_isTrigger = false;
					}
					break;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.tag);
		if (collision.tag == "Player")
		{
			_isTrigger = true;
		}
	}
}
