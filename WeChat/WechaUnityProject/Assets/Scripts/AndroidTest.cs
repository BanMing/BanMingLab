using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AndroidTest : MonoBehaviour
{
    public GameObject DebugView;
    private bool isGetUserInfoNow;
    void Start()
    {
        DebugView.SetActive(true);
        isGetUserInfoNow = false;
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
                AndroidJavaClass sender = new AndroidJavaClass("com.suixinplay.base.WX.WXSender");
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

    public void ShareContentToFriend()
    {
        WXSender.Call("ShareContentToFriend", "www.baidu.com", "测试", "测试Content", "http://mmbiz.qpic.cn/mmbiz/PiajxSqBRaEIVJ6bW5EhIpIVZuxavukF9QFCv8huhyAFhXWvianxRYw9vyfjoEmr8uSPAJOI3ckfRWLEiaJw6EIwA/0?wx_fmt=png");
    }

    public void ShareLocalPicToFriend()
    {
        StartCoroutine(ShareShotScreenPic());
    }
    private IEnumerator ShareShotScreenPic(){
        
        // 截屏1帧后再呼起微信
        yield return new WaitForEndOfFrame();

        string imgPath = System.IO.Path.Combine(Application.persistentDataPath, "Screenshot.jpg");
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        tex.Apply();
        System.IO.File.WriteAllBytes(imgPath, tex.EncodeToJPG());
        Debug.Log("分享截图：" + imgPath);

        WXSender.Call("ShareLocalPicToFriend", imgPath);
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

    public void GetAccessToken(string code)
    {
        if (!isGetUserInfoNow)
        {
            isGetUserInfoNow = true;
            Debug.Log("Code:" + code);
            var url = WeChatTool.AccessTokenUrl.Replace("#CODE#", code);
            StartCoroutine(getAccessToken(url));
        }

    }
    IEnumerator getAccessToken(string accessUrl)
    {
        var webRequest = UnityWebRequest.Get(accessUrl);
        yield return webRequest.Send();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.LogError("getAccessToken error:" + webRequest.error);
        }
        else
        {
            Debug.Log("webRequest getAccessToken:" + webRequest.downloadHandler.text);
            var jsonObj = JSONObject.Create(webRequest.downloadHandler.text);
            if (jsonObj["errcode"] == null)
            {
                var openId = jsonObj["openid"].ToString();
                // Debug.LogWarning("openId:" + openId);
                var accessToken = jsonObj["access_token"].ToString();
                // Debug.LogWarning("accessToken:" + accessToken);
                var newUrl = WeChatTool.UserInfoUrl.Replace("#OPENID#", openId);
                newUrl = newUrl.Replace("#ACCESS_TOKEN#", accessToken);
                // Debug.LogWarning("newUrl:" + newUrl);
                newUrl = newUrl.Replace("\"", "");
                // Debug.LogWarning("@@@@@newUrl:" + newUrl);
                var webRequest1 = UnityWebRequest.Get(newUrl);
                yield return webRequest1.Send();
                if (webRequest1.isHttpError || webRequest1.isNetworkError)
                {
                    Debug.LogError("getUserinfo error:" + webRequest1.error);
                }
                else
                {
                    Debug.Log("webRequest1:" + webRequest1.downloadHandler.text);
                    if (JSONObject.Create(webRequest1.downloadHandler.text)["errcode"] == null)
                    {
                        var userInfo = JsonUtility.FromJson<WeChatUserInfo>(webRequest1.downloadHandler.text);
                        Debug.Log("userInfo.openid:" + userInfo.openid);
                        Debug.Log("userInfo.nickname:" + userInfo.nickname);
                    }
                }
            }
        }
        isGetUserInfoNow = false;
    }
}
