using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

	Vector3 oldPosition;
	HexComponent[] hexes;

	// Use this for initialization
	void Start () {
		oldPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Code motion methods here possibly
		// 		WASD etc.
	

		CheckIfCameraMoved();
	}

	public void PanToHex (Hex hex) {
		// TODO: move camera to hex	
	}

	void CheckIfCameraMoved() {

		if (oldPosition != this.transform.position)
			{
				oldPosition = this.transform.position;

				if (hexes == null)
					hexes = GameObject.FindObjectsOfType<HexComponent>();

				foreach (HexComponent hex in hexes)
					{
						hex.UpdatePosition();
					}
			}
	}

}
