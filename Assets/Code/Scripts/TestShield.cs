using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShield : MonoBehaviour
{
	public int numberOfSides;
	public float polygonRadius;
	public float _offsetX;
	public float _offsetY;
	private Vector2 polygonCenter;
	[SerializeField]
	public List<Vector2> _positions;


	void Update()
	{
		polygonCenter = new Vector2(transform.position.x + _offsetX, transform.position.y + _offsetY);
		_positions = DebugDrawPolygon(polygonCenter, polygonRadius, numberOfSides);
	}

	// Draw a polygon in the XY plane with a specfied position, number of sides
	// and radius.
	List<Vector2> DebugDrawPolygon(Vector2 center, float radius, int numSides)
	{
		List<Vector2> positions = new List<Vector2>();
		// The corner that is used to start the polygon (parallel to the X axis).
		Vector2 startCorner = new Vector2(radius, 0) + center;

		// The "previous" corner point, initialised to the starting corner.
		Vector2 previousCorner = startCorner;

		// For each corner after the starting corner...
		for (int i = 1; i < numSides; i++)
		{
			// Calculate the angle of the corner in radians.
			float cornerAngle = (2f * Mathf.PI / (float)numSides * i)/2f;

			// Get the X and Y coordinates of the corner point.
			Vector2 currentCorner = new Vector2(Mathf.Cos(cornerAngle) * radius, Mathf.Sin(cornerAngle) * radius) + center;
			positions.Add(currentCorner);
			// Draw a side of the polygon by connecting the current corner to the previous one.
			Debug.DrawLine(currentCorner, previousCorner);

			// Having used the current corner, it now becomes the previous corner.
			previousCorner = currentCorner;
		}

		// Draw the final side by connecting the last corner to the starting corner.
		Debug.DrawLine(startCorner, previousCorner);
		return positions;
	}

}
