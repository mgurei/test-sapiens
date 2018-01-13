using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	public int numColumns = 60;
	public int numRows = 30;

	// Use this for initialization
	void Start () {
		GenerateMap();
	}

	public GameObject HexPrefab;

	public Mesh MeshWater;
	public Mesh MeshFlat;
	public Mesh MeshHill;
	public Mesh MeshMountain;

	public Material MatOcean;
	public Material MatPlains;
	public Material MatGrassland;
	public Material MatMountain;

	private Hex[,] hexes;
	private Dictionary<Hex, GameObject> hexToGameOnjectMap;

	// TODO: Connect with other one
	public bool allowWrapEastWest = true;
	public bool allowWrapNorthSouth = true;

	public Hex GetHexAt(int x, int y) 
	{
		if(hexes == null) 
		{
			throw new UnityException("Hexes array not instantiated.");
			return null;
		}

		if (allowWrapEastWest)
			x = (x + numRows) % numRows;
		if (allowWrapNorthSouth)
			y = (y + numColumns) % numColumns;
			
		try {
			Debug.Log("GetHexAt: " + x + "," + y);
			return hexes[x, y];
		}
	catch
		{
			Debug.LogError("GetHexAt: " + x + "," + y);
			return null;
		}
	}


	virtual public void GenerateMap() {
		// Generates totolly random map

		hexes = new Hex[numColumns, numRows];	
		hexToGameOnjectMap = new Dictionary<Hex, GameObject>();

		for (int column = 0; column < numColumns; column++)
			{	
				for (int row = 0; row < numRows; row++)
					{

						// Instantiate an Hex
						Hex h = new Hex(column, row);
						h.Elevation = -0.5f;

						hexes[column, row] = h;

						Vector3 pos = h.PositionFromCamera(
					              Camera.main.transform.position,
								  numRows,
								  numColumns
							);

						// Instantiate
						GameObject hexGO = (GameObject)Instantiate(
				                   HexPrefab,
				                   pos,
				                   Quaternion.identity,
				                   this.transform
							);
					
						hexToGameOnjectMap[h] = hexGO;

						hexGO.GetComponent<HexComponent>().Hex = h;
						hexGO.GetComponent<HexComponent>().HexMap = this;
						hexGO.name = string.Format("HEX: {0},{1}", column, row);

						hexGO.GetComponentInChildren<TextMesh>().text = 
								string.Format("{0},{1}", column, row);


						// Render objects
						MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
						mr.material = MatOcean;

						MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();
						mf.mesh = MeshWater;
					}
			}

		// StaticBatchingUtility.Combine(this.gameObject);
		UpdateHexVisuals();
	}


	public void UpdateHexVisuals() {

		for (int column = 0; column < numColumns; column++)
		{	
			for (int row = 0; row < numRows; row++)
			{
				// Render objects
				Hex h = hexes[column, row];
				GameObject hexGO = hexToGameOnjectMap[h];

				MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
				MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();

					if (h.Elevation >= 0)
					{
						mr.material = MatGrassland;

					} 
					else
					{
						mr.material = MatOcean;

					}
				mf.mesh = MeshWater;
			}
		}
	}

	public Hex[] GetHexesWithinRangeOf(Hex centerHex, int range)
		{
			List<Hex> results = new List<Hex>();

			for (int dx = -range; dx < range-1; dx++)
				{
					for (int dy = Mathf.Max(-range+1, -dx-range); dy < Mathf.Min(range, -dx+range-1); dy++)
						{
							results.Add( GetHexAt(centerHex.Q + dx, centerHex.R + dy) );
						}
				}

			return results.ToArray();
		}
	

}
