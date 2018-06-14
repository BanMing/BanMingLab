using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidTest : MonoBehaviour
{
    public GameObject DebugView;

    void Start()
    {
        DebugView.SetActive(true);
    }
    private AndroidJavaObject activity;
    private AndroidJavaObject wxSender;
    private AndroidJavaObject Activity
    {
        get
        {
            if (activity == null)
            {
                AndroidJavaClass Player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = Player.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return activity;
        }
    }
    private AndroidJavaObject WXSender
    {
        get
        {
            if (wxSender == null)
            {
                AndroidJavaClass sender = new AndroidJavaClass("com.suixinplay.base.WXSender");
                wxSender = sender.CallStatic<AndroidJavaObject>("Instance");
                wxSender.Call("Init", Activity);
            }
            return wxSender;
        }
    }

    public void CheckWeChat()
    {
        WXSender.Call("CheckWeChatSdk");
    }

    public void LoginWeChat()
    {
        // Debug.Log("LoginWeChat");
        WXSender.Call("LoginWeChat");
    }
    public void AndroidCall(string str)
    {
        Debug.Log("AndroidCall:" + str);
    }
}
