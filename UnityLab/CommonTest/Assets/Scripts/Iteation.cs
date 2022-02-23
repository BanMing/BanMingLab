#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteationTest : MonoBehaviour
{
    private struct TestStruct
    {
        public int a;
    }

    private int _count = 100000;

    private TestStruct[] _testStructs;
    private List<TestStruct> _testStructList;

    private int b;
    private void Awake()
    {
        _testStructs = new TestStruct[_count];
        _testStructList = new List<TestStruct>(_count);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("GC Collect"))
        {
            System.GC.Collect();
        }

        if (GUILayout.Button("for array"))
        {
            UnityEngine.Profiling.Profiler.BeginSample("for array");
            for (int i = 0; i < _testStructs.Length; i++)
            {
                //b = _testStructList[i].a;
            }
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("for list"))
        {
            UnityEngine.Profiling.Profiler.BeginSample("for list");
            for (int i = 0; i < _testStructList.Count; i++)
            {
                //b = _testStructList[i].a;
            }
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("foreach array"))
        {
            UnityEngine.Profiling.Profiler.BeginSample("foreach array");
            foreach (var item in _testStructs)
            {
                //b = item.a;
            }
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEditor.EditorApplication.isPaused = true;
        }

        if (GUILayout.Button("foreach list"))
        {
            UnityEngine.Profiling.Profiler.BeginSample("foreach list");
            foreach (var item in _testStructList)
            {
                //b = item.a;
            }
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEditor.EditorApplication.isPaused = true;
        }
    }
}
#endif
