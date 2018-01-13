// Hex.cs
// Author: mihaig
// The Hex class defines the grip position

using UnityEngine;

public class Hex {

	// Constructor
	public Hex(int q, int r) {
		this.Q = q;
		this.R = r;
		this.S = (-q + r);
	}
	
	// Components
	public readonly int Q;	//Colomn
	public readonly int R;	//Row
	public readonly int S;	// S = -(Q + R)

	// Data for map generation 
	public float Elevation;
	public float Moisture;

	static readonly float WIDTH_MULTIPLYER = Mathf.Sqrt(3) / 2;

	float radius = 1f;

	// TODO: Connect to other 
	public bool allowWrapEastWest = true;
	public bool allowWrapNorthSouth = true;

	// Return the world space position
	public Vector3 Position() {
	
		float height = HexHeight();
		float width = HexWidth();

		float vert = HexVerticalSpacing();
		float horiz = HexHorizontalSpacing();
		
		return new Vector3(
			horiz * (this.Q + this.R/2f),
			0,
			vert * this.R
		);		
	}

	public float HexHeight() {
		return radius * 2;
	}

	public float HexWidth() {
		return WIDTH_MULTIPLYER * HexHeight();	
	}

	public float HexVerticalSpacing() {
		return HexHeight() * 0.75f;
	}
	
	public float HexHorizontalSpacing() {
		return HexWidth();
	}


	public Vector3 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns) {

		float mapHeight = numRows * HexVerticalSpacing();
		float mapWidth = numColumns * HexHorizontalSpacing();
		
		Vector3 position = Position();

		if (allowWrapEastWest)
			{
				float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

				if (howManyWidthsFromCamera > 0)
					howManyWidthsFromCamera += 0.5f;
				else
					howManyWidthsFromCamera -= 0.5f;
		
				int howManyWidthToFix = (int)howManyWidthsFromCamera;

					position.x -= howManyWidthToFix * mapWidth;
			}

		if (allowWrapNorthSouth)
			{
				float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;
						
				if (howManyHeightsFromCamera > 0)
					howManyHeightsFromCamera += 0.5f;
				else
					howManyHeightsFromCamera -= 0.5f;

				int howManyHeightsToFix = (int)howManyHeightsFromCamera;

				position.z -= howManyHeightsToFix * mapHeight;
			}

		
		return position;

	}
}