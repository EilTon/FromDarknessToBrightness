using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEnable : MonoBehaviour
{
	#region Declarations public
	public float _limitLeft;
	public float _limitRight;
	public float _limitUp;
	public float _limitDown;
	public float _speed;
	#endregion

	#region Declarations private
	private float _direction = 1;
	private bool _isTranslateHorizontal = false;
	private bool _isTranslateVertical = false;
	private bool _isOpenGate = false;
	private bool _isGrowing = false;
	private bool _isStreching = false;
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
		if (_isTranslateHorizontal)
		{
			MoveHorizontal();
		}
		else if (_isTranslateVertical)
		{
			MoveVertical();
		}
		else if (_isOpenGate)
		{
			Debug.Log("Gate open");
		}
		else if (_isGrowing)
		{
			Debug.Log("Growth");
		}
		else if (_isStreching)
		{
			Debug.Log("Strech");
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
		_isTranslateHorizontal = true;
	}

	public void TranslateVerticalPlatform()
	{
		_isTranslateVertical = true;
	}

	public void OpenGate()
	{
		_isOpenGate = true;
	}

	public void Strenching()
	{
		_isStreching = true;
	}

	public void Growing()
	{
		_isGrowing = true;
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

	}

	void GrownthGameObject()
	{

	}
	#endregion

	#region Coroutine

	#endregion
}
