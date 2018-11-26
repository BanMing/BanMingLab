using System;
using System.Threading.Tasks;

public class TaskTest
{
    /// <summary>
    /// 创建
    /// </summary>
    public void TestCreate()
    {
        //第一种方式，直接实例化
        var task1 = new Task(() =>
        {
            Console.WriteLine("Hello,Task");
        });
        task1.Start();

        //第二种使用工厂方式打开
        var task2 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Task Factory Create");
        });
        Console.ReadLine();
    }

    /// <summary>
    /// 生命周期 
    /// </summary>
    public void TaskLife()
    {
        var task1 = new Task(() =>
        {
            Console.WriteLine("begin");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Finish");
        });
        Console.WriteLine("Before start:" + task1.Status);
        task1.Start();
        Console.WriteLine("After start:" + task1.Status);
        //等待相当于阻塞
        task1.Wait();
        Console.WriteLine("After Finish:" + task1.Status);

        Console.Read();
    }

    /// <summary>
    /// 等待所有任务执行完成
    /// </summary>
    public void TaskWaitAll()
    {
        var task1 = new Task(() =>
        {
            Console.WriteLine("Task1 Start");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Task1 End");
        });
        var task2 = new Task(() =>
        {
            Console.WriteLine("Task2 Start");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Task2 End");
        });

        task1.Start();
        task2.Start();
        Task.WaitAll(task1, task2);
        Console.WriteLine("Task Finish");
        Console.Read();
    }

    /// <summary>
    /// 只要有任意一个task完成就继续执行
    /// </summary>
    public void TaskWaitAny()
    {
        var task1 = new Task(() =>
        {
            Console.WriteLine("Task1 Start");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Task1 End");
        });

        var task2 = new Task(() =>
        {
            Console.WriteLine("Task2 Start");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Task2 End");
        });

        task1.Start();
        task2.Start();
        Task.WaitAny();
        Console.WriteLine("Task Finish");
        Console.Read();
    }

    /// <summary>
    /// 在一个任务后执行另一个任务
    /// </summary>
    public void TaskContinue()
    {
        var task1 = new Task(() =>
       {
           Console.WriteLine("Task1 Start");
           System.Threading.Thread.Sleep(2000);
           Console.WriteLine("Task1 End");
       });

        var task2 = new Task(() =>
        {
            Console.WriteLine("Task2 Start");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Task2 End");
        });
        task1.Start();
        task2.Start();
        //在task1执行完成后再执行该task
        task1.ContinueWith(task=>{
            task.ToString();
        });
    }
}