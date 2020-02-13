using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorParticle : MonoBehaviour
{
	public float _maxIntensity;
	public float _minIntensity;
	[HideInInspector]
	public bool _isHit = false;
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


}
