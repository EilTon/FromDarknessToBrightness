using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
	private GameObject _buttonSelected;
	private MenuController _menuController;
	private void Start()
	{
		_menuController = FindObjectOfType<MenuController>();
	}

	void Update()
    {
		_buttonSelected = _menuController.GetButton();
        if(Input.GetButtonDown("Submit"))
		{
			if(_buttonSelected.tag == "Start")
			{
				StartGame();
			}
			else if(_buttonSelected.tag == "Quit")
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
