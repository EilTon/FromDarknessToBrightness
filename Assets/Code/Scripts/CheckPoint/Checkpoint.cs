using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			FindObjectOfType<ParticlesCrystal>().transform.position = new Vector2(transform.position.x,transform.position.y - 0.5f);
			FindObjectOfType<ParticlesCrystal>().SetEmissionCrystal();
			FindObjectOfType<PlayerController>().SetResetPosition(transform.position);
		}
	}
}
