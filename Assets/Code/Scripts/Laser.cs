﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	LineRenderer _lineRenderer;
	Ray2D _ray;
	RaycastHit2D _hit;

	public float _rayDistance;
	public LayerMask _layer;

	Vector3[] _positions;

	/*Camera _camera;*/

	private void Awake()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		/*_camera = Camera.main;*/
	}

	private void Update()
	{
		_ray = new Ray2D(transform.position, transform.up/*(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*/);
		_positions = CastLaser(_ray, ref _hit, _rayDistance);
		_lineRenderer.positionCount = _positions.Length;
		_lineRenderer.SetPositions(_positions);


	}

	Vector3[] CastLaser(Ray2D ray, ref RaycastHit2D _hit, float distance = 10)
	{
		List<Vector3> positions = new List<Vector3>();

		Vector3 lastPosition = ray.origin;
		Vector3 lastDirection = ray.direction;

		float laserLength = 0;


		positions.Add(lastPosition);

		while (positions.Count < 100)
		{
			_hit = Physics2D.Raycast(lastPosition, lastDirection, Mathf.Clamp(distance - laserLength, 0, distance), _layer);
			if (_hit)
			{
				positions.Add(_hit.point);
				switch(_hit.collider.tag)
				{
					case "Player":
						Destroy(_hit.collider.gameObject);
						break;
					case "Reflect":
						laserLength += _hit.distance;
						lastPosition = _hit.point + _hit.normal * 0.01f;
						lastDirection = Vector3.Reflect(lastDirection, _hit.normal);
						break;
					case "Fear":
						Debug.Log("Test Fear");
						break;

					case "Enable":
						_hit.collider.GetComponent<Enable>()._Action.Invoke();
						break;

					case "Burn":
						_hit.collider.GetComponent<BurnObject>().SetIsHit();
						break;

					default:
						break;
				}
			}
			else
			{
				positions.Add(lastPosition + lastDirection * (distance - laserLength));
				break;
			}
		}
		return positions.ToArray();
	}

}
