using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{

    void Update()
    {
        if(Input.GetButtonDown("Submit"))
		{
			if(gameObject.tag == "Start")
			{
				StartGame();
			}
			else if(gameObject.tag == "Quit")
			{
				QuitGame();
			}
		}
    }

	public void StartGame()
	{
		Debug.Log("Test start");
	}

	public void QuitGame()
	{
		Debug.Log("test quit");
		Application.Quit();
	}
}
