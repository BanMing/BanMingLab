using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Dome : MonoBehaviour
{
    public PlayableDirector director;

    public Text text;
    private int addNum;
    void OnGUI()
    {   
        //动态添加设置
        if (GUILayout.Button("ss", GUILayout.Width(100), GUILayout.Height(100)))
        {   
            //遍历所有
            foreach (var item in director.playableAsset.outputs)
            {
                // Debug.Log(item.sourceObject.GetType()+"  "+item.streamName);
                //找到PlayableTrack轨道
                if (item.sourceObject.GetType()==typeof(PlayableTrack))
                {
                    //遍历该轨道上的段落
                    var playableTrack=(PlayableTrack)item.sourceObject;
                    foreach (var clip in playableTrack.GetClips())
                    {   //找到我们制定的方法脚本
                        if (clip.asset is PlayableAssetTest)
                        {
                            Debug.Log(clip.asset);
                            var test= clip.asset as PlayableAssetTest;
                            director.SetReferenceValue(test.testText.exposedName,text);
                        }
                        
                    }
                }
                
                // 
                // Debug.Log(item.);
                director.Play();
            }
        }
    }

}
