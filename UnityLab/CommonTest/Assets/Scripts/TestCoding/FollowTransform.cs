using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Transform/Follow Transform")]
public class FollowTransform : MonoBehaviour
{
    public Texture texture;
    public RectTransform transform;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.anchoredPosition = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0);
    }
    void OnGUI()

    {

        //绘制准心

        Rect rect = new Rect(Input.mousePosition.x - (texture.width >> 1),

      Screen.height - Input.mousePosition.y - (texture.height >> 1),

      texture.width, texture.height);

        GUI.DrawTexture(rect, texture);

    }


}
