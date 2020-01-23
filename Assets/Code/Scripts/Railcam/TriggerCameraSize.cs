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

	public Camera _camera;
	private bool _isTrigger = false;
	private static float _sizeCamera;
	private static float _speedCam;
	public static float offsetX;
	public static float offsetY;
	void Start()
	{
		_camera = Camera.main;
		_sizeCamera = _newSizeCamera;
		_speedCam = _speedCamera;
		offsetX=_offsetX;
		offsetY=_offsetY;
	}
	private void Update()
	{
		if (_isTrigger)
		{
			ChangeSizeCamera();
			//ChangeOffsetCamera();
		}
	}

	void ChangeSizeCamera()
	{
		switch (_moreOrLess)
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
				break;

		}
	}

	void ChangeOffsetCamera()
	{
		_camera.GetComponent<Railcam2DCore>().OffsetX = offsetX;
		_camera.GetComponent<Railcam2DCore>().OffsetY = offsetY;
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
