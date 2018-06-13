using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidTest : MonoBehaviour {
    private AndroidJavaObject activity;

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
	public void CheckWeChat(){
        Activity.Call("CheckWeChatSdk");
	}

    public void LoginWeChat(){
        Activity.Call("ReqLoginWeChat");
    }
    public void AndroidCall(string str){
        // Debug.
    }
}
