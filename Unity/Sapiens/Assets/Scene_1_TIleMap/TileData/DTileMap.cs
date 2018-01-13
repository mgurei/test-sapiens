//
//  EmptyClass.cs
//
//  Author: mihaig 
//  Date: x
// 	Time: x
//
// Description:
// This class holds methods needed to generate the tile map of the world.
// This is a randomized process atm.
// 

using UnityEngine;
using System;
public class DTileMap {

	// Map size 
	int size_x;
	int size_y;

	int[,] map_data;

	TDTile tiles;

	// DTileMap
	// Constructor by width and height of the map
	// Standard method is the construction of an hexagon shaped map
	public DTileMap(int width, int height) {
		this.size_x = width;
		this.size_y = height;

		// Initialization 
		map_data = GenerateBasicHex(width);
	}


	/**************************************
	 * Map generation functions
	 ***************************************/



	public int[,] GenerateBasicHex(int diameter) {
	
		int[,] data = SimpleVoid (TDTile.types.TILE_VOID);

		Hexagon hex = CreateHexagon (diameter);

		for (int x = 0; x < size_x; x++) {
			for (int y = 0; y < size_y; y++) {
				Vector3 point = new Vector3 (x, 0, y);
				if (IsPointInPolygon(hex.polygon, point)) {
					data [x, y] = (int)TDTile.types.TILE_OCEAN; 
				}
			}
		}


		return data;
	}

	public int[,] RandomMap(int num) {

		int[,] data = new int[size_x, size_y];

		for (int x = 0; x < size_x; x++) {
			for (int y = 0; y < size_y; y++) {
				data[x,y] = UnityEngine.Random.Range(0,num);
			}
		}

		return data;
	}


	public int[,] SimpleVoid(TDTile.types type) {

		int[,] data = new int[size_x, size_y];

		for (int x = 0; x < size_x; x++) {
			for (int y = 0; y < size_y; y++) {
				data[x,y] = (int)type;
			}
		}

		return data;
	}

	/**************************************
	 * Helper functions
	 **************************************/

	public int GetTileAt(int x, int y) {
		return map_data [x, y];
	}



	/**************************************
	 * Hexagon related functions
	 **************************************/

	// Hexagon structure 
	protected struct Hexagon {
		public float outer_radius;
		public float inner_radius;

		// Center coordinates
		public int center_x;
		public int center_z;

		public Vector3[] polygon;

	};

	Hexagon CreateHexagon(int diameter) {
		Hexagon hex;

		hex.outer_radius = diameter / 2;
		hex.inner_radius = Mathf.Sqrt (3) / 2 * diameter / 2;

		Debug.Log ("Outer radius is: " + hex.outer_radius);
		Debug.Log ("Inner radius is: " + hex.inner_radius);

		// center coordinates
		hex.center_x = diameter / 2;
		hex.center_z = diameter / 2;

		Debug.Log ("Center is: " + hex.center_x + ", " + hex.center_z);

		// Corners
		hex.polygon = new Vector3[6];

		for (int i = 0; i < 6; i++) {
			hex.polygon [i] = hex_corners (hex.center_x, hex.center_z, hex.outer_radius, i);
			Debug.Log ("Corner[" + i + "] = " + hex.polygon [i].x + ", " + hex.polygon [i].z);
		}

		return hex;
	}

	Vector3 hex_corners(int center_x, int center_z, float size, int i) {
		int angle_deg = 60 * i + 30;
		float angle_rad = ((float)angle_deg / 180f) * Mathf.PI;

		return new Vector3	(
			center_x + size * Mathf.Cos (angle_rad),
			0,
			center_z + size * Mathf.Sin (angle_rad));
	}
		
	public bool IsPointInPolygon(Vector3[] polygon, Vector3 point) {

		int polygonLength = polygon.Length, i=0;
		bool inside = false;
		// x, y for tested point.
		float pointX = point.x;
		float pointZ = point.z;
		// start / end point for the current polygon segment.
		float startX, startZ, endX, endZ;
		Vector3 endPoint = polygon[polygonLength-1];           
		endX = endPoint.x; 
		endZ = endPoint.z;
		while (i<polygonLength) {
			startX = endX;
			startZ = endZ;
			endPoint = polygon[i++];
			endX = endPoint.x;
			endZ = endPoint.z;
			//
			inside ^= ( endZ > pointZ ^ startZ > pointZ ) /* ? pointY inside [startY;endY] segment ? */
				&& /* if so, test if it is under the segment */
				( (pointX - endX) < (pointZ - endZ) * (startX - endX) / (startZ - endZ) ) ;
		}
		return inside;
	}
}
