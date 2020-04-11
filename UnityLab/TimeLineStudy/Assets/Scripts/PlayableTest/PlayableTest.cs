using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
public class PlayableTest : PlayableBehaviour
{
    public Text testText;
    public string testStr;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (testText != null)
        {
            testText.gameObject.SetActive(true);
            testText.text = testStr;
        }
    }
    /// <summary>
    /// 在开始和播放完后会调用
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //这里筛选开始的
        if (info.deltaTime==0)
        {
            return;
        }
        if (testText != null)
        {
            testText.gameObject.SetActive(false);
        }
        UnityEngine.Debug.Log(playable);
        UnityEngine.Debug.Log(info);
        UnityEngine.Debug.Log("OnBehaviourPause");
    }
    public override void OnBehaviourDelay(Playable playable, FrameData info){
        UnityEngine.Debug.Log("OnBehaviourDelay");
    }
    public override void OnGraphStop(Playable playable) { 
        UnityEngine.Debug.Log("OnGraphStop");
    }
}
