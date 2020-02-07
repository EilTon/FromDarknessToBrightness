using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
	#region Declarations public
	public GameObject _player1;
	public GameObject _colliderBody;
	public float _shieldSpeed = 15;
	public float _shieldDistance = 1;
	public Vector2 _minMaxShieldAngle = new Vector2(-30, 30);
	public float _currentShieldAngle = 0;
	#endregion

	#region Declarations privates
	private AnimationManager _animationManager;
	private bool _rotation = false;
	private float _playerAngles;
	private ShieldScript _shield;
	private float _rightX;
	private int _cursor = 0;
	private bool _trigger = false;
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
		_shield = _player1.GetComponent<ShieldScript>();
		_animationManager = FindObjectOfType<AnimationManager>();
		//_cursor = _shield._positions.Count;
		#endregion
	}

	private void Update()
	{
		#region Movement
		//MoveShield();
		MoveShieldTrigger();
		#endregion

		#region Actions
		//SetCursor();
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
	void MoveShieldJoyStick()
	{
		if (_trigger)
		{
			_rightX = Input.GetAxis("RightJoystickX");
			List<Vector2> positions = _shield._positions;
			positions.Reverse();
			if (_rightX > 0 && _cursor < positions.Count)
			{
				_cursor++;
				transform.position = positions[_cursor];
			}
			if (_rightX < 0 && _cursor > -1)
			{
				_cursor--;
				transform.position = positions[_cursor];
			}
		}
		else
		{
			_cursor = 0;
			_rightX = 0;
		}
	}

	
	void MoveShieldTrigger()
	{
		//List<Vector2> positions = _shield._positions;
		//positions.Reverse();
		//if (trigger > 0 && _cursor < positions.Count)
		//{
		//	_cursor++;
		//}
		//if (trigger < 0 && _cursor > -1)
		//{
		//	_cursor--;
		//}
		
		if (_player1.GetComponent<PlayerController>().GetDetach()==false)
		{
			float trigger = Input.GetAxis("LTRT");
			_currentShieldAngle = Mathf.Clamp(_currentShieldAngle + trigger * _shieldSpeed * _player1.transform.right.x * Time.deltaTime, _minMaxShieldAngle.x, _minMaxShieldAngle.y);
			Vector2 p1Top2 = new Vector2(Mathf.Sin(_currentShieldAngle * Mathf.Deg2Rad * _player1.transform.right.x), Mathf.Cos(_currentShieldAngle * Mathf.Deg2Rad * _player1.transform.right.x));
			transform.position = _player1.transform.position + (Vector3)p1Top2 * _shieldDistance;
<<<<<<< HEAD
			_animationManager.SetCourbe(_currentShieldAngle / _minMaxShieldAngle.y);
=======
			//_colliderBody.transform.position = _player1.transform.position + (Vector3)p1Top2 * _shieldDistance; 
			_animationManager.SetCourbe(_currentShieldAngle / _minMaxShieldAngle.y);
			
>>>>>>> origin/IntegrationAnimations
			//_test.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(-p1Top2.x , p1Top2.y) * Mathf.Rad2Deg * _currentShieldAngle/_minMaxShieldAngle.y, Vector3.forward) * _player1.transform.rotation;
		}
	}

	public void SetTrigger(bool trigger)
	{
		_trigger = trigger;
	}

	public Vector2 GetPositionOrigin()
	{
		return _shield._positions[0];
	}

	//void SetCursor()
	//{
	//	if (_playerAngles == 180 && _rotation == false)
	//	{
	//		_cursor = Mathf.Abs(100 - _cursor);
	//		_rotation = true;
	//	}
	//	else if (_playerAngles == 0 && _rotation == true)
	//	{
	//		_cursor = Mathf.Abs(100 - _cursor);
	//		_rotation = false;
	//	}
	//}

	//public void SetPlayerAngle(float y)
	//{
	//	_playerAngles = y;
	//}
	#endregion

	#region Coroutine

	#endregion
}
