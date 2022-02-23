using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTest : MonoBehaviour
{

    public Text text;
    private int a = 1;
    public void OnClick()
    {
        a++;
        text.text = a.ToString();
    }
}
