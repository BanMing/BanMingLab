﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCenter : MonoBehaviour
{
    LuaManager luaManager = null;
    static public GameCenter Instance;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //UnityEngine.Profiler.maxNumberOfSamplesPerFrame = 8096000;
        Application.runInBackground = true;
        // 关闭锁屏
        UnityEngine.Screen.sleepTimeout = -1;
        try
        {
            Debug.Log("GlobalManager.Start");

            //LoadingBackground.Instance.SetVisible(true);
            //显示第一个界面
            UIWindowFirstLoading.Instance.SetTargetProgress(UIWindowFirstLoading.StartProgressValue);

            //检查网络是否可以访问
            CheckNetworkState(Init);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


    void Update()
    {

    }


    void Init()
    {
        System.Action<bool> updateFinish = delegate (bool result)
        {
            UIWindowUpdate.Close();
            UIWindowFirstLoading.Show();
            Debug.Log("GlobalManager.Init:检查更新结束");
            InitResManager();
        };

        if (true)//SystemConfig.Instance.IsAutoUpdate)
        {
			
            // UIManager.Instance.OpenWindow("PanelUpdate");
            //VersionManager.Instance.UpdateGame(updateFinish);
            UIWindowFirstLoading.Hide();

            Debug.Log("GlobalManager.Init:开始检查更新");
            VersionManager.Instance.UpdateGame(updateFinish);
        }
        else
        {
            UIWindowUpdate.Close();
            UIWindowFirstLoading.Show();
            InitResManager();
        }

        Debug.Log("GlobalManager.Init");
    }

    void InitResManager()
    {
        Action<bool> initCB = delegate (bool result)
        {
            if (result)
            {
                Debug.Log("GlobalManager.InitResManager:资源系统初始化成功");
            }
            else
            {
                Debug.LogError("GlobalManager.InitResManager:资源系统初始化失败");
            }

            UIWindowFirstLoading.Instance.SetTargetProgress(UIWindowFirstLoading.FinishResProgressValue);

            //初始化图集工具
            // UIAtlasTool.Instance.Init(() =>
            // {
                InitLuaManager();
            // });

        };

        UIWindowFirstLoading.Instance.SetTargetProgress(UIWindowFirstLoading.InitResProgressValue);
        ResourcesManager.Instance.Init(initCB);
        Debug.Log("GlobalManager.InitResManager:资源系统开始初始化");
    }

    void InitLuaManager()
    {
        LuaManager.m_InitFinishCB = delegate ()
        {
            UIWindowFirstLoading.Instance.SetTargetProgress(UIWindowFirstLoading.FullProgressValue);
            //UIWindowFirstLoading.Close();
        };
        luaManager = gameObject.AddComponent<LuaManager>();

        Debug.Log("GlobalManager.InitLuaManager");
    }

    public LuaManager GetLuaManager()
    {
        return luaManager;
    }

    //--------------------------------------------------------------------------//

    //没有网络提示
    static public void CheckNetworkState(System.Action checkFinish)
    {
        Debug.Log("GlobalManager.CheckNetworkState");

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //没有网络
            System.Action<bool> clickAction = delegate (bool result)
            {
                if (result)
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        UITextTips.Instance.ShowText(LanguageConfig.GetText(12));
                    }
                    else
                    {
                        UIMsgBox.Instance.HideMsgBox();

                        if (checkFinish != null)
                        {
                            checkFinish();
                        }
                    }
                }
                else
                {
                    Application.Quit();
                }
            };
            UIMsgBox.Instance.ShowMsgBoxOKCancel(LanguageConfig.GetText(11), LanguageConfig.GetText(12), LanguageConfig.GetText(13), LanguageConfig.GetText(14), clickAction, false);
        }
        else
        {
            if (checkFinish != null)
            {
                checkFinish();
            }
        }
    }
}
