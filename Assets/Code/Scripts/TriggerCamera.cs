using Railcam2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraOption
{
	ChangeSize,
	Offset
}

public class TriggerCamera : MonoBehaviour
{
	#region Declarations public
	public CameraOption _actionCamera;
	public float _newSizeCamera;
	public float _speedCamera;
	public float _offsetX;
	public float _offsetY;
	public float _speedOffsetX;
	public float _speedOffsetY;
	#endregion

	#region Declarations private
	private CameraOption _action;
	private static Camera _camera;
	private static Railcam2DCore _railcam;
	private bool _isTrigger = false;
	private static float _sizeCamera;
	private static float _speedCam;
	private static float _privateOffsetX;
	private static float _privateOffsetY;
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
		_action = _actionCamera;
		_camera = Camera.main;
		_railcam = _camera.GetComponent<Railcam2DCore>();
		_sizeCamera = _newSizeCamera;
		_speedCam = _speedCamera;
		_privateOffsetX = _offsetX;
		_privateOffsetY = _offsetY;
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isTrigger)
		{
			CameraSetup();
		}
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
	#endregion

	#region Helper
	void CameraSetup()
	{
		switch(_action)
		{
			case CameraOption.ChangeSize:
				ChangeSizeCamera();
				break;

			case CameraOption.Offset:
				ChangeOffsetCamera();
				break;
		}
	}

	void ChangeSizeCamera()
	{
		if(_camera.orthographicSize != _newSizeCamera)
		{
			if (_camera.orthographicSize < _newSizeCamera)
			{
				_camera.orthographicSize += Mathf.Round(Time.deltaTime * _speedCam * _sizeCamera);
			}
			else if (_camera.orthographicSize > _newSizeCamera)
			{
				_camera.orthographicSize -= Mathf.Round(Time.deltaTime * _speedCam * _sizeCamera);
			}
		}
		else
		{
			_isTrigger = false;
		}
		
	}

	void ChangeOffsetCamera()
	{
		if(_railcam.OffsetX != _privateOffsetX)
		{
			if (_railcam.OffsetX < _privateOffsetX)
			{
				_railcam.OffsetX += Mathf.Round(Time.deltaTime * _speedOffsetX * _privateOffsetX);
			}

			else if (_railcam.OffsetX > _privateOffsetX)
			{
				_railcam.OffsetX -= Mathf.Round(Time.deltaTime * _speedOffsetX * _privateOffsetX);
			}
		}

		else if(_railcam.OffsetY != _privateOffsetY)
		{
			 if (_railcam.OffsetY < _privateOffsetY)
			{
				_railcam.OffsetY += Mathf.Round(Time.deltaTime * _speedOffsetY * _privateOffsetY );
			}

			else if (_railcam.OffsetY > _privateOffsetY)
			{
				_railcam.OffsetY -= Mathf.Round(Time.deltaTime * _speedOffsetY * _privateOffsetY );
			}
		}

		else
		{
			_isTrigger = false;
		}
	}
	#endregion

	#region Coroutine

	#endregion
}
