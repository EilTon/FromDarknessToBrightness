using Railcam2D;
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
	public float _limitAttach;
	public float _bufferDelay;
	#endregion

	#region Declarations private
	private bool _isGrounded;
	private bool _isBuffering = false;
	private Player2Controller _controllerPlayer2;
	private Rigidbody2D _rigidbodyPlayer1;
	private Rigidbody2D _rigidbodyPlayer2;
	private Rigidbody2D _rigidbodyPlayer;
	private float _distToGround;
	private float _jumpTime;
	private Vector3 _movement;
	private bool _isTrigger = false;
	private bool _switch = false;
	private bool _detach = false;
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
	private bool _isReset = false;
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
		StartCoroutine(Jumping());
		StartCoroutine(isGroundedBuffering());
		#endregion
	}
	private void Update()
	{
		#region Movement
		Move();
		#endregion

		#region Actions
		DetachAttach();
		SwitchPlayer();
		ShieldMode();
		
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
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement
		MovePlayer();
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
		if (Input.GetAxisRaw("LeftTrigger") == 1)
		{
			_controllerPlayer2.SetTrigger(true);
			_isTrigger = true;
			if(_isReset==false)
			{
				_isReset = true;
			}
		}
		else if(_isReset == true)
		{
			_positionOrigin = _controllerPlayer2.GetPositionOrigin();
			_rigidbodyPlayer2.transform.position = _positionOrigin;
			_isReset = false;
		}
		else
		{
			_isTrigger = false;
			_controllerPlayer2.SetTrigger(false);
		}
	}

	void Move()
	{
		_horizontal = Input.GetAxis("Horizontal");
		if (Mathf.Abs(_horizontal) > 0.1f && _isTrigger == false) _rigidbodyPlayer.transform.eulerAngles = new Vector2(0, (Mathf.Sign(-_horizontal) + 1) * 90);
	}

	void MovePlayer()
	{
		if (_isGrounded)
		{
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
				_rigidbodyPlayer2.bodyType = RigidbodyType2D.Dynamic;
				_detach = !_detach;
			}

			else if (_detach == true && _switch == false)
			{
				_rigidbodyPlayer2.transform.position = transform.position + -transform.right + transform.up * 0.75f;
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
			_delaySwitch = 0;
			if (_switch == false)
			{
				_camera.Target = _rigidbodyPlayer2.transform;
				_rigidbodyPlayer = _rigidbodyPlayer2;
				_switch = !_switch;
				SetPlayer();
			}
			else if (_switch == true)
			{
				_camera.Target = _rigidbodyPlayer1.transform;
				_rigidbodyPlayer = _rigidbodyPlayer1;
				_switch = !_switch;
				SetPlayer();
			}
		}
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

	public void ResetPlayer()
	{
		_rigidbodyPlayer.position = _resetPosition;
	}

	#endregion

	#region Coroutine

	IEnumerator Jumping()
	{
		while (true)
		{
			if (Input.GetButtonDown("Jump") && _isGrounded)
			{
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
