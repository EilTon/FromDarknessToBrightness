using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum Action
{
	TranslateHorizontal, 
	TranslateVertical, 
	OpenGate,
	Growing,
	Streching
}
public class ActionEnable : MonoBehaviour
{
	#region Declarations public
	public float _limitLeft;
	public float _limitRight;
	public float _limitUp;
	public float _limitDown;
	public float _speed;
	public float _speedScaleY;
	public float _speedPositionY;
	public float _speedScaleX;
	public float _speedPositionX;
	public float _timeToStreching;
	public float _timeToGrowing;
	#endregion

	#region Declarations private
	private Action _action;
	private float _direction = 1;
	private float _timerStrech;
	private float _timerGrowth;
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

		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion
		#region Actions
		switch(_action)
		{
			case Action.TranslateHorizontal:
				MoveHorizontal();
				break;

			case Action.TranslateVertical:
				MoveVertical();
				break;

			case Action.OpenGate:
				break;

			case Action.Streching:
				StrechGameObject();
				break;

			case Action.Growing:
				GrownthGameObject();
				break;

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
	#endregion

	#region Helper
	public void TranslateHorizontalPlatform()
	{
		_action = Action.TranslateHorizontal;
	}

	public void TranslateVerticalPlatform()
	{
		_action = Action.TranslateVertical;
	}

	public void OpenGate()
	{
		_action = Action.OpenGate;
	}

	public void Strenching()
	{
		_action = Action.Streching;
	}

	public void Growing()
	{
		_action = Action.Growing;
	}

	void MoveHorizontal()
	{
		Vector2 movement;
		if (transform.position.x > _limitRight)
		{
			_direction = -1;
		}
		else if (transform.position.x < _limitLeft)
		{
			_direction = 1;
		}
		movement = Vector2.right * _direction * _speed * Time.deltaTime;
		transform.Translate(movement);
	}

	void MoveVertical()
	{
		Vector2 movement;
		if (transform.position.y > _limitUp)
		{
			_direction = -1;
		}
		else if (transform.position.y < _limitDown)
		{
			_direction = 1;
		}
		movement = Vector2.up * _direction * _speed * Time.deltaTime;
		transform.Translate(movement);
	}

	void StrechGameObject()
	{
		if(_timerStrech<_timeToStreching)
		{
			transform.Translate(new Vector2(0, 1 * _speedPositionY * Time.deltaTime));
			transform.localScale += new Vector3(0, 1 * _speedScaleY * Time.deltaTime);
		}
		_timerStrech += Time.deltaTime;

	}

	void GrownthGameObject()
	{
		if (_timerGrowth < _timeToGrowing)
		{
			transform.Translate(new Vector2(1 * _speedPositionX * Time.deltaTime, 1 * _speedPositionY * Time.deltaTime));
			transform.localScale += new Vector3(1 * _speedScaleX * Time.deltaTime, 1 * _speedScaleY * Time.deltaTime);
		}
		_timerGrowth += Time.deltaTime;
	}
	#endregion

	#region Coroutine

	#endregion
}
