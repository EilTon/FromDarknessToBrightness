using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
	public Image _fade;
	public float _duration;

    public void FadeIn()
	{
		_fade.canvasRenderer.SetAlpha(0.0f);
		_fade.CrossFadeAlpha(1, _duration, false);
	}

	public void FadeOut()
	{
		_fade.canvasRenderer.SetAlpha(1.0f);
		_fade.CrossFadeAlpha(0, _duration, false);
	}
}
