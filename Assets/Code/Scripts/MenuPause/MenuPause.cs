using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
	#region Declarations public

	#endregion

	#region Declarations private
	private bool _isPause = false;
	private PlayerController _playerController;
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
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		PauseGame();
		#endregion

		#region Timer

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
		if(Input.GetButtonDown("Pause"))
		{
			_isPause = !_isPause;
			if (_isPause)
			{
				_playerController.SetFreeze(true);
				Time.timeScale = 0;
			}
			else
			{
				_playerController.SetFreeze(false);
				Time.timeScale = 1;
			}
			
		}
	}
	#endregion

	#region Coroutine

	#endregion
}
