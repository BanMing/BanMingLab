/******************************************************************
** AsyncTest.cs
** @Author       : BanMing 
** @Date         : 10/19/2020 2:07:37 PM
** @Description  : 
*******************************************************************/

using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class AsyncTest : MonoBehaviour
{
    public async void WaitSecond()
    {
        Debug.Log("$$$$$$$$$$");

        await Task.Delay(1000);

        Debug.Log("@@@@@@@@@@");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("sss"))
        {
            WaitSecond();
        }
    }

}
