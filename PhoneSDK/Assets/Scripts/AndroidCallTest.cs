using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCallTest : MonoBehaviour
{
    private float level = 0;
    private int current=0;
    private int total=0;

#if    UNITY_ANDROID
    void OnGUI()
    {
        if (GUILayout.Button("Open WeChat"))
        {
            OpenInstalledApp("com.tencent.mm");
        }
        if (GUILayout.Button("Get Batter Level"))
        {
            level = GetBatterLevel();

        }
        GUILayout.Label("Level:" + level);
        GUILayout.Label("current:" + current);
        GUILayout.Label("total:" + total);
    }
    /// <summary>
    /// 通过包名打开已安装的应用，若未安装则产生异常
    /// </summary>
    /// <param name="PKName">包名</param>
    /// <returns>bool</returns>
    public bool OpenInstalledApp(string PKName)
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

    public int GetBatterLevel()
    {
        AndroidJavaClass Player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject Activity = Player.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject it = new AndroidJavaObject("android.content.Intent");
        var getExtras = it.Call<AndroidJavaObject>("getExtras");
        var getInt = getExtras.Call<AndroidJavaObject>("getInt");
        current = getInt.Call<int>("level");
        total = getInt.Call<int>("scale");

        return current/total;
    }

    public int GetBatterLevelTemp()
    {
        AndroidJavaClass Player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject Activity = Player.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject it = new AndroidJavaObject("android.content.Intent");
        var getExtras = it.Call<AndroidJavaObject>("getExtras");
        var getInt = getExtras.Call<AndroidJavaObject>("getInt");
        current = getInt.Call<int>("level");
        total = getInt.Call<int>("scale");
        
        return current/total;
    }
#endif
}
