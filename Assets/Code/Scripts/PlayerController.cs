using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Declarations public
	public float _speed;
	public float _jumpForce;
	public float _gravityScale;
	public float _limitAttach;
	public GameObject _player2;
	public float _delayDetach = 0.5f;
	public float _delaySwitch = 0.5f;
	#endregion

	#region Declarations private
	private CharacterController _characterController;
	private CharacterController _player1CharacterController;
	private CharacterController _player2CharacterController;
	private Vector3 _movement;
	private float _storeDelayDetach;
	private float _storeDelaySwitch;
	private bool _switch = false;
	private bool _detach = false;
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
		_characterController = GetComponent<CharacterController>();
		_player1CharacterController = GetComponent<CharacterController>();
		_player2CharacterController = _player2.GetComponent<CharacterController>();
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
		SwitchPlayer();
		DetachAttach();
		#endregion

		#region Timer
		if (_delayDetach <= _storeDelayDetach)
		{
			_delayDetach += Time.deltaTime;
		}
		if (_delaySwitch <= _storeDelaySwitch)
		{
			_delaySwitch += Time.deltaTime;
		}
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions
		DetachAttach();
		#endregion

		#region Timer

		#endregion
	}
	#endregion

	#region Helper
	void MovePlayer()
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
	void DetachAttach()
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

	void SwitchPlayer()
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
		
	}
	#endregion
}
