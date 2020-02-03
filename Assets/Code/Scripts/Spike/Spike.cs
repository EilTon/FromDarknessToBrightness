using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	public PlayerController _resetPlayer;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player" || collision.tag == "ReflectPlayer")
		{
			_resetPlayer.ResetPlayer();
		}
	}
}
