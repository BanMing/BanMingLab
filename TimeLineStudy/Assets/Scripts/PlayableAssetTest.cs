using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine;

public class PlayableAssetTest : PlayableAsset
{
    public ExposedReference<Text> testText;
    public string testStr;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PlayableTest>.Create(graph);
        playable.GetBehaviour().testText = testText.Resolve(graph.GetResolver());
        playable.GetBehaviour().testStr = testStr;
        return playable;
    }
}