using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	[SerializeField] private Vector2 _parallaxEffectMultiplier;
	[SerializeField] private bool _infiniteHorizontal;
	[SerializeField] private bool _infiniteVertical;

	public Transform _cameraTransform;
	private Vector3 _lastCameraPosition;
	private float _texturesUnitSizeX;
	private float _texturesUnitSizeY;

	// Start is called before the first frame update
	void Start()
    {
		_lastCameraPosition = _cameraTransform.position;
		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		_texturesUnitSizeX = texture.width / sprite.pixelsPerUnit;
		_texturesUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

	private void LateUpdate()
	{
		Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
		transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y);
		_lastCameraPosition = _cameraTransform.position;

		if (_infiniteHorizontal)
		{
			if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _texturesUnitSizeX)
			{
				float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _texturesUnitSizeX;
				transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
			}
		}
		
		if(_infiniteVertical)
		{
			if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _texturesUnitSizeY)
			{
				float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _texturesUnitSizeY;
				transform.position = new Vector3(_cameraTransform.position.y + offsetPositionY, transform.position.y);
			}
		}
		
	}
}
