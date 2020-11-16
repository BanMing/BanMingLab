using System;
using System.Collections;
using System.Collections.Generic;
public class StructNewTest
{
    struct Test
    {
        // public int a;

    }

    struct Point
    {
        // 在结构体中不能手动申明一个无参数构成函数
        // public Point(){

        // }
        public float x;
        // public float y;
    }

    sealed class Line
    {
        public float x = 0;
        // public float y = 0;
    }

    public static void Run()
    {
        Point point1 = new Point();
        Console.WriteLine($"{point1.x.ToString()}");

        // Point point2;
        // Console.WriteLine($"{point2.x.ToString()}");

        Line line1 = new Line();
        Console.WriteLine($"{line1.x.ToString()}");

        // Line line2;
        // Console.WriteLine($"{line2.x.ToString()}");
    }

    public interface IPoint
    {
        float GetX();
        float GetY();
    }
}