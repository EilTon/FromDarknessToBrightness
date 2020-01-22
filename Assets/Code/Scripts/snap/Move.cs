using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public Vector2 Destination;
	public float Speed;
	private bool IsMoving=false;

    // Update is called once per frame
    void Update()
    {
        if(IsMoving)
		{
			Debug.Log("is moving");
		}
    }

	public void SetMove()
	{
		IsMoving = true;
	}
}
