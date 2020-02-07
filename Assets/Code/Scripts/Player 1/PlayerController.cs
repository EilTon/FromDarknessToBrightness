﻿using Railcam2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Declarations public
	public float _speedPlayer1;
	public float _jumpImpulsePlayer1;
	public float _jumpForcePlayer1;
	public float _jumpTimeDelayPlayer1;
	public float _speedPlayer2;
	public float _jumpImpulsePlayer2;
	public float _jumpForcePlayer2;
	public float _jumpTimeDelayPlayer2;
	public float _airControlForcePlayer1;
	public float _airControlForcePlayer2;
	public LayerMask _layer;
	public GameObject _player2;
	public float _delayDetach;
	public float _delaySwitch;
	public float _delayShield;
	public float _limitAttach;
	public float _bufferDelay;
	public CircleCollider2D _Shield;
	#endregion

	#region Declarations private
	private AnimationManager _animationManager;
	private bool _isJumping = false;
	private bool _isFreeze = false;
	private bool _isGrounded;
	private bool _isBuffering = false;
	private bool _isReset = false;
	private bool _isTrigger = false;
	private bool _switch = false;
	private bool _detach = false;
	private Player2Controller _controllerPlayer2;
	private Rigidbody2D _rigidbodyPlayer1;
	private Rigidbody2D _rigidbodyPlayer2;
	private Rigidbody2D _rigidbodyPlayer;
	private float _distToGround;
	private float _jumpTime;
	private float _storeDelayShield;
	private float _storeDelayDetach;
	private float _storeDelaySwitch;
	private float _speed;
	private float _jumpImpulse;
	private float _jumpForce;
	private float _jumpTimeDelay;
	private float _horizontal;
	private float _airControlForce;
	private Railcam2DCore _camera;
	private Vector2 _resetPosition;
	private Vector2 _positionOrigin;
	private CapsuleCollider2D _colliderPlayer2Capsule;
	private Vector3 _movement;
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
		SetPlayer();
		_resetPosition = transform.position;
		_camera = FindObjectOfType<Railcam2DCore>();
		_rigidbodyPlayer1 = GetComponent<Rigidbody2D>();
		_rigidbodyPlayer2 = _player2.GetComponent<Rigidbody2D>();
		_rigidbodyPlayer = _rigidbodyPlayer1;
		_distToGround = GetComponent<Collider2D>().bounds.extents.y;
		_controllerPlayer2 = FindObjectOfType<Player2Controller>();
		_colliderPlayer2Capsule = _player2.GetComponent<CapsuleCollider2D>();
		StartCoroutine(Jumping());
		StartCoroutine(isGroundedBuffering());
		_animationManager = GetComponent<AnimationManager>();
		#endregion
	}

	private void Update()
	{
		#region Movement
		Move();
		#endregion

		#region Actions
		//_controllerPlayer2.SetPlayerAngle(transform.eulerAngles.y);
		DetachAttach();
		SwitchPlayer();
		MoveShieldTrigger();
		//ShieldMode();

		#endregion

		#region Timer
		if (_delaySwitch <= _storeDelaySwitch)
		{
			_delaySwitch += Time.deltaTime;
		}
		if (_delayDetach <= _storeDelayDetach)
		{
			_delayDetach += Time.deltaTime;
		}
		if (_delayShield <= _storeDelayShield)
		{
			_delayShield += Time.deltaTime;
		}
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement
		MovePlayer();
		if (_rigidbodyPlayer.velocity.y < -0.1f)
		{
			_animationManager.SetDuoLanding();
		}
		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}

	#endregion

	#region Helper
	void ShieldMode()
	{
		if (_delayShield >= _storeDelayShield && _detach == false)
		{
			if (Input.GetAxisRaw("LeftTrigger") == 1)
			{
				_controllerPlayer2.SetTrigger(true);
				_isTrigger = true;
				if (_isReset == false)
				{
					_isReset = true;
				}
			}
			else if (_isReset == true)
			{
				_rigidbodyPlayer.transform.eulerAngles = new Vector2(0, 0);
				_positionOrigin = _controllerPlayer2.GetPositionOrigin();
				_rigidbodyPlayer2.transform.position = _positionOrigin;
				_isReset = false;
				_isTrigger = false;
				_controllerPlayer2.SetTrigger(false);
			}
		}
	}

	void Move()
	{
		if (_isFreeze == false)
		{
			_horizontal = Input.GetAxis("Horizontal");
			if (_detach == false)
			{
				_animationManager.StartSpeedDuo();
				_animationManager.SetSpeedDuo(_horizontal);
			}
			else
			{
				_animationManager.StopSpeedDuo();
				if (_switch == false)
				{
					_animationManager.SetSpeedLichen(_horizontal);
				}
				else
				{
					_animationManager.SetSpeedTrilo(_horizontal);
				}

			}

			if (Mathf.Abs(_horizontal) > 0.1f && _isTrigger == false) _rigidbodyPlayer.transform.eulerAngles = new Vector2(0, (Mathf.Sign(-_horizontal) + 1) * 90);

		}
	}

	void MovePlayer()
	{
		if (_isGrounded)
		{
<<<<<<< HEAD
			//if(_isJumping == true)
			//{
			//	_animationManager.StopDuoJump();
			//	_isJumping = false;
			//}
			//_isJumping = false;
=======
			_animationManager.StopDuoLanding();
>>>>>>> origin/IntegrationAnimations
			_rigidbodyPlayer.velocity = (Vector3.right * _horizontal * _speed) + Vector3.up * _rigidbodyPlayer.velocity.y;
		}
		else
		{
			_rigidbodyPlayer.AddForce(Vector3.right * _horizontal * _airControlForce * Time.fixedDeltaTime);
			_rigidbodyPlayer.velocity = new Vector2(Mathf.Clamp(_rigidbodyPlayer.velocity.x, -_speed, _speed), _rigidbodyPlayer.velocity.y);
		}
	}

	bool CheckIfGrounded()
	{
		return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.01f), new Vector2(0, -_distToGround + 0.01f), -_distToGround - 1f, _layer);
	}

	void DetachAttach()
	{
		if (Input.GetButtonDown("DetachAttach") && (_rigidbodyPlayer2.transform.position.x >= _rigidbodyPlayer.transform.position.x - _limitAttach && _rigidbodyPlayer2.transform.position.x <= _rigidbodyPlayer.transform.position.x + _limitAttach))
		{
			if (_detach == false && _switch == false)
			{
				_rigidbodyPlayer2.transform.parent = null;
				_colliderPlayer2Capsule.enabled = true;
				_Shield.enabled = false;
				_rigidbodyPlayer2.bodyType = RigidbodyType2D.Dynamic;
				_detach = !_detach;
			}

			else if (_detach == true && _switch == false)
			{
				_colliderPlayer2Capsule.enabled = false;
				_Shield.enabled = true;
				_rigidbodyPlayer2.transform.position = new Vector2(transform.position.x - 0.1300149f, transform.position.y - 0.03745449f);
				if (_rigidbodyPlayer2.transform.eulerAngles.y != _rigidbodyPlayer1.transform.eulerAngles.y)
				{
					_rigidbodyPlayer2.transform.eulerAngles = new Vector2(_rigidbodyPlayer2.transform.eulerAngles.x, _rigidbodyPlayer1.transform.eulerAngles.y);
				}
				_rigidbodyPlayer2.transform.SetParent(transform);
				_rigidbodyPlayer2.bodyType = RigidbodyType2D.Kinematic;
				_detach = !_detach;
			}
		}
	}

	void SwitchPlayer()
	{

		if (Input.GetButtonDown("Switch") && _delaySwitch >= _storeDelaySwitch && _detach == true)
		{
			_animationManager.SetSpeedLichen(0);
			_animationManager.SetSpeedTrilo(0);
			_delaySwitch = 0;
			if (_switch == false)
			{
				_rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
				_rigidbodyPlayer.velocity = Vector2.zero;
				_camera.Target = _rigidbodyPlayer2.transform;
				_rigidbodyPlayer = _rigidbodyPlayer2;
				_switch = !_switch;
				SetPlayer();
			}
			else if (_switch == true)
			{
				_rigidbodyPlayer.velocity = Vector2.zero;
				_camera.Target = _rigidbodyPlayer1.transform;
				_rigidbodyPlayer = _rigidbodyPlayer1;
				_switch = !_switch;
				SetPlayer();
			}
		}
	}

	void MoveShieldTrigger()
	{

		//if(_detach == false)
		//{
		//	float trigger = Input.GetAxis("LTRT");
		//	Vector3 test = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + trigger * Time.deltaTime * _shieldSpeed);
		//	Debug.Log(test);
		//	transform.eulerAngles = test;

		//}
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
		//if (_player1.GetComponent<PlayerController>().GetDetach() == false)
		//{
		//	float trigger = Input.GetAxis("LTRT");
		//	_currentShieldAngle = Mathf.Clamp(_currentShieldAngle + trigger * _shieldSpeed * _player1.transform.right.x * Time.deltaTime, _minMaxShieldAngle.x, _minMaxShieldAngle.y);
		//	Vector2 p1Top2 = new Vector2(Mathf.Sin(_currentShieldAngle * Mathf.Deg2Rad * _player1.transform.right.x), Mathf.Cos(_currentShieldAngle * Mathf.Deg2Rad * _player1.transform.right.x));
		//	transform.position = _player1.transform.position + (Vector3)p1Top2 * _shieldDistance;
		//	transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(-p1Top2.x, p1Top2.y) * Mathf.Rad2Deg + 90, Vector3.forward);
		//}
	}

	void SetPlayer()
	{
		if (_switch == false)
		{
			_jumpImpulse = _jumpImpulsePlayer1;
			_jumpForce = _jumpForcePlayer1;
			_jumpTimeDelay = _jumpTimeDelayPlayer1;
			_speed = _speedPlayer1;
			_airControlForce = _airControlForcePlayer1;
		}
		else
		{
			_jumpImpulse = _jumpImpulsePlayer2;
			_jumpForce = _jumpForcePlayer2;
			_jumpTimeDelay = _jumpTimeDelayPlayer2;
			_speed = _speedPlayer2;
			_airControlForce = _airControlForcePlayer2;
		}
	}

	public void SetResetPosition(Vector2 position)
	{
		_resetPosition = position;
	}

	public bool GetDetach()
	{
		return _detach;
	}

	public void ResetPlayer()
	{
		_rigidbodyPlayer.velocity = Vector2.zero;
		_rigidbodyPlayer = _rigidbodyPlayer1;
		_jumpImpulse = _jumpImpulsePlayer1;
		_jumpForce = _jumpForcePlayer1;
		_jumpTimeDelay = _jumpTimeDelayPlayer1;
		_speed = _speedPlayer1;
		_airControlForce = _airControlForcePlayer1;
		_rigidbodyPlayer.transform.eulerAngles = new Vector2(0,0);
		_camera.Target = _rigidbodyPlayer.transform;
		_rigidbodyPlayer2.transform.parent = _rigidbodyPlayer.transform;
		_rigidbodyPlayer2.bodyType = RigidbodyType2D.Kinematic;
<<<<<<< HEAD
=======
		_rigidbodyPlayer2.transform.eulerAngles = new Vector2(0, 0);
>>>>>>> origin/IntegrationAnimations
		_rigidbodyPlayer2.transform.position = new Vector2(transform.position.x - 0.1300149f, transform.position.y - 0.03745449f);
		_switch = false;
		_detach = false;
		_rigidbodyPlayer.position = _resetPosition;
	}

	public void SetFreeze(bool freeze)
	{
		_isFreeze = freeze;
	}

	#endregion

	#region Coroutine
	float delayJump = 0;
	IEnumerator Jumping()
	{
		while (true)
		{

			if (_isFreeze == true)
			{
				delayJump = 0.2f;
			}
			else if (delayJump > 0)
			{
				delayJump -= 0.01f;
			}

			if (Input.GetButtonDown("Jump") && _isGrounded && delayJump <= 0)
			{
				_isJumping = true;
<<<<<<< HEAD
				if(_detach == false)
				{
					//_animationManager.SetDuoJump();
				}
=======

				if (_detach == false)
				{
					_animationManager.SetDuoJump();
				}

>>>>>>> origin/IntegrationAnimations
				_jumpTime = Time.time;
				_rigidbodyPlayer.AddForce(Vector2.up * _jumpImpulse, ForceMode2D.Impulse);

				while (Input.GetButton("Jump") && Time.time < _jumpTime + _jumpTimeDelay)
				{
					_rigidbodyPlayer.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
					yield return new WaitForFixedUpdate();
				}
			}

			yield return null;
		}
	}

	IEnumerator isGroundedBuffering()
	{
		float lastHitTime = 0;
		while (true)
		{
			if (CheckIfGrounded())
			{
				lastHitTime = Time.time;
			}

			_isGrounded = (lastHitTime > Time.time - _bufferDelay);

			yield return null;
		}
	}

	#endregion
}
