using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class TestMono : MonoBehaviour
{
    //单个jobs
    public void SchedulingJobs()
    {
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);
        MyJob jobData = new MyJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;

        //开始一个任务
        JobHandle handle = jobData.Schedule();

        //等待job完成
        handle.Complete();

        float aPlusB = result[0];
        UnityEngine.Debug.Log("result:" + aPlusB);
        //释放内存中的result
        result.Dispose();
    }

    //多个jobs依赖关系
    public void SchedulingTwoJobs()
    {
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);
        MyJob jobData = new MyJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;
        // 设置第一个任务开关
        JobHandle firstHandle = jobData.Schedule();

        //设置第二个任务
        AddOneJob incJobData = new AddOneJob();
        incJobData.result = result;

        // UnityEngine.Debug.Log(result[0]);
        //设置依赖
        JobHandle secondHandle = incJobData.Schedule(firstHandle);

        //等待完成
        secondHandle.Complete();

        float aPlusB = result[0];

        UnityEngine.Debug.Log(aPlusB);

        //释放资源
        result.Dispose();
    }
    //并行jobs
    public void SchedulingParalleJob()
    {
        var a = new NativeArray<float>(2, Allocator.TempJob);
        var b = new NativeArray<float>(2, Allocator.TempJob);
        var result = new NativeArray<float>(2, Allocator.TempJob);

        a[0] = 1.1f;
        b[0] = 2.2f;
        a[1] = 3.3f;
        b[1] = 4.4f;

        MyParalleForJobs jobData = new MyParalleForJobs();
        jobData.a = a;
        jobData.b = b;
        jobData.result = result;

        JobHandle handle = jobData.Schedule(result.Length, 1);

        handle.Complete();



        for (int i = 0; i < a.Length; i++)
        {
            UnityEngine.Debug.Log("a:" + a[i]);
        }
        for (int i = 0; i < b.Length; i++)
        {
            UnityEngine.Debug.Log("b:" + b[i]);
        }
        for (int i = 0; i < result.Length; i++)
        {
            UnityEngine.Debug.Log("result:" + result[i]);
        }


        a.Dispose();
        b.Dispose();
        result.Dispose();
    }
    void OnGUI()
    {
        if (GUILayout.Button("Scheduling Jobs"))
        {
            SchedulingJobs();
        }
        if (GUILayout.Button("Scheduling Two Jobs"))
        {
            SchedulingTwoJobs();
        }
        if (GUILayout.Button("Scheduling Parallel Jobs"))
        {
            SchedulingParalleJob();
        }
        // UnityEngine.Debug.Log(Input.touchCount);
    }
}
