using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour {
	private static ResourcesManager instance;

	public static ResourcesManager Instance {
		get {
			if (!instance) {
				instance = (ResourcesManager) FindObjectOfType (typeof (ResourcesManager));
				if (!instance) {
					Debug.LogError ("There needs to be one active ResourcesManager script on a GameObject in your scene");
				}
			}
			return instance;
		}
	}
	public Material GetColorMaterial (ColorType colorType) {
		// var colorName = Enum.GetName (typeof (ColorType), colorType);
		// var m = Instantiate(Resources.Load<Material> ("Materials/" + colorName));
		return Resources.Load<Material> ("Materials/RedColor");
	}
}