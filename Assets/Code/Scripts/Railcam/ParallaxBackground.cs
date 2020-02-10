using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
	public float _parallaxEffect;
	public GameObject _camera;

	private float _lenght;
	private float _startPos;
	// Start is called before the first frame update
	void Start()
	{
		_startPos = transform.position.x;
		_lenght = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	// Update is called once per frame
	void Update()
	{
		float temp = (_camera.transform.position.x * (1 - _parallaxEffect));
		float distance = (_camera.transform.position.x * _parallaxEffect);
		transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

		if (temp>_startPos + _lenght)
		{
			_startPos += _lenght;
		}
		else if(temp<_startPos - _lenght)
		{
			_startPos -= _lenght;
		}
	}
}
