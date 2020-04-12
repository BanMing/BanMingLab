using System;

public class EventTest
{
    public event Action<string> EventPrintStr;

    public Action<string> ActionPrintStr;

    public void DoEvent()
    {
        EventPrintStr.Invoke("mmmm");
    }

    public static void RunTest()
    {
        //  EventTest eventTest = new EventTest ();
        //     eventTest.EventPrintStr += (str) => { Console.WriteLine ("Event Print Str" + str); };
        //     // eventTest.PrintStr();
        //     eventTest.ActionPrintStr += (str) => { Console.WriteLine ("Action Print Str" + str); };
        //     eventTest.ActionPrintStr.Invoke ("sssss");
        //     eventTest.DoEvent ();
    }
}