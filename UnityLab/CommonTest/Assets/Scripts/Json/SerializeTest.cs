/******************************************************************
** SerializeTest.cs
** @Author       : BanMing 
** @Date         : 11/16/2020 10:03:15 AM
** @Description  : 
*******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SerializeTest : MonoBehaviour
{
    [SerializeField]
    private int[,] schedule = new int[7, 24];

    [SerializeField]
    private int a = 3;

    [SerializeField]
    private List<int> b = new List<int>();

    [SerializeField]
    private int[] c = new int[9];
}
