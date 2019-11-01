using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAnimation : MonoBehaviour
{
    [SpineAnimation()]
    public string idle, start;
    public SkeletonGraphic skeleton;

    private void Start()
    {
        skeleton.AnimationState.Complete += OnComplete;
        StartAnim();
    }

  /*  private void OnEnable()
    {
        skeleton.AnimationState.Complete += OnComplete;
        StartAnim();
    }*/

    private void OnComplete(TrackEntry trackEntry)
    {
        string animName = trackEntry.animation.name;

        if(animName.Equals(start))
        {
            IdleAnim();
        }
    }
    
    public void IdleAnim()
    {
        skeleton.AnimationState.ClearTrack(0);
        skeleton.AnimationState.SetAnimation(0, idle, true);
    }
    public void StartAnim()
    {
        skeleton.AnimationState.ClearTrack(0);
        skeleton.AnimationState.SetAnimation(0, start, false);
    }
}
