using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
/// <summary>
///并行jobs
/// </summary>
public struct MyParalleForJobs : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<float> a;
    [ReadOnly]
    public NativeArray<float> b;
    public NativeArray<float> result;

    public void Execute(int index)
    {
        UnityEngine.Debug.Log("index:"+index+"a[index]:"+a[index]);
        result[index]=a[index]+b[index];
    }
}