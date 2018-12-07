using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine;

/// <summary>
/// 事件数据类
/// </summary>
public class PlayableEventAsset : PlayableAsset
{
    public PlayableEventId eventId;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {

        var playable = ScriptPlayable<PlayableEvent>.Create(graph);
        playable.GetBehaviour().eventId = eventId;//plaeventId
        return playable;
    }
}