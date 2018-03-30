using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;

	public float temp=10;
	private float tempPos;
	void Start () {
		tempPos = transform.localPosition.z;
	}

	void Update () {
		// Debug.LogWarning("[one]:"+Random.Range (0, 3)+"[two]:"+Random.Range (0, 3)+"[three]:"+Random.Range (0, 3));
		if (transform.localPosition.z > tempPos + temp) {
			tempPos = transform.localPosition.z;
			RoadManager.Instance.RandomNewRoad();
		}
		transform.Translate (speed * Time.deltaTime * Vector3.forward);
	}
}