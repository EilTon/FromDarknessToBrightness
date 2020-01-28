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
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
		yield return null;
	}
}
