package com.suixinplay.base;


import android.os.Bundle;
import android.widget.Toast;


import com.tencent.mm.opensdk.constants.ConstantsAPI;
import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;



public class MyMainActivity extends UnityPlayerActivity implements IWXAPIEventHandler {

    private static final String APP_ID = "wx57fdd2cd0b0a8440";
    private IWXAPI api;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        api = WXAPIFactory.createWXAPI(this, APP_ID);
        api.registerApp(APP_ID);
    }

    @Override
    public void onReq(BaseReq baseReq) {

    }

    @Override
    public void onResp(BaseResp baseResp) {
        UnityPlayer.UnitySendMessage("AndroidTest","AndroidCall","WeChatOnResp");
        switch (baseResp.getType()){
            case ConstantsAPI.COMMAND_PAY_BY_WX:
                break;
            case ConstantsAPI.COMMAND_INVOICE_AUTH_INSERT:
                break;
        }

    }

    public void CheckWeChatSdk() {
        if (isWXAppInstalledAndSupported(api)) {
            ShowToast("微信已安装！！");
        } else {
            ShowToast("微信未安装！！");
        }
    }

    //微信登录
    public void  ReqLoginWeChat(){
        final SendAuth.Req req=new SendAuth.Req();
        req.scope="snsapi_userinfo";
        req.state = "wechat_sdk_demo_test";
        api.sendReq(req);
    }

    private void ShowToast(final String toast) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                //onCoderReturn("CreateToast()");
                Toast.makeText(
                        MyMainActivity.this,
                        toast, Toast.LENGTH_LONG).show();
            }
        });
    }

    //检测是否安装微信
    private boolean isWXAppInstalledAndSupported(IWXAPI _api) {
        boolean bIsWXAppInstalledAndSupported = _api.isWXAppInstalled() && _api.isWXAppSupportAPI();

        return bIsWXAppInstalledAndSupported;
    }
}
