using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine;

/// <summary>
/// 事件生命周期类
/// </summary>
public class PlayableEvent : PlayableBehaviour
{
    public PlayableEventId eventId;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }

    /// <summary>
    /// 这里激活事件
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (info.deltaTime == 0)
        {
            return;
        }
        //激活事件
    }
}