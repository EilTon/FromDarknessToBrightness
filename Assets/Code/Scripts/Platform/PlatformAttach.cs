using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
	private GameObject target = null;
	private Vector3 offset;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.gameObject.transform.parent = transform;
			target = collision.gameObject;
			offset = target.transform.position - transform.position;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.gameObject.transform.parent = null;
			target = null;
		}
	}

	void Update()
	{
		if (target != null)
		{
			target.transform.position = transform.position + offset;
		}

	}
}

