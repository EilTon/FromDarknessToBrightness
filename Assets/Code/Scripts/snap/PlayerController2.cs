using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
	private Rigidbody2D _rigidbodyPlayer1;
	private Rigidbody2D _rigidbodyPlayer2;
	private Rigidbody2D _rigidbodyPlayer;
	private float _distToGround;
	private Vector3 _movement;
	public float _jumpTimeDelay;
	float _jumpTime;	

	public bool _isGrounded;
	public float _jumpImpulse;
	public float _jumpForce;
	public LayerMask _layer;
	public float _speed;
	public GameObject _player2;

	// Start is called before the first frame update
	void Start()
    {
		_rigidbodyPlayer1 = GetComponent<Rigidbody2D>();
		_rigidbodyPlayer2 = _player2.GetComponent<Rigidbody2D>();
		_rigidbodyPlayer = _rigidbodyPlayer1;
		_distToGround = GetComponent<Collider2D>().bounds.extents.y;
		StartCoroutine(Jumping());
	}

    // Update is called once per frame
    void Update()
    {
		_isGrounded = CheckIfGrounded();
		_movement = new Vector3(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, _rigidbodyPlayer.velocity.y);
	}

	void MovePlayer()
	{
		_rigidbodyPlayer.AddForce(_movement);
	}

	//void JumpPlayer()
	//{
	//	if(_isGrounded && Input.GetButtonUp("Jump"))
	//	{
	//		_rigidbodyPlayer.velocity = new Vector2(_rigidbodyPlayer.velocity.x, _minJump * _jumpForce);
	//	}
	//
	//	else if(_isGrounded && Input.GetButtonDown("Jump"))
	//	{
	//		_jumpTimeCounter = _jumpTime;
	//		_rigidbodyPlayer.velocity = new Vector2(_rigidbodyPlayer.velocity.x, 1 * _jumpForce);
	//	}
	//
	//	if (_isGrounded == false && Input.GetButton("Jump"))
	//	{
	//		if(_jumpTimeCounter>0)
	//		{
	//			_rigidbodyPlayer.velocity = new Vector2(_rigidbodyPlayer.velocity.x, 1 * _jumpForce);
	//			_jumpTimeCounter -= Time.deltaTime;
	//		}
	//	}
	//}

	IEnumerator Jumping()
	{
		while (true)
		{
			if (_isGrounded && Input.GetButtonDown("Jump"))
			{
				_jumpTime = Time.time;
				_rigidbodyPlayer.AddForce(Vector2.up * _jumpImpulse, ForceMode2D.Impulse);
				while (Input.GetButton("Jump") && Time.time<_jumpTime+_jumpTimeDelay)
				{
					_rigidbodyPlayer.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
					yield return new WaitForFixedUpdate();
				}

			}
			yield return null;
		}
	}


	bool CheckIfGrounded()
	{
		return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.01f), new Vector2(0, -_distToGround + 0.5f), -_distToGround - 1f, _layer);
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
}
