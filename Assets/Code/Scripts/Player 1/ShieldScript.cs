using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
	#region Declarations public
	public int _numberOfSides;
	public float _polygonRadius;
	public float _offsetX;
	public float _offsetY;
	[SerializeField]
	public List<Vector2> _positions;
	#endregion

	#region Declarations private
	private Vector2 _polygonCenter;
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

		#endregion
	}

	private void Update()
	{
		#region Movement

		#endregion

		#region Actions
		_polygonCenter = new Vector2(transform.position.x + _offsetX, transform.position.y + _offsetY);
		_positions = DebugDrawPolygon(_polygonCenter, _polygonRadius, _numberOfSides);
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
			float cornerAngle = (2f * Mathf.PI / (float)numSides * i) / 2f;

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
	#endregion

	#region Coroutine

	#endregion

}
