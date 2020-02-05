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
		_lichen.SetFloat("Speed_Duo_Lichen", speed);
		_trilo.SetFloat("Speed_Duo_Trilo", speed);
	}

	public void SetSpeedLichen(float speed)
	{
		speed = Mathf.Abs(speed);
		_lichen.SetFloat("Speed_Solo_Lichen", speed);
	}

	public void SetSpeedTrilo(float speed)
	{
		speed = Mathf.Abs(speed);
		_trilo.SetFloat("Speed_Solo_Trilo", speed);
	}

	public void StopSpeedDuo()
	{
		_lichen.SetLayerWeight(2, 0);
		_trilo.SetLayerWeight(2, 0);
	}

	public void StartSpeedDuo()
	{
		_lichen.SetLayerWeight(2, 1);
		_trilo.SetLayerWeight(2, 1);
	}

	public void SetCourbe(float courbe)
	{
		_lichen.SetLayerWeight(4, courbe);
		_trilo.SetLayerWeight(1, courbe);
	}
}
