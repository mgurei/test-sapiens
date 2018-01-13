//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//[RequireComponent(typeof(TileMap))]
//
//public class TileMapMouse : MonoBehaviour {
//
//	public Transform tileSelection;
//
//	TileMap _tileMap;
//
//	Vector3 currentTileCoord;
//
//	void Start() {
//		_tileMap = GetComponent<TileMap> ();
//	}
//
//	// Update is called once per frame
//	void Update () {
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit hitInfo;
//
//		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
//			int x = Mathf.FloorToInt (hitInfo.point.x / _tileMap.tileSize);
//			int z = Mathf.FloorToInt (hitInfo.point.z / _tileMap.tileSize);
//			Debug.Log ("Tile: " + x + ", " + z);
//
//			currentTileCoord.x = x;
//			currentTileCoord.z = z;
//
//			tileSelection.transform.position = currentTileCoord*_tileMap.tileSize;
//
//		} else {
//
//		}
//
//	}
//}
