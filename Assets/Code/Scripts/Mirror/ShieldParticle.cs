using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldParticle : MonoBehaviour
{
	private ParticleSystem _mirror;
	private bool _isHit = false;
    // Start is called before the first frame update
    void Start()
    {
		_mirror = GetComponent<ParticleSystem>();
		if(_mirror!= null)
		{
			_mirror.enableEmission = false;
		}
    }

	private void Update()
	{
		if (_isHit == false)
		{
			SetEmissionParticle(false);
		}
	}

	public void SetEmissionParticle(bool boo)
	{
		_mirror.enableEmission = boo;
	}

	public void SetIsHit(bool boo)
	{
		_isHit = boo;
	}
}
