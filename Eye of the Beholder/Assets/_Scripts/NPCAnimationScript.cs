using UnityEngine;
using System.Collections;

public class NPCAnimationScript : MonoBehaviour
{

    public Animation AinObjs;
    public AnimationClip[] clips;
    public int CurAnimClip = 1;
    bool first = true;
    
    void Start()
    {
        StandAnimation();
    }
    public void SwapAnimation()
    {
        CurAnimClip++;
        CurAnimClip %= clips.Length;
        PlayAnim();
    }

    void PlayAnim()
    {
        AinObjs.Play(clips[CurAnimClip].name);
    }

    void Update()
    {
        if (first)
        {
            PlayAnim();
            first = false;
        }
    }
    public void WalkAnimation()
    {
        if (CurAnimClip != 0)
        {
            CurAnimClip = 0;
            first = true;
        }
    }
    public void StandAnimation()
    {
        if (CurAnimClip != 1)
        {
            CurAnimClip = 1;
            first = true;
        }
    }


}
