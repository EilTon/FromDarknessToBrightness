using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public struct Collisions
	{
		public bool top, bottom, left, right;
		public bool topBuffer, bottomBuffer, leftBuffer, rightBuffer;


		public void Reset()
		{
			top = bottom = left = right = false;
		}
	}

	#region Declarations public
	public float _speed;
	public float _jumpForce;
	public float _limitAttach;
	public float _limitMaxJump;
	public float _limitMinJump;
	public GameObject _player2;
	//public float _gravityScale;
	public float _delayDetach = 0.5f;
	public float _delaySwitch = 0.5f;
	#endregion

	#region Declarations private
	private Collisions _collisions;
	private CharacterController _characterController;
	private CharacterController _player1CharacterController;
	private CharacterController _player2CharacterController;
	private Rigidbody2D _rigidbodyPlayer1;
	private Rigidbody2D _rigidbodyPlayer2;
	private Rigidbody2D _rigidbodyPlayer;
	private Vector3 _movement;
	private float _storeDelayDetach;
	private float _storeDelaySwitch;
	private bool _switch = false;
	private bool _detach = false;
	private bool _isJumping = false;
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
		//_characterController = GetComponent<CharacterController>();
		//_player1CharacterController = GetComponent<CharacterController>();
		//_player2CharacterController = _player2.GetComponent<CharacterController>();
		_storeDelayDetach = _delayDetach;
		_storeDelaySwitch = _delaySwitch;
		#endregion
	}

	private void Update()
	{
		#region Movement
		MovePlayer();
		#endregion

		#region Actions
		//SwitchPlayer();
		//DetachAttach();
		#endregion

		#region Timer
		/*if (_delayDetach <= _storeDelayDetach)
		{
			_delayDetach += Time.deltaTime;
		}
		if (_delaySwitch <= _storeDelaySwitch)
		{
			_delaySwitch += Time.deltaTime;
		}*/
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement
		MovePlayer();
		JumpPlayer();
		#endregion

		#region Actions

		DetachAttach();
		SwitchPlayer();
		#endregion

		#region Timer
		if (_delaySwitch <= _storeDelaySwitch)
		{
			_delaySwitch += Time.fixedDeltaTime;
		}

		if (_delayDetach <= _storeDelayDetach)
		{
			_delayDetach += Time.deltaTime;
		}
		#endregion
	}
	#endregion

	#region Helper

	#region CharacterControllerComponent
	/*void MovePlayerCharacterController()
	{
		float yStore = _movement.y;
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		_movement = transform.right * moveHorizontal;
		_movement = _movement.normalized * _speed;
		_movement.y = yStore;
		if (_characterController.isGrounded)
		{
			_movement.y = 0f;
			if (Input.GetButton("Jump"))
			{
				_movement.y = _jumpForce;
			}
		}
		_movement.y = _movement.y + (Physics.gravity.y * _gravityScale * Time.deltaTime);
		_characterController.Move(_movement * Time.deltaTime);
	}
	void DetachAttachCharacterController()
	{
		if (Input.GetKey(KeyCode.A) && (_player2.transform.position.x >= transform.position.x - _limitAttach && _player2.transform.position.x <= transform.position.x + _limitAttach) && _characterController.isGrounded && _delayDetach >= _storeDelayDetach)
		{

			
			_delayDetach = 0;
			if (_detach == false && _switch == false)
			{
				_player2.transform.parent = null;
				_characterController = _player2CharacterController;
				_detach = !_detach;

			}

			else if (_detach == true && _switch == true)
			{
				_player2.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
				_player2.transform.SetParent(transform);
				_characterController = _player1CharacterController;
				_detach = !_detach;
				_switch = false;
			}
		}
	}
	void SwitchPlayerCharacterController()
	{

		if (Input.GetKey(KeyCode.E) && _detach == true && _characterController.isGrounded && _delaySwitch >= _storeDelaySwitch)
		{
			_delaySwitch = 0;
			if (_switch == false)
			{
				_characterController = _player1CharacterController;
				_switch = !_switch;
			}

			else if(_switch == true)
			{
				_characterController = _player2CharacterController;
				_switch = !_switch;
			}
		}
		
	}*/
	#endregion

	#region RigibodyComponent

	void MovePlayer()
	{
		_movement = new Vector3(Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime, _rigidbodyPlayer.velocity.y);
		_rigidbodyPlayer.AddForce(_movement);
	}

	void DetachAttach()
	{
		if (Input.GetButtonDown("DetachAttach") && (_player2.transform.position.x >= transform.position.x - _limitAttach && _player2.transform.position.x <= transform.position.x + _limitAttach) && _delayDetach >= _storeDelayDetach)
		{
			_delayDetach = 0;
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

		if (Input.GetButtonDown("Switch") && _delaySwitch >= _storeDelaySwitch && _detach == true)
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

	void JumpPlayer()
	{
		if (Input.GetButton("Jump"))
		{
			float LimitMax = _limitMaxJump * _jumpForce;
			float LimitMin = _limitMinJump * _jumpForce;
			Vector2 jump = Vector2.up * _jumpForce;
			if (_rigidbodyPlayer.transform.position.y < LimitMin)
			{
				_rigidbodyPlayer.AddForce(new Vector2(0,LimitMin * _jumpForce), ForceMode2D.Impulse);
			}
			else if(_rigidbodyPlayer.transform.position.y > LimitMin && _rigidbodyPlayer.transform.position.y <LimitMax)
			{

			}
			else if (_rigidbodyPlayer.transform.position.y >= LimitMax)
			{
				_rigidbodyPlayer.AddForce(new Vector2());
			}
		}
	}

	#endregion

	#endregion
}
