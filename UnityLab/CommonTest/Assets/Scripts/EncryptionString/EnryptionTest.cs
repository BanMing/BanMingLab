using UnityEngine;

public class EnryptionTest : MonoBehaviour {

private string inputText="测试";
    void OnGUI()
    {
        inputText=GUILayout.TextField(inputText,GUILayout.Width(900));
        if (GUILayout.Button("加密"))
        {
            inputText=Enryption.EnryptionStr(inputText);
            Debug.Log("加密后："+inputText);
        }
        if (GUILayout.Button("解密"))
        {
            inputText=Enryption.DecipheringStr(inputText);
            Debug.Log("解密后："+inputText);
        }
    }
}