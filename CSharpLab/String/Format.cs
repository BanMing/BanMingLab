using System;
using System.Collections.Generic;
namespace Test
{
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