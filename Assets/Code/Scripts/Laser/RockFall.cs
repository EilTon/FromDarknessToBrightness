using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RockFall : MonoBehaviour
{
	public AudioClip _rockFall;
	public LayerMask _layer;

	private AudioSource _audio;
	private float _distToGround;
	private bool _isPlay = false;
	void Start()
	{
		_distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
		_audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 3.00f), Vector2.down * 10f);
		if (CheckGround())
		{
			if (_isPlay == false)
			{
				_audio.PlayOneShot(_rockFall);
				_isPlay = true;
			}
		}
	}

	bool CheckGround()
	{
		return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 3.1f), new Vector2(0, -_distToGround + 0.01f), _distToGround - 1f, _layer);
	}
}
