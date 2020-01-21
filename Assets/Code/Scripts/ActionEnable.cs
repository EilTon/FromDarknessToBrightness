using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnable : MonoBehaviour
{
	private bool _isTranslateHorizontal = false;
	private bool _isTranslateVertical = false;
	private bool _isOpenGate = false;
	private bool _isGrowing = false;
	private bool _isStreching = false;

	private void Update()
	{
		
	}

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
}
