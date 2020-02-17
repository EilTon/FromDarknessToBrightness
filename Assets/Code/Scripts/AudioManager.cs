using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] _steps;
	public AudioClip[] _jump;
	public AudioClip[] _landing;
	public AudioClip _burning;
	public AudioClip _chimeLight;
	public AudioClip _cristalActive;
	public AudioClip _fungiGrowing;
	public AudioClip _rockFall;
	public AudioClip _wobblingWall;

	private AudioSource _playerAudio;

	private void Start()
	{
		_playerAudio = GetComponent<AudioSource>();
	}

	public void Step()
	{
		int random = Random.Range(0, _steps.Length);
		//_playerAudio.clip = _lichenSteps[random];
		_playerAudio.PlayOneShot(_steps[random]);
	}

	public void Jump()
	{
		int random = Random.Range(0, _jump.Length);
		//_playerAudio.clip = _lichenSteps[random];
		_playerAudio.PlayOneShot(_jump[random]);
	}

	public void Landing()
	{
		int random = Random.Range(0, _landing.Length);
		//_playerAudio.clip = _lichenSteps[random];
		_playerAudio.PlayOneShot(_landing[random]);
	}

	public void PlayBurning()
	{
		_playerAudio.PlayOneShot(_burning);
	}

	public void PlayChimeLight()
	{
		_playerAudio.PlayOneShot(_chimeLight);
	}

	public void PlayCristal()
	{
		_playerAudio.PlayOneShot(_cristalActive);
	}

	public void PlayFungi()
	{
		_playerAudio.PlayOneShot(_fungiGrowing);
	}

	public void PlayRockFall()
	{
		_playerAudio.PlayOneShot(_rockFall);
	}

	public void PlayWobblingFall()
	{
		_playerAudio.PlayOneShot(_wobblingWall);
	}
}
