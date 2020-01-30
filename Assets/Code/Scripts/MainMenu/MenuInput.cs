using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
	public string _sceneToLoad;
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
		SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Single);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
