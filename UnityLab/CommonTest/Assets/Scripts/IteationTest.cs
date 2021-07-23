using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteator : MonoBehaviour
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
        if (GUILayout.Button("for array"))
        {
            for (int i = 0; i < _testStructs.Length; i++)
            {
                //b = _testStructList[i].a;
            }
        }

        if (GUILayout.Button("for list"))
        {
            for (int i = 0; i < _testStructList.Count; i++)
            {
                //b = _testStructList[i].a;
            }
        }

        if (GUILayout.Button("for array"))
        {
            foreach (var item in _testStructs)
            {
                //b = item.a;
            }
        }

        if (GUILayout.Button("for list"))
        {
            foreach (var item in _testStructList)
            {
                //b = item.a;
            }
        }
    }
}
