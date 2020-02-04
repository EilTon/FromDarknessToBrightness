using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	public Animator _lichen;
	public Animator _trilo;
    
	public void SetSpeedDuo(float speed)
	{
		speed = Mathf.Abs(speed);
		_lichen.SetFloat("Speed_Duo_Lichen",speed);
		_trilo.SetFloat("Speed_Duo_Trilo", speed);
	}
}
