using System;
using System.Collections;
using System.Collections.Generic;

public class ReferneceTest1
{
    public int a;
}

public class ReferenceTest
{

    private List<int> list;

    public ReferenceTest()
    {
        this.list = new List<int>() { 1, 2, 3, 4 };
    }

    public void Test1()
    {
        Test2(list);
        Console.WriteLine(list[0]);

        var list2 = new List<int>() { 2, 3 };
        Test2(list2);
        Console.WriteLine(list2[0]);

        var ararry = new int[] { 2 };
        Test3(ararry);
        Console.WriteLine(ararry[0]);
    }

    public void Test2(List<int> testList)
    {
        testList[0] = 3;
    }

    public void Test3(int[] ararry)
    {
        ararry[0] = 4;
    }

    public static void Run()
    {
        ReferenceTest referenceTest = new ReferenceTest();
        referenceTest.Test1();
    }

    private static void Test1(ReferneceTest1 test1)
    {
        test1.a = 4;
    }

    public static void Run1()
    {
        ReferneceTest1 test1 = new ReferneceTest1();
        test1.a = 3;
        Test1(test1);
        Console.WriteLine(test1.a);
    }
}