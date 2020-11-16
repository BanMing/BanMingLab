/******************************************************************
** AsyncTest.cs
** @Author       : BanMing 
** @Date         : 2020/10/19 下午1:54:28
** @Description  : 
*******************************************************************/

using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
public class AsyncTest
{
    public async Task WaitSeconds(int millisecondsDelay)
    {
        Console.WriteLine("$$$$$$$$$$$$$$$$$$$");

        await Task.Delay(millisecondsDelay);

        Console.WriteLine("@@@@@@@@@@@@@");
    }


    public static async void Run()
    {
        AsyncTest test = new AsyncTest();

        await test.WaitSeconds(1000);
    }
}