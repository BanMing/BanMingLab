using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class LoopTest
{
    private int _teamp = 0;
    private int _costCount = 1000000;
    private int _invokeTimes = 4000;

    private void CostTimeMethod(int index)
    {
        for (int i = 0; i < _costCount; i++)
        {
            _teamp += i;
        }
        // System.Threading.Thread.Sleep(1);
    }
    private void ParallelForTest()
    {
        Stopwatch watch = new Stopwatch();

        watch.Start();
        CostTimeMethod(0);
        watch.Stop();

        Console.WriteLine($"Single method cost time : {watch.ElapsedMilliseconds} ms");

        watch.Restart();
        for (int i = 0; i < _invokeTimes; i++)
        {
            CostTimeMethod(i);
        }
        watch.Stop();

        Console.WriteLine($"For Invoke { _invokeTimes} count method cost time : {watch.ElapsedMilliseconds} ms ");

        watch.Restart();
        Parallel.For(0, _invokeTimes, CostTimeMethod);
        watch.Stop();

        Console.WriteLine($"Parallel For Invoke { _invokeTimes} count method cost time : {watch.ElapsedMilliseconds} ms ");
    }
    private void SplitLoopTest()
    {
        Stopwatch watch = new Stopwatch();

        watch.Start();
        for (int i = 0; i < _invokeTimes; i++)
        {
            CostTimeMethod(i);
        }
        watch.Stop();

        Console.WriteLine($"For Invoke { _invokeTimes} count method cost time : {watch.ElapsedMilliseconds} ms ");


        watch.Restart();
        for (int i = 0; i < _invokeTimes / 2; i++)
        {
            CostTimeMethod(i);
        }
        for (int i = 0; i < _invokeTimes / 2; i++)
        {
            CostTimeMethod(i);
        }
        watch.Stop();

        Console.WriteLine($"Split For Invoke { _invokeTimes} count method cost time : {watch.ElapsedMilliseconds} ms ");
    }
    public static void Run()
    {
        LoopTest test = new LoopTest();
        System.Threading.Thread thread = new System.Threading.Thread(test.ParallelForTest);
        thread.Start();
    }
}