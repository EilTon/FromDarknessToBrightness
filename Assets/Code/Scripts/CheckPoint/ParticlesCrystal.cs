using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCrystal : MonoBehaviour
{
	private ParticleSystem _crystal;
	void Start()
	{
		_crystal=GetComponent<ParticleSystem>();
		_crystal.enableEmission = false;
	}

	public void SetEmissionCrystal()
	{
		if (_crystal.emission.enabled == false)
		{
			_crystal.enableEmission = true;
		}
	}
}
