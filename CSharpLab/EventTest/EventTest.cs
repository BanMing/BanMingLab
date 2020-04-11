using System;

public class EventTest {
    public event Action<string> EventPrintStr;

    public Action<string> ActionPrintStr;

    public void DoEvent () {
        EventPrintStr.Invoke ("mmmm");
    }
}