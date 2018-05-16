using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCallTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
       if (GUILayout.Button("Open WeChat"))
       {
           OpenInstalledApp("com.tencent.mm");
       } 
    }
    /// <summary>
    /// 通过包名打开已安装的应用，若未安装则产生异常
    /// </summary>
    /// <param name="PKName">包名</param>
    /// <returns>bool</returns>
    public  bool OpenInstalledApp(string PKName)
    {
        if (string.IsNullOrEmpty(PKName))
        {
            Debug.Log("PKname is empty");
            return false;
        }

        AndroidJavaClass Player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject Activity = Player.GetStatic<AndroidJavaObject>("currentActivity");
        try
        {
            AndroidJavaObject it = new AndroidJavaObject("android.content.Intent");
            AndroidJavaObject packageManager = Activity.Call<AndroidJavaObject>("getPackageManager");
            it = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", PKName);
            Activity.Call("startActivity", it);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    // /// <summary>
    // /// 打开已安装的app（暂时通过打开appStore再打开app）
    // /// </summary>
    // /// <param name="url">app的appstore地址</param>
    // /// <returns>bool</returns>
    // public static bool OpenInstalledApp(string url){
    //     if (string.IsNullOrEmpty(url))
    //     {
    //         Debug.Log("url is empty");
    //         return false;
    //     }
    //     string appUrl = url.Replace("https://", "itms-apps://");
    //     Application.OpenURL(appUrl);
    //     return true;
    // }
}
