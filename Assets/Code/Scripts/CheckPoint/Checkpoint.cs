using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			FindObjectOfType<ParticlesCrystal>().transform.position = transform.position;
			FindObjectOfType<ParticlesCrystal>().SetEmissionCrystal();
			FindObjectOfType<PlayerController>().SetResetPosition(transform.position);
		}
	}
}
