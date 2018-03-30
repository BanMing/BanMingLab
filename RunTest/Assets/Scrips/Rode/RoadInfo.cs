using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadConst {
	public const int RoadCount = 3;

	public const int RoadLenght=12;
	public static Vector3 RoadDis = new Vector3 (0, 0, 11);
}

public enum ColorType {
	YellowColor,
	RedColor,
	WhiteColor
}

public struct RoadItemName {
	public static string left = "Left";
	public static string middle = "Middle";
	public static string right = "Right";
}