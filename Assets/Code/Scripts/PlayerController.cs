using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Declarations public
	public float _jumpImpulse;
	public float _jumpForce;
	public float _speedMax;
	public float _speedDelay;
	public float _jumpTimeDelay;
	public LayerMask _layer;
	public GameObject _player2;
	public float _delayDetach;
	public float _delaySwitch;
	public float _limitAttach;
	#endregion

	#region Declarations private
	private bool _isGrounded;
	private Rigidbody2D _rigidbodyPlayer1;
	private Rigidbody2D _rigidbodyPlayer2;
	private Rigidbody2D _rigidbodyPlayer;
	private float _distToGround;
	private float _jumpTime;
	private Vector3 _movement;
	private bool _switch = false;
	private bool _detach = false;
	private float _storeDelayDetach;
	private float _storeDelaySwitch;
	private float _accelerationPerSecond;
	private float _speed;
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
		_rigidbodyPlayer1 = GetComponent<Rigidbody2D>();
		_rigidbodyPlayer2 = _player2.GetComponent<Rigidbody2D>();
		_rigidbodyPlayer = _rigidbodyPlayer1;
		_distToGround = GetComponent<Collider2D>().bounds.extents.y;
		_accelerationPerSecond = _speedMax/_speedDelay;
		StartCoroutine(Jumping());
		#endregion
	}

	private void Update()
	{
		#region Movement
		_isGrounded = CheckIfGrounded();
		Move();
		#endregion

		#region Actions
		DetachAttach();
		SwitchPlayer();
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
	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		if(horizontal!=0)
		{
			_speed += horizontal * _accelerationPerSecond * Time.deltaTime;
		}
		else
		{
			_speed = 0;
		}
		if(_speed>_speedMax)
		{
			_speed = _speedMax;
		}
		Debug.Log(_speed);
		_movement = new Vector3(_speed, _rigidbodyPlayer.velocity.y);
	}

	void MovePlayer()
	{
		_rigidbodyPlayer.AddForce(_movement);
	}

	bool CheckIfGrounded()
	{
		return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.01f), new Vector2(0, -_distToGround + 0.5f), -_distToGround - 1f, _layer);
	}

	void DetachAttach()
	{
		if(Input.GetButtonDown("DetachAttach"))
		{
			if (_detach == false && _switch == false)
			{
				_rigidbodyPlayer2.transform.parent = null;
				_rigidbodyPlayer2.bodyType = RigidbodyType2D.Dynamic;
				_rigidbodyPlayer = _rigidbodyPlayer2;
				_detach = !_detach;

			}

			else if (_detach == true && _switch == true)
			{
				_rigidbodyPlayer2.transform.position = new Vector2(transform.position.x - 1f, transform.position.y + 0.75f);
				_rigidbodyPlayer2.transform.SetParent(transform);
				_rigidbodyPlayer2.bodyType = RigidbodyType2D.Kinematic;
				_rigidbodyPlayer = _rigidbodyPlayer1;
				_detach = !_detach;
				_switch = false;
			}
		}
	}

	void SwitchPlayer()
	{

		if (Input.GetButton("Switch") && _delaySwitch >= _storeDelaySwitch && _detach == true)
		{
			_delaySwitch = 0;
			if (_switch == false)
			{
				_rigidbodyPlayer = _rigidbodyPlayer1;
				_switch = !_switch;
			}
			else if (_switch == true)
			{
				_rigidbodyPlayer = _rigidbodyPlayer2;
				_switch = !_switch;
			}
		}
	}

	#endregion

	#region Coroutine
	IEnumerator Jumping()
	{
		while (true)
		{
			if (_isGrounded && Input.GetButtonDown("Jump"))
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

	#endregion
}
