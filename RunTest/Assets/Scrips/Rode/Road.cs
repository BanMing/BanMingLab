using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

	private Dictionary<string, RoadItem> roadItems;
	private int index;

	public int Index { get; set; }

	void Start () {
		roadItems = new Dictionary<string, RoadItem> ();
		foreach (Transform item in transform) {
			roadItems.Add (item.name, item.GetComponent<RoadItem> ());
		}
	}

	public void SetRoadPos (Vector3 pos) {
		transform.localPosition = pos;
	}

	public Vector3 GetRoadNextPos () {
		return transform.localPosition + RoadConst.RoadDis;
	}

	public void SetRoadColor (ColorType lfet, ColorType middle, ColorType right) {
		// Debug.Log("[lfet]:"+lfet+"[middle]:"+middle+"[right]:"+right);
		roadItems[RoadItemName.left].SetColor (lfet);
		roadItems[RoadItemName.right].SetColor (right);
		roadItems[RoadItemName.middle].SetColor (middle);
	}
}