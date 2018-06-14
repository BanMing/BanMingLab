package com.suixinplay.base;

import android.app.Activity;
import android.widget.Toast;

import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.unity3d.player.UnityPlayer;

public class WXSender {
    private WXSender(){}

    private static class singlon{
        private static  final  WXSender INSTANCE=new WXSender();
    }
    public static WXSender Instance(){return singlon.INSTANCE;}

    public IWXAPI api;


    public void Init(Activity activity) {
        api = WXAPIFactory.createWXAPI(activity, Constants.APP_ID);
        api.registerApp(Constants.APP_ID);
    }

    public void LoginWeChat() {
        SendAuth.Req req = new SendAuth.Req();
        req.scope = "snsapi_userinfo";
        req.state = "none";
        api.sendReq(req);
    }

    public boolean IsIntallWeChat() {
        boolean bIsWXAppInstalledAndSupported = api.isWXAppInstalled() && api.isWXAppSupportAPI();

        return bIsWXAppInstalledAndSupported;
    }

    public void CheckWeChatSdk() {
        UnityPlayer.UnitySendMessage("AndroidTest", "AndroidCall", "Jar CheckWeChatSdk");
        if (IsIntallWeChat()) {
            UnityPlayer.UnitySendMessage("AndroidTest", "AndroidCall", "微信已安装！");
        } else {
            UnityPlayer.UnitySendMessage("AndroidTest", "AndroidCall", "微信未安装！");
        }
    }
}
