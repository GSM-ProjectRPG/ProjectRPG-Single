using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Buff
{
    public override string Name => "기절";
    public override string Description => "일정 시간동안 행동이 불가능합니다.";
    public override Sprite Sprite => null;

    public float endTime;
    private Animator animator;

    public Stun(float stunTime)
    {
        endTime = Time.time + stunTime;
    }

    public override void MergeBuff(Buff other)
    {
        endTime = MathF.Max(endTime, ((Stun)other).endTime);
    }

    public override void OnAdded(BuffSystem manager)
    {
        animator = manager.GetComponentInChildren<Animator>();
        animator?.SetInteger("MoveMode", -1);
    }

    public override void OnUpdate(BuffSystem manager)
    {
        if(endTime < Time.time)
        {
            manager.RemoveBuff<Stun>();
        }
    }

    public override void OnDeleted(BuffSystem manager)
    {
    }
}