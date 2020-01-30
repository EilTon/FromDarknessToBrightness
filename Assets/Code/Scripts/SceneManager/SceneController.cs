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
			//collision.GetComponent<PlayerController>().enabled = false;
			Vector3 startPosition = new Vector3(collision.transform.position.x, collision.transform.position.y);
			Vector3 EndPosition = new Vector3(collision.transform.position.x + 10, collision.transform.position.y);
			StartCoroutine(MoveFromTo(collision.transform,startPosition, EndPosition,5));
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

	IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
	{
		float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
		float t = 0;
		while (t <= 1.0f)
		{
			t += step; // Goes from 0 to 1, incrementing by step each time
			objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
			yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
		}
		objectToMove.position = b;
	}
}
