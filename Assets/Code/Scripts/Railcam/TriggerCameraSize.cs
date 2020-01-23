using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Railcam2D;
public enum ActionCamera
{
	More,
	Less,
	Offset,
	Nothing
}

public class TriggerCameraSize : MonoBehaviour
{
	public ActionCamera _moreOrLess;
	public float _newSizeCamera;
	public float _speedCamera;
	public float _offsetX;
	public float _offsetY;

	private ActionCamera _action;
	private static Camera _camera;
	private static Railcam2DCore _railcam;
	private bool _isTrigger = false;
	private static float _sizeCamera;
	private static float _speedCam;
	private static float offsetX;
	private static float offsetY;

	void Start()
	{
		_action = _moreOrLess;
		_camera = Camera.main;
		_railcam = _camera.GetComponent<Railcam2DCore>();
		_sizeCamera = _newSizeCamera;
		_speedCam = _speedCamera;
		offsetX = _offsetX;
		offsetY = _offsetY;
	}

	void Update()
	{
		if (_isTrigger)
		{
			ChangeSizeCamera();
			//ChangeOffsetCamera();
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

			case ActionCamera.Offset:
				ChangeOffsetCamera();
				//action = ActionCamera.Nothing;
				break;

		}
	}

	void ChangeOffsetCamera()
	{
		_railcam.OffsetX = offsetX;
		_railcam.OffsetY = offsetY;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			_isTrigger = true;
			_sizeCamera = _newSizeCamera;
			_speedCam = _speedCamera;
			offsetX = _offsetX;
			offsetY = _offsetY;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			_isTrigger = false;
		}
	}
}
