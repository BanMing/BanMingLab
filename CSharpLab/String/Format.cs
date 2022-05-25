using System;
using System.Collections.Generic;
namespace Test
{
    struct StringTestStruct
    {
        int id;

        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class StringTest
    {
        public static void Run()
        {
            StringTestStruct structTest = new StringTestStruct();
            object structTestObj = (object)structTest;
            // System.InvalidCastException: Unable to cast object of type 'Test.StringTestStruct' to type 'System.String'. 
            string a = structTestObj as string;
            Console.WriteLine(a);
        }
    }
    public class FormatTest
    {
        private void SpaceFormat()
        {
            // string test = "Hello";
            Console.WriteLine($"->{"test",10}<-");
            Console.WriteLine($"->{"test",-10}<-");
        }

        private void ListFormat()
        {
            List<string> lists = new List<string>() { "ss", "333", "rrr" };
            Console.WriteLine(lists.ToString());
            Console.WriteLine(string.Join(',', lists.ToArray()));
        }

        public static void Run()
        {
            var a = new FormatTest();
            Console.WriteLine(a.ToString());
            Console.WriteLine(a.GetType());
        }
    }
}