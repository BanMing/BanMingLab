using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStart : MonoBehaviour
{

    private  NetFramework.NetworkManager networkManager;

    public string ip="127.0.0.1";

    public string port="8888";

    void Start()
    {
		networkManager=new GameObject("NetworkManager").AddComponent<NetFramework.NetworkManager>();
    }


    void Update()
    {

    }

    void OnGUI()
    {
		ip=GUILayout.TextField(ip,GUILayout.Height(30));
		port=GUILayout.TextField(port,GUILayout.Height(30));
		if (GUILayout.Button("Connect Server"))
		{
			if (string.IsNullOrEmpty(ip)||string.IsNullOrEmpty(port))
			{
				return;
			}
			networkManager.SendConnect(ip,int.Parse(port));
		}
    }
}
