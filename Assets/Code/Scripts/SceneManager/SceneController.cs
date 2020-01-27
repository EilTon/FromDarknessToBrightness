using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public List<string> _scenesNames;
	private static int _sceneInt=0;
	Scene[] _testScene;

	private void Start()
	{
		_testScene = SceneManager.GetAllScenes();
		foreach(var scene in _testScene)
		{
			//SceneManager.UnloadSceneAsync(scene);
		}
		//SceneManager.sceneUnloaded
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			_testScene = SceneManager.GetAllScenes();
			Debug.Log(_testScene.Length);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("EndLevel"))
		{
			_sceneInt++;
			SceneManager.LoadScene(_scenesNames[_sceneInt], LoadSceneMode.Single);
		}
	}
}
