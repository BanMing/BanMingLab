using System;
using System.Collections;
using System.Collections.Generic;


public class ReferenceTest
{
    public List<int> list;

    public ReferenceTest()
    {
        this.list = new List<int>() { 1, 2, 3, 4 };
    }

    public void Print()
    {
        for (int i = 0; i < list.Count; i++)
        {
            System.Console.WriteLine($"Test List[{i}] = {list[i]}");
        }

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
}

public class ReferneceTest1
{
    public int a;

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

public class ReferneceTest2
{
    public ReferneceTest1 a;

    public static void Run()
    {
        ReferneceTest1 test1 = new ReferneceTest1();
        test1.a = 3;
        Console.WriteLine(test1.a);

        ReferneceTest2 test2 = new ReferneceTest2();
        test2.a = test1;
        Console.WriteLine(test2.a.a);

        test1 = new ReferneceTest1();
        test1.a = 4;
        Console.WriteLine(test2.a.a);
    }
}

public class ReferneceTest3
{
    private List<int> list;

    public static void Run()
    {
        ReferenceTest test = new ReferenceTest();

        ReferneceTest3 test3 = new ReferneceTest3();
        test3.list = test.list;

        test3.list[2] = 999;
        
        test.Print();
    }
}