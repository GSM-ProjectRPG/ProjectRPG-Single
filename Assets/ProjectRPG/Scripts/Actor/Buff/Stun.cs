using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : TimerBuff<Stun>
{
    public float endTime;
    private Animator animator;

    public Stun(BuffData buffData, float stunTime) : base(buffData, stunTime)
    {
        endTime = Time.time + stunTime;
    }

    public override void MergeBuff(Stun other)
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
        if (endTime < Time.time)
        {
            manager.RemoveBuff<Stun>();
        }
    }

    public override void OnDeleted(BuffSystem manager)
    {
    }
}