using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap {

	override public void GenerateMap(){
		
		// Make Ocean
		base.GenerateMap();

		// Make elevate land
		ElevateArea(0,0,5);
		ElevateArea(10,8,5);


		// Add lumpiness Perlin Noise?

		//Set mesh for mountain/hill/flat/water based on height

		//Simulate rainfall/moisture

		// Update graphics
		UpdateHexVisuals();

	}

	void ElevateArea
		(int q, int r, int radius) 
	{
		Hex centerHex = GetHexAt(q, r);

		Hex[] areaHexes = GetHexesWithinRangeOf(centerHex, radius);

		foreach (Hex h in areaHexes)
		{
			h.Elevation = 1f;
		}
	}
}
