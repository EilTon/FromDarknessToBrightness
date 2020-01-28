using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
	public string _sceneName;
	public Image _canvas;

	private FadeInFadeOut _fade;
	private float _time;

	private void Start()
	{
		_canvas.canvasRenderer.SetAlpha(0.0f);
		_fade = FindObjectOfType<FadeInFadeOut>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(ChangeScene());
			
		}
	}

	IEnumerator ChangeScene()
	{
		_fade.FadeIn();
		while(_time<_fade._duration)
		{
			_time += Time.deltaTime;
		}
		//SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
		yield return null;
	}
}
