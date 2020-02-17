using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BurnObject : MonoBehaviour
{
	#region Declarations public
	public float _timeToBurn;
	public Rigidbody2D _rigidbody2D;
	public Sprite _rendererParticlesFire;
	public AudioManager _audioManager;
	//public Material _rendererParticlesSmoke;
	//public ParticleSystem _fire;
	#endregion

	#region Declarations private
	private float _timerBurn;
	private bool _isHit = false;
	private bool _isBurn = false;
	private ParticleSystem _particles;
	private bool _isPlay = false;
	#endregion

	#region Declarations Event Args

	#endregion

	#region Declarations Event Handler

	#endregion

	#region Declarations Event Call

	#endregion

	#region Functions Unity
	private void Awake()
	{
		#region Initialize

		#endregion
	}

	private void Start()
	{
		#region Initialize
		_particles = GetComponent<ParticleSystem>();
		_particles.enableEmission = false;
		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		if (_isHit)
		{
			_particles.enableEmission = true;
		}
		else
		{
			_particles.enableEmission = false;
		}

		if (_isBurn)
		{
			//_fire.Play();
			//_fire.enableEmission = true;
			if(_isPlay == false)
			{
				_isPlay = true;
				_audioManager.PlayBurning();
			}
			_particles.textureSheetAnimation.SetSprite(0, _rendererParticlesFire);
			_timerBurn += Time.deltaTime;
			if (_timerBurn > _timeToBurn)
			{
				if (_rigidbody2D != null)
				{
					_audioManager.PlayRockFall();
					_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				}
				Destroy(gameObject);
			}
		}
		#endregion

		#region Timer

		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}
	#endregion

	#region Helper
	public void SetIsHit(bool boo)
	{
		_isHit = boo;
	}

	public void SetIsBurn()
	{
		_isBurn = true;
	}
	#endregion

	#region Coroutine

	#endregion
}
