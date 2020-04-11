using System;

namespace CSharpTest {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            EventTest eventTest = new EventTest ();
            eventTest.EventPrintStr += (str) => { Console.WriteLine ("Event Print Str" + str); };
            // eventTest.PrintStr();
            eventTest.ActionPrintStr += (str) => { Console.WriteLine ("Action Print Str" + str); };
            eventTest.ActionPrintStr.Invoke ("sssss");
            eventTest.DoEvent ();
        }
    }
}