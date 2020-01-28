using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public string _scenesName;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EndLevel"))
		{
			SceneManager.LoadScene(_scenesName, LoadSceneMode.Single);
		}
	}
}
