using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(TGMap))]
public class TipeMapInspector : Editor {
		
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		if (GUILayout.Button ("Regenerate")) {
			TGMap tileMap = (TGMap)target;
			tileMap.BuildSquareMash();
		}
	}

}
