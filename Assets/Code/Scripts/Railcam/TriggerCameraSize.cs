using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Railcam2D;
public enum ActionCamera
{
	More,
	Less,
	OffsetMore,
	OffsetLess,
	Nothing
}

public class TriggerCameraSize : MonoBehaviour
{
	public ActionCamera _actionCamera;
	public float _newSizeCamera;
	public float _speedCamera;
	public float _offsetX;
	public float _offsetY;
	public float _speedOffsetX;
	public float _speedOffsetY;

	private ActionCamera _action;
	private static Camera _camera;
	private static Railcam2DCore _railcam;
	private bool _isTrigger = false;
	private static float _sizeCamera;
	private static float _speedCam;
	private static float _privateOffsetX;
	private static float _privateOffsetY;

	void Start()
	{
		_action = _actionCamera;
		_camera = Camera.main;
		_railcam = _camera.GetComponent<Railcam2DCore>();
		_sizeCamera = _newSizeCamera;
		_speedCam = _speedCamera;
		_privateOffsetX = _offsetX;
		_privateOffsetY = _offsetY;
	}

	void Update()
	{
		if (_isTrigger)
		{
			ChangeSizeCamera();
			ChangeOffsetCamera();
		}
	}

	void ChangeSizeCamera()
	{
		//Debug.Log(_railcam.OffsetY);
		switch (_action)
		{
			case ActionCamera.More:
				if (_camera.orthographicSize < _newSizeCamera)
				{
					_camera.orthographicSize += Time.deltaTime * _speedCam * _sizeCamera;
				}
				else
				{
					_isTrigger = false;
				}
				break;

			case ActionCamera.Less:
				if (_camera.orthographicSize > _newSizeCamera)
				{
					_camera.orthographicSize -= Time.deltaTime * _speedCam * _sizeCamera;
				}
				else
				{
					_isTrigger = false;
				}
				break;
		}
	}

	void ChangeOffsetCamera()
	{
		switch(_action)
		{
			case ActionCamera.OffsetMore:
				if (_railcam.OffsetX < _privateOffsetX)
				{
					_railcam.OffsetX += Time.deltaTime * _speedOffsetX;
				}

				if (_railcam.OffsetY < _privateOffsetY)
				{
					_railcam.OffsetY += Time.deltaTime * _speedOffsetY;
				}
				break;

			case ActionCamera.OffsetLess:
				if (_railcam.OffsetX > _privateOffsetX)
				{
					_railcam.OffsetX -= Time.deltaTime * _speedOffsetX;
				}

				if (_railcam.OffsetY > _privateOffsetY)
				{
					_railcam.OffsetY -= Time.deltaTime * _speedOffsetY;
				}
				break;
		}
		
		//_railcam.OffsetX = offsetX;
		//_railcam.OffsetY = offsetY;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			_isTrigger = true;
			_sizeCamera = _newSizeCamera;
			_speedCam = _speedCamera;
			_privateOffsetX = _offsetX;
			_privateOffsetY = _offsetY;
		}
	}

	//private void OnTriggerExit2D(Collider2D collision)
	//{
	//	if(collision.tag == "Player")
	//	{
	//		_isTrigger = false;
	//	}
	//}
}
