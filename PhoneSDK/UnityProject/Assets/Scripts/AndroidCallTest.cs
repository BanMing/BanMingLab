using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCallTest : MonoBehaviour
{
    private float level = 0;
    private int current = 0;
    private int total = 0;
    private AndroidJavaObject activity;
#if  UNITY_ANDROID
    void OnGUI()
    {
        GUILayout.Label("Level:" + level);
        GUILayout.Label("current:" + current);
        GUILayout.Label("total:" + total);
    }
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
    /// <summary>
    /// 通过包名打开已安装的应用，若未安装则产生异常
    /// </summary>
    /// <param name="PKName">包名</param>
    /// <returns>bool</returns>
    public void OpenInstalledApp(string PKName)
    {
        if (string.IsNullOrEmpty(PKName))
        {
            Debug.Log("PKname is empty");
            // return false;
        }
        try
        {
            AndroidJavaObject it = new AndroidJavaObject("android.content.Intent");
            AndroidJavaObject packageManager = Activity.Call<AndroidJavaObject>("getPackageManager");
            it = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", PKName);
            Activity.Call("startActivity", it);
            // return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            // return false;
        }
    }

    public void OpenOtherApp(string PKName)
    {
        Activity.Call("CallThirdApp", PKName);
    }
    public void CreateToast(string PKName)
    {

        Activity.Call("CreateToast", PKName);
    }
    public void ShareText(string message, string body)
    {
        Activity.Call("ShareText", message, body);
    }
#else
  public void OpenOtherApp(string PKName)
    {
    }
    public void OpenInstalledApp(string PKName)
    {
    }
     public void CreateToast(string PKName)
    {


    }
    public void ShareText(string message, string body)
    {

    }
#endif
}
