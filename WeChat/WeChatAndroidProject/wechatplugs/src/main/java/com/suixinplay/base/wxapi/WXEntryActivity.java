package com.suixinplay.base.wxapi;

import android.app.Activity;
import android.os.Bundle;
import android.widget.Toast;
import android.content.Intent;

import com.suixinplay.base.Constants;
import com.suixinplay.base.MyMainActivity;
import com.suixinplay.base.WXSender;
import com.tencent.mm.opensdk.constants.ConstantsAPI;
import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.unity3d.player.UnityPlayer;

public class WXEntryActivity extends Activity implements IWXAPIEventHandler {

    private IWXAPI api;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (WXSender.Instance().api != null) {
            WXSender.Instance().api.handleIntent(getIntent(), this);
        }
        api = WXAPIFactory.createWXAPI(this, Constants.APP_ID, false);
        api.handleIntent(getIntent(), this);
    }

    @Override
    public void onReq(BaseReq baseReq) {

    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        setIntent(intent);
        api.handleIntent(intent, this);
    }

    @Override
    public void onResp(BaseResp baseResp) {
        Toast.makeText(this, "baseresp.getType = " + baseResp.getType(), Toast.LENGTH_SHORT).show();
        switch (baseResp.getType()) {
            //支付
            case ConstantsAPI.COMMAND_PAY_BY_WX:
                break;
                case ConstantsAPI.COMMAND_SENDMESSAGE_TO_WX:
                    break;
                //玩家登陆
            case ConstantsAPI.COMMAND_SENDAUTH:
                OnLoginResp(baseResp) ;
                break;
        }
        finish();
    }

    private void OnLoginResp(BaseResp baseResp){
        switch (baseResp.errCode){
            //登陆成功
            case BaseResp.ErrCode.ERR_OK:
//                SendAuth.Resp sendResp = (SendAuth.Resp) baseResp;
//                GetUserInfo(((SendAuth.Resp) baseResp).code);
                break;
                //取消或者失败
                default:
                    break;
        }
    }
//    private void GetUserInfo(String code){
//        String accessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Constants.APP_ID
//                + "&secret=" + Constants.APP_SECRET + "&code=" + code + "&grant_type=authorization_code";
//        WeiXinPresenter.wxLoginPresenter(accessTokenUrl, new ILoadDataUIRunnadle() {
//            @Override
//            public boolean onPreRun() {
//                return false;
//            }
//
//            @Override
//            public void onPostRun(Object... params) {
//                if (params[0] instanceof WechatBack) {
//                    WechatBack wechatBack = (WechatBack) params[0];
//                    if (wechatBack.getAccessToken() != null && wechatBack.getOpenid() != null) {
//                        UnityPlayer.UnitySendMessage("MainCamera", "WEloginCallBack", wechatBack.getAccessToken() + "#" + wechatBack.getOpenid());
//                    }
//                }
//            }
//
//            @Override
//            public void onFailUI(Object... params) {
//            }
//        });
//    }
}
