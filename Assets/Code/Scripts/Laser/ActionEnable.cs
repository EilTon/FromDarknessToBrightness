using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEnable : MonoBehaviour
{
	#region Declarations public

	#endregion

	#region Declarations private
	private bool _isTranslateHorizontal = false;
	private bool _isTranslateVertical = false;
	private bool _isOpenGate = false;
	private bool _isGrowing = false;
	private bool _isStreching = false;
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

		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isTranslateHorizontal)
		{
			Debug.Log("translate H");
		}
		else if (_isTranslateVertical)
		{
			Debug.Log("translate V");
		}
		else if (_isOpenGate)
		{
			Debug.Log("Gate open");
		}
		else if (_isGrowing)
		{
			Debug.Log("Growth");
		}
		else if (_isStreching)
		{
			Debug.Log("Strech");
		}
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
	public void TranslateHorizontalPlatform()
	{
		_isTranslateHorizontal = true;
	}

	public void TranslateVerticalPlatform()
	{
		_isTranslateVertical = true;
	}

	public void OpenGate()
	{
		_isOpenGate = true;
	}

	public void Strenching()
	{
		_isStreching = true;
	}

	public void Growing()
	{
		_isGrowing = true;
	}
	#endregion

	#region Coroutine

	#endregion
}
