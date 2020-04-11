using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct MyJob : IJob
{
    public float a;
    public float b;

    public NativeArray<float> result;
    public void Execute()
    {
        // throw new NotImplementedException();
        result[0] = a + b;
    }
}
public struct AddOneJob : IJob
{
    public NativeArray<float> result;
    public void Execute()
    {
        result[0] += 1;
    }
}