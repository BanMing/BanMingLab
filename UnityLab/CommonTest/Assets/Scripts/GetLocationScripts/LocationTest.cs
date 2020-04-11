using System;
using System.Collections;
using UnityEngine;

public class location : MonoBehaviour
{
    private const float EARTH_RADIUS = 6378.137f;
    private string anotherAltitude = "0";
    private string anotherLatitude = "0";

    /// <summary>
    ///     获得本机经纬度
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        //先判断是否打开了定位
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("没有打开定位");
            yield break;
        }
        //这里开起定位计算
        Input.location.Start();
        //时间为20秒
        var maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            Debug.Log("时间到");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("无法使用定位");
            yield break;
        }
        Debug.Log("经纬度: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " +
                  Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " +
                  Input.location.lastData.timestamp);
        Input.location.Stop();
    }

    private void OnGUI()
    {
        GUILayout.Label("本机纬度：" + Input.location.lastData.latitude + "本机经度：" + Input.location.lastData.longitude +
                        "本机海拔： " + Input.location.lastData.altitude +
                        "水平精确度" + Input.location.lastData.horizontalAccuracy + "查询时间点：" +
                        Input.location.lastData.timestamp);
        GUILayout.BeginHorizontal();
        GUILayout.Label("另一个经度：");
        anotherLatitude = GUILayout.TextField(anotherLatitude);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("另一个纬度：");
        anotherAltitude = GUILayout.TextField(anotherAltitude);
        GUILayout.EndHorizontal();
        GUILayout.Label("算法1距离为" +
                        GetDistance1(Input.location.lastData.latitude, float.Parse(anotherLatitude),
                            Input.location.lastData.altitude, float.Parse(anotherAltitude)) + "Km");
        GUILayout.Label("算法2距离为" +
                        GetDistance2(Input.location.lastData.latitude, float.Parse(anotherLatitude),
                            Input.location.lastData.altitude, float.Parse(anotherAltitude)) + "Km");
    }

    private float GetDistance1(float la1, float la2, float al1, float al2)
    {
        var radLat1 = Rad(la1);
        var radLat2 = Rad(la2);
        var a = radLat1 - radLat2;
        var b = Rad(al1) - Rad(al2);
        var s = 2*
                Math.Asin(
                    Math.Sqrt(Math.Pow(Math.Sin(a/2), 2) +
                              Math.Cos(radLat1)*Math.Cos(radLat2)*Math.Pow(Math.Sin(b/2), 2)));
        var dis = s*EARTH_RADIUS;
        return (float) dis;
    }

    private float GetDistance2(float la1, float la2, float al1, float al2)
    {
        var radLat1 = Rad(la1);
        var radLat2 = Rad(la2);
        var radLon1 = Rad(al1);
        var radLon2 = Rad(al2);
        if (radLat1 < 0)
        {
            radLat1 = (float) (Math.PI/2 + Math.Abs(radLat1)); //south
        }
        if (radLat1 > 0)
        {
            radLat1 = (float) (Math.PI/2 - Math.Abs(radLat1)); //north
        }
        if (radLat1 < 0)
        {
            radLon1 = (float) (Math.PI/2 - Math.Abs(radLon1)); //west
        }
        if (radLat2 < 0)
        {
            radLat2 = (float) (Math.PI/2 + Math.Abs(radLat2)); //south
        }
        if (radLat2 > 0)
        {
            radLat2 = (float) (Math.PI/2 - Math.Abs(radLat2)); //north
        }
        if (radLon2 < 0)
        {
            radLon2 = (float) (Math.PI/2 - Math.Abs(radLat2)); //west
        }
        var x1 = EARTH_RADIUS*Math.Cos(radLon1)*Math.Sin(radLat1);
        var y1 = EARTH_RADIUS*Math.Sin(radLon1)*Math.Sin(radLat1);
        var z1 = EARTH_RADIUS*Math.Cos(radLat1);

        var x2 = EARTH_RADIUS*Math.Cos(radLon2)*Math.Sin(radLat2);
        var y2 = EARTH_RADIUS*Math.Sin(radLon2)*Math.Sin(radLat2);
        var z2 = EARTH_RADIUS*Math.Cos(radLat2);

        var d = Math.Sqrt((x1 - x2)*(x1 - x2) + (y1 - y2)*(y1 - y2) + (z1 - z2)*(z1 - z2));
        var theta =
            Math.Acos((EARTH_RADIUS*EARTH_RADIUS + EARTH_RADIUS*EARTH_RADIUS - d*d)/(2*EARTH_RADIUS*EARTH_RADIUS));
        //var dist = theta*EARTH_RADIUS;
        return (float) theta;
    }

    private float Rad(float d)
    {
        return (float) (d*Math.PI/180.0f);
    }
}