using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnObject : MonoBehaviour
{
	public float _timeToBurn;
	private float _timerBurn;
	private bool _isHit = false;
    
    void Update()
    {
        if(_isHit)
		{
			_timerBurn += Time.deltaTime;
			if(_timerBurn>_timeToBurn)
			{
				Destroy(gameObject);
			}
		}
    }

	public void SetIsHit()
	{
		_isHit = true;
	}
}
