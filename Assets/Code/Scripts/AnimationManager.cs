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
		_trilo.SetLayerWeight(0, 0);
		_trilo.SetLayerWeight(3, 1);
	}

	public void StartSpeedDuo()
	{
		_trilo.SetLayerWeight(3, 0);
		_lichen.SetLayerWeight(2, 1);
		_trilo.SetLayerWeight(2, 1);
	}

	public void SetCourbe(float courbe)
	{
		_lichen.SetLayerWeight(4, courbe);
		_trilo.SetLayerWeight(1, courbe);
	}

	public void SetDuoJump()
	{
		//stop speed
		_lichen.SetLayerWeight(2, 0);
		_trilo.SetLayerWeight(0, 0);
		//start jump
		_lichen.SetLayerWeight(3, 1);
		_trilo.SetLayerWeight(2, 1);
		//Enable jump
		_lichen.SetBool("Jump_Duo_Lichen",true);
<<<<<<< HEAD
		_lichen.SetBool("Landing_Duo_Lichen", true);
		_trilo.SetTrigger("Jump_Duo_Trilo");
		_trilo.SetBool("Landing_Duo_Trilo", true);
	}

	public void StopDuoJump()
	{
		//stop speed
		_lichen.SetLayerWeight(2, 1);
		_trilo.SetLayerWeight(0, 1);
		//start jump
		_lichen.SetLayerWeight(3, 0);
		_trilo.SetLayerWeight(2, 0);
		//Enable jump
		_lichen.SetBool("Jump_Duo_Lichen", false);
		_lichen.SetBool("Landing_Duo_Lichen", false);
		_trilo.SetTrigger("Jump_Duo_Trilo");
		_trilo.SetBool("Landing_Duo_Trilo", false);
	}
=======
		//_lichen.SetBool("Landing_Duo_Lichen", true);
		_trilo.SetBool("Jump_Duo_Trilo",true);
		//_trilo.SetBool("Landing_Duo_Trilo", true);
	}

	public void SetDuoLanding()
	{
		_lichen.SetBool("Jump_Duo_Lichen", false);
		_trilo.SetBool("Jump_Duo_Trilo", false);
		_lichen.SetBool("Landing_Duo_Lichen", true);
		_trilo.SetBool("Landing_Duo_Trilo", true);
	}

	public void StopDuoLanding()
	{
		_lichen.SetBool("Landing_Duo_Lichen", false);
		_trilo.SetBool("Landing_Duo_Trilo", false);
		//start speed
		_lichen.SetLayerWeight(2, 1);
		_trilo.SetLayerWeight(0, 1);
		//stop jump
		_lichen.SetLayerWeight(3, 0);
		_trilo.SetLayerWeight(2, 0);
	}

>>>>>>> origin/IntegrationAnimations
}
