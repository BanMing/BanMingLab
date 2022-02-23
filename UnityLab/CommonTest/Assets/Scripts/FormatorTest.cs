#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatorTest : MonoBehaviour
{
    private System.Text.StringBuilder _sb = new System.Text.StringBuilder();
    private static System.Text.StringBuilder s_sb = new System.Text.StringBuilder();
    private const string kFormator = "2222 {0}";
    private string _test = string.Empty;

    private void OnGUI()
    {
        if (GUILayout.Button("GC Collect"))
        {
            System.GC.Collect();
        }

        if (GUILayout.Button("StringBuilder"))
        {
            for (int i = 0; i < 100000; i++)
            {
                _sb.Clear();
                _sb.Length = 0;
                _sb.AppendFormat("in {0}", i);
                _test = _sb.ToString();
            }
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("StringBuilder Const"))
        {
            for (int i = 0; i < 100000; i++)
            {
                _sb.Clear();
                _sb.Length = 0;
                _sb.AppendFormat(kFormator, i);
                _test = _sb.ToString();
            }
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("Static StringBuilder"))
        {
            for (int i = 0; i < 100000; i++)
            {
                s_sb.Clear();
                s_sb.Length = 0;
                s_sb.AppendFormat("in {0}", i);
                _test = s_sb.ToString();
            }
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("Static StringBuilder Const"))
        {
            for (int i = 0; i < 100000; i++)
            {
                s_sb.Clear();
                s_sb.Length = 0;
                s_sb.AppendFormat(kFormator, i);
                _test = s_sb.ToString();
            }
            UnityEditor.EditorApplication.isPaused = true;
        }


        if (GUILayout.Button("Insert"))
        {
            for (int i = 0; i < 100000; i++)
            {
                _test = $"sssss{i}";
            }
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("Format"))
        {
            for (int i = 0; i < 100000; i++)
            {
                _test = string.Format("sss {0}", i);
            }
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("Format Const"))
        {
            for (int i = 0; i < 100000; i++)
            {
                _test = string.Format(kFormator, i);
            }
            UnityEditor.EditorApplication.isPaused = true;
        }
    }
}
#endif