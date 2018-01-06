using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(TileMap))]
public class TipeMapInspector : Editor {
		
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		if (GUILayout.Button ("Regenerate")) {
			TileMap tileMap = (TileMap)target;
			tileMap.BuildMash ();
		}
	}

}
