using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Execute in editor
[ExecuteInEditMode]

// Required components
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class TGMap : MonoBehaviour {


	// Size of the map
	public int distance = 50;
	public float tileSize = 1.0f;

	public Texture2D terrainTiles;
	public int tileResolution = 16;

	// Use this for initialization
	void Start () {	
		BuildSquareMash ();
	}


	Color[][] ChopUpTiles() {
		int numTilesRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;

		Color[][] tiles = new Color[numTilesRow * numRows][];

		for(int y = 0; y < numRows; y++) {
			for(int x = 0; x < numTilesRow; x++) {
				tiles [y*numTilesRow + x*numRows] = 
					terrainTiles.GetPixels (
						x * tileResolution, 
						y * tileResolution, 
						tileResolution, 
						tileResolution);
			}
		}
			
		return tiles;
	}

	void BuildSquareTexture() {
		DTileMap map = new DTileMap (distance, distance);

		int texWidth = distance * tileResolution;
		int texHeight = distance * tileResolution;
		Texture2D texture = new Texture2D (texWidth, texHeight);

		Color[][] tiles = ChopUpTiles ();

		for (int z = 0; z < distance; z++) {
			for (int x = 0; x < distance; x++) {
				Color[] p = tiles [map.GetTileAt (x, z)];
				texture.SetPixels (
					x * tileResolution,
					z * tileResolution,
					tileResolution,
					tileResolution,
					p);
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply ();

		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		mesh_renderer.sharedMaterials [0].mainTexture = texture;
	}

	public void BuildSquareMash() {

		int size_x = distance;
		int size_z = distance;

		// Numbers and sizes
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;

		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[numTris * 3];


		int x, z;
		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertices[z * vsize_x + x] = new Vector3( 
					x * tileSize, 
					0, 
					- z * tileSize);
				
				normals [z * vsize_x + x] = Vector3.up;

				uv [z * vsize_x + x] = new Vector2 (
					(float)x / size_x, 
					1f - (float)z / size_z);
			}
		}

		for (z = 0; z < size_z; z++) {
			for (x = 0; x < size_x; x++) {
				int triOffset = (z * size_x + x) * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 1;

				triangles[triOffset + 3] = z * vsize_x + x + 0;
				triangles[triOffset + 5] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 4] = z * vsize_x + x + 1;

			}
		}


		// Create a new Mesh and populate the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		// Assign out mask to the filter/render/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
//		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		// Applyign mesh
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;

		// Building up the texture
		BuildSquareTexture ();
	}
}
