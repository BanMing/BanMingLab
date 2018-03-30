using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	private List<Road> roads;
	private static RoadManager instance;

	public static RoadManager Instance {
		get {
			if (!instance) {
				instance = (RoadManager) FindObjectOfType (typeof (RoadManager));
				if (!instance) {
					Debug.LogError ("There needs to be one active RoadManager script on a GameObject in your scene");
				}
			}
			return instance;
		}
	}
	void Start () {
		roads = new List<Road> ();
		var index = 0;
		foreach (Transform item in transform) {
			var road = item.GetComponent<Road> ();
			road.Index = index;
			index++;
			roads.Add (road);
		}
	}

	public void SetNewRoad (ColorType lfet, ColorType middle, ColorType right) {
		roads[0].SetRoadPos (roads[roads.Count - 1].GetRoadNextPos ());
		roads[0].SetRoadColor (lfet, middle, right);
		var item = roads[0];
		roads.RemoveAt (0);
		roads.Add (item);
	}

	public void RandomNewRoad () {
		SetNewRoad ((ColorType) Random.Range (0, 3), (ColorType) Random.Range (0, 3), (ColorType) Random.Range (0, 3));
	}
}