using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorParticle : MonoBehaviour
{
	public float _maxIntensity;
	public float _minIntensity;

	private bool _isHit = false;
	private ParticleSystem _reflect;

	void Start()
	{
		_reflect = GetComponentInChildren<ParticleSystem>();
	}
	private void Update()
	{
		if(_isHit == false)
		{
			MinIntensity();
		}
	}

	void SetMirrorIntense(float intensity)
	{
		_reflect.startSpeed = intensity;
	}

	public void MaxIntensity()
	{
		SetMirrorIntense(_maxIntensity);
	}

	public void MinIntensity()
	{
		SetMirrorIntense(_minIntensity);
	}

	public void SetIsHit(bool isHit)
	{
		_isHit = isHit;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		MaxIntensity();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		MaxIntensity();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("test");
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		Debug.Log("test");
	}


}
