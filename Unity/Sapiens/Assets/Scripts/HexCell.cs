using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {

	[SerializeField]
	HexCell[] neighbors = new HexCell[6];

	public HexCoordinates coordinates;
	public RectTransform uiRect;

	public HexGridChunk chunk;

	public int Elevation {
		get { return elevation;	}
		set { 
			
			if (elevation == value) {
				return;
			}

			elevation = value;
			Vector3 position = transform.localPosition;
			position.y = value * HexMetrics.elevationStep;
			position.y +=
				(HexMetrics.SampleNoise(position).y * 2f - 1f) *
				HexMetrics.elevationPerturbStrength;
			transform.localPosition = position;
				
			Vector3 uiPosition = uiRect.localPosition;
			uiPosition.z =  -position.y;
			uiRect.localPosition = uiPosition;
			// refresh chunk
			 Refresh();
		}
	}


	int elevation = int.MinValue;


	public Color Color {
			get {
				return color;
			}
			set {
					if (color == value) {
						return;
					}
				color = value;
				Refresh();
			}
	}

	public Color color;

	void Refresh () {
		if (chunk)
		{
			chunk.Refresh();

			for (int i = 0; i < neighbors.Length; i++)
				{
					HexCell neighbor = neighbors[i];

					if (neighbor != null && neighbor.chunk != chunk)
						neighbor.chunk.Refresh();
				}
		}
	}

	public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}
			
	public Vector3 Position {
			get {
				return transform.localPosition;
			}
	}
	
	public HexCell[] GetNeighbors (HexDirection[] directions) {
	
		foreach (HexDirection direction in directions) {
			neighbors[(int)direction] = GetNeighbor(direction);
		}

		return neighbors;
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}

	public HexEdgeType GetEdgeType (HexDirection direction) {
		return HexMetrics.GetEdgeType(
			elevation, neighbors[(int)direction].elevation
		);
	}

	public HexEdgeType GetEdgeType (HexCell otherCell) {
		return HexMetrics.GetEdgeType(
			elevation, otherCell.elevation
		);
	}


}
