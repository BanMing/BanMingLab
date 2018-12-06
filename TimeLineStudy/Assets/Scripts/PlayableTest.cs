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
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (testText != null)
        {
            testText.gameObject.SetActive(false);
        }
    }
}
