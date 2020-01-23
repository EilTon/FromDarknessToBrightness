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
	public MoreOrLessSize _moreOrLess;
	public float _newSizeCamera;
	public float _speedCamera;
	private Camera _camera;
	private bool _isTrigger = false;
	void Start()
	{
		_camera = Camera.main;
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
						_camera.orthographicSize += Time.deltaTime * _speedCamera;
					}
					break;

				case MoreOrLessSize.Less:
					if (_camera.orthographicSize > _newSizeCamera)
					{
						_camera.orthographicSize -= Time.deltaTime * _speedCamera;
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
