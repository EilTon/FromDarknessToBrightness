using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DirectionStart
{
	Left,
	Right
}
public class StartScene : MonoBehaviour
{
	public GameObject _player;
	public DirectionStart _direction;

	void Start()
	{
		Vector3 startPosition;
		Vector3 endPosition;
		if (_direction == DirectionStart.Right)
		{
			startPosition = new Vector3(_player.transform.position.x, _player.transform.position.y);
			endPosition = new Vector3(_player.transform.position.x + 10, _player.transform.position.y);
		}
		else
		{
			startPosition = new Vector3(_player.transform.position.x, _player.transform.position.y);
			endPosition = new Vector3(_player.transform.position.x - 10, _player.transform.position.y);
		}

		StartCoroutine(MoveFromTo(_player.transform, startPosition, endPosition, 5));
		FindObjectOfType<FadeInFadeOut>().FadeOut();
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
