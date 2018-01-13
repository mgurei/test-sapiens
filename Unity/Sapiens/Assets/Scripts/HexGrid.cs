using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
	// Chunk parameters
	public HexGridChunk chunkPrefab;
	HexGridChunk[] chunks;
	int cellCountX, cellCountZ;
	public int chunkCountX = 4, chunkCountZ = 3;

	// Colors
	public Color defaultColor = Color.white;

	// My elevation step
	float elevation_step = 1f;

	// Simple bioma 
	float height_plain = 1f;
	float height_grassland = 2f;
	float height_mountain = 3f;
	float height_snow = 4f;

	// Assosiated color values
	Color color_water = Color.white;
	Color color_plain = Color.yellow;
	Color color_grassland = Color.green;
	Color color_mountain = Color.gray;
	Color color_snow = Color.white;

	// Models and labeling
	public HexCell cellPrefab;
	public Text cellLabelPrefab;
		
	// All cells
	HexCell[] cells;

	// Noise's texture
	public Texture2D noiseSource;


	/*************************
	 * 	UNITY FUNCTIONS
	 /**************************/

	// Awake method
	void Awake()
	{
		// Init
		HexMetrics.noiseSource = noiseSource;
					
		cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		CreateChunks();
		CreateCells();

	}

	void CreateChunks () {
		chunks = new HexGridChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++) {
			for (int x = 0; x < chunkCountX; x++) {
				HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
			}
		}
	}

	void CreateCells() {
		cells = new HexCell[cellCountZ * cellCountX];

		// Loop for cell creation
		for (int z = 0, i = 0; z < cellCountZ; z++)		{
			for (int x = 0; x < cellCountX; x++)	{
				CreateCell(x, z, i++);
			}
		}
	}
			
	// Update is called once per frame
	void Update()	{
		System.Threading.Thread.Sleep(50);

		if (Input.GetMouseButton(0))	{
			HandleInput(0);
		} else if (Input.GetMouseButton(1))	{
			HandleInput(1);

		}	
	}

	// 
	void OnEnable () {
		HexMetrics.noiseSource = noiseSource;
	}
			
	// Color tile based on the cell height
	void Colorize(HexCell[] cells) {
		foreach (HexCell cell in cells)
			{
				if (cell.Elevation >= 0 && cell.Elevation < height_plain)
					cell.color = color_water;
				if (cell.Elevation >= height_plain && cell.Elevation < height_grassland)
					cell.color = color_plain;
				if (cell.Elevation >= height_grassland && cell.Elevation < height_mountain)
					cell.color = color_grassland;
				if (cell.Elevation >= height_mountain && cell.Elevation < height_snow)
					cell.color = color_mountain;
				if (cell.Elevation >= height_snow)
					cell.color = color_snow;
			}
	}

	// Create cell at position X, Z
	void CreateCell (int x, int z, int i) 
	{
		// Actual creation
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0;
		position.z = z * (HexMetrics.outerRadius * 1.5f);
		
		// Generate and add properties;
		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
//		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = defaultColor;
		cell.name = "HexTile: " + cell.coordinates.ToString();

		// Assigning Neighbors: Oppesite directions are calculated in setNeighbor
		// WEST
		if (x > 0) {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
					
		// SOUTHWEST AND SOUTHEAST
		if (z > 0) {
				if ((z & 1) == 0) {
					cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
					if (x > 0) {
							cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
					}
				}
				else {
					cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
						if (x < cellCountX - 1) {
							cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
						}
				}
		}

		// Label the cells
		Text label = Instantiate<Text>(cellLabelPrefab);
//		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.name = "Label: " + cell.coordinates.ToString();
		label.rectTransform.anchoredPosition = 
				new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();

		cell.uiRect = label.rectTransform;
		cell.Elevation = 0;

		AddCellToChunk(x, z, cell);
	}

	void AddCellToChunk(int x, int z, HexCell cell){
		int chunkX = x / HexMetrics.chunkSizeX;
		int chunkZ = z / HexMetrics.chunkSizeZ;
		HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

		int localX = x - chunkX * HexMetrics.chunkSizeX;
		int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
		chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
	}

	// Handle the mouse inputs
	void HandleInput(int flag) 
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

				if (Physics.Raycast(inputRay, out hit, Mathf.Infinity))	{
					EditCell(GetCell(hit.point), flag);
				}
		}

	// Edit cell accordingly properties
	void EditCell (HexCell cell, int flag) {
		if (flag == 0)
			cell.Elevation = SetElevation(cell.Elevation + elevation_step);
		if (flag == 1)
			cell.Elevation = SetElevation(cell.Elevation - elevation_step);
	}

	public HexCell GetCell (Vector3 position) {
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
		return cells[index];
	}

	public void ColorCell(Vector3 position, Color color) 
	{
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = color;
	}

	public int SetElevation (float elevation) {
		return (int)elevation;
	}
			
}
