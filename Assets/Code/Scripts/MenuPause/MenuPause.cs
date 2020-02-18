using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MenuController
{
	#region Declarations public

	#endregion

	#region Declarations private
	private bool _isPause = false;
	private PlayerController _playerController;
	public GameObject _pauseGUI;
	#endregion

	#region Declarations Event Args

	#endregion

	#region Declarations Event Handler

	#endregion

	#region Declarations Event Call

	#endregion

	#region Functions Unity
	private void Awake()
	{
		#region Initialize

		#endregion
	}

	private void Start()
	{
		#region Initialize
		_playerController = GetComponent<PlayerController>();
		_pauseGUI.SetActive(false);
		SetupMenu();
		#endregion
	}

	private void Update()
	{
		#region Movement
		if(_isPause == true)
		{
			NavigateMenu();
			InputPause();
		}
		#endregion

		#region Actions
		PauseGame();
		#endregion

		#region Timer
		Timer();
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}
	#endregion

	#region Helper
	void PauseGame()
	{
		if (Input.GetButtonDown("Pause"))
		{
			_isPause = !_isPause;
			if (_isPause)
			{
				_pauseGUI.SetActive(true);
				_playerController.SetFreeze(true);
				Time.timeScale = 0;
				
			}
			else
			{
				Resume();
			}

		}
	}

	void Resume()
	{
		_isPause = !_isPause;
		_pauseGUI.SetActive(false);
		_playerController.SetFreeze(false);
		Time.timeScale = 1;
	}

	void InputPause()
	{
		if (Input.GetButtonDown("Submit"))
		{
			switch (GetButton().tag)
			{
				case "Resume":
					Resume();
					break;

				case "ResetCheckpoint":
					Resume();
					_playerController.ResetPlayer("");
					break;

				case "QuitMainMenu":
					Resume();
					SceneManager.LoadScene("MainMenu");
					break;	
			}
		}
	}
	#endregion

	#region Coroutine

	#endregion
}
