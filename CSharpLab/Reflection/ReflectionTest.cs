using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

public class ReflectionTest
{
    public const int kHashInitialValue = unchecked((int)2166136261U);
    private const int kHashMultiplier = 65599;

    public static class GetType<T>
    {
        public static readonly Type kType = typeof(T);
        public static readonly string kName = kType.Name;
        public static readonly string kFullName = kType.FullName;
        public static readonly int kFullNameHashCode = kFullName.GetHashCode();
        public static readonly bool kIsValueType = kType.IsValueType;
    }

    public static int Combine(int val1, int val2)
    {
        int sum = kHashInitialValue;
        sum = sum * kHashMultiplier + val1;
        sum = sum * kHashMultiplier + val2;
        return sum;
    }

    private class Test1
    {
        private int a;
        private void print() { }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public static void Run()
    {
        int hashCode = GetType<Test1>.kFullNameHashCode;
        Console.WriteLine($"hashCode : {hashCode}");

        int combine0 = ReflectionTest.Combine(hashCode, 0);
        
        Console.WriteLine($"0 Combine {combine0}");
        
        Console.WriteLine($"-combine0 Combine {ReflectionTest.Combine(hashCode, -combine0)}");
    }
}