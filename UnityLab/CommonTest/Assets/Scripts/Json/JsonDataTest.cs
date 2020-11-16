/******************************************************************
** JsonDataTest.cs
** @Author       : BanMing 
** @Date         : 11/16/2020 9:46:58 AM
** @Description  : 
*******************************************************************/
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class JsonDataTest
{
    [SerializeField]
    private int[,] schedule = new int[7, 24];

    [SerializeField]
    private int a = 3;

    [SerializeField]
    private List<int> b = new List<int>();

    [SerializeField]
    private int[] c = new int[9];

    public void Save()
    {
        string jsn = JsonUtility.ToJson(this);
        Debug.Log($"json string :{jsn}");
    }

}
