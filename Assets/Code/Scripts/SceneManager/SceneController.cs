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

	private void Start()
	{
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
		yield return new WaitForSeconds(_fade._duration);
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
		yield return null;
	}
}
