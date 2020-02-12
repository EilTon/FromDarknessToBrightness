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
	Streching,
	HoldStreching,
	Nothing
}

public enum DirectionPlatform
{
	Up, Down, Left, Right
}
public class ActionEnable : MonoBehaviour
{
	#region Declarations public
	public Action _actionPublic;
	public DirectionPlatform _directionPlatform;
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
	[HideInInspector]
	public bool _isStreching;
	public UnityEvent _Action;
	public GameObject _platformToHold;
	public float _timeToDecreaze;
	#endregion

	#region Declarations private
	private float _direction = 1;
	private Action _action;
	private float _timerStrech;
	private float _timerGrowth;
	private float _timerDecreaze;
	private Vector2 _originTransform;
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
		_originTransform = new Vector2(transform.position.x,transform.position.y);
		float x = transform.position.x;
		float y = transform.position.y;
		//_resetStrech.position = transform.position;
		_limitRight = x + _limitRight;
		_limitLeft = x - _limitLeft;
		_limitDown = y - _limitDown;
		_limitUp = y + _limitUp;

		switch (_actionPublic)
		{
			case Action.TranslateHorizontal:
				TranslateHorizontalPlatform();
				break;

			case Action.TranslateVertical:
				TranslateVerticalPlatform();
				break;

			case Action.Nothing:
				_action = Action.Nothing;
				break;

			case Action.Streching:
				Strenching();
				break;

			case Action.Growing:
				Growing();
				break;

		}

		if (_directionPlatform == DirectionPlatform.Left || _directionPlatform == DirectionPlatform.Down)
		{
			_direction = -1;
		}
		else
		{
			_direction = 1;
		}
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		switch (_action)
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

			case Action.HoldStreching:
				HoldStrechGameObject();
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

	public void HoldStrech()
	{
		_action = Action.HoldStreching;
	}

	void MoveHorizontal()
	{
		Vector2 movement;
		if (transform.position.x > _limitRight && _direction != -1)
		{
			_direction = -1;
		}
		else if (transform.position.x < _limitLeft && _direction != 1)
		{
			_direction = 1;
		}
		movement = Vector2.right * _direction * _speed * Time.deltaTime;
		transform.Translate(movement);
	}

	void MoveVertical()
	{
		Vector2 movement;
		if (transform.position.y > _limitUp && _direction != -1)
		{
			_direction = -1;
		}
		else if (transform.position.y < _limitDown && _direction != 1)
		{
			_direction = 1;
		}
		movement = Vector2.up * _direction * _speed * Time.deltaTime;
		transform.Translate(movement);
	}

	void HoldStrechGameObject()
	{

		if (_timerStrech < _timeToStreching && _isStreching == true)
		{
			transform.Translate(new Vector2(0, 1 * _speedPositionY * Time.deltaTime));
			transform.localScale += new Vector3(0, 1 * _speedScaleY * Time.deltaTime);
			_platformToHold.transform.Translate(new Vector2(0, 1 * -_speedPositionY * Time.deltaTime));
			_timerStrech += Time.deltaTime;
		}
		else if (_isStreching == false && _timerStrech > 0)
		{
			transform.Translate(new Vector2(0, 1 * -_speedPositionY * Time.deltaTime));
			transform.localScale += new Vector3(0, 1 * -_speedScaleY * Time.deltaTime);
			_platformToHold.transform.Translate(new Vector2(0, 1 * _speedPositionY * Time.deltaTime));
			_timerStrech -= Time.deltaTime;
		}

	}

	void StrechGameObject()
	{
		if (_timerStrech < _timeToStreching)
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
