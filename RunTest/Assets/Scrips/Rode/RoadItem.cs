using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadItem : MonoBehaviour {

	private new Renderer renderer;

	void Start () {
		renderer = GetComponent<Renderer> ();
	}

	public void SetColor (ColorType colorType) {
		Color color;

		switch (colorType) {
			case ColorType.RedColor:
				color = Color.red;
				break;
			case ColorType.WhiteColor:
				color = Color.white;
				break;
			default:
				color = Color.yellow;
				break;
		}

		renderer.material.color = color;
	}

	public void SetActive (bool flag) {
		gameObject.SetActive (flag);
	}
}