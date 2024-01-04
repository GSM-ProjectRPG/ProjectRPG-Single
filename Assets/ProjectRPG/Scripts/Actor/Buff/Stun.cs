using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : TimerBuff<Stun>
{
    protected override BuffData _buffData => ResourceManager.ResourceBindingData.StunData;

    private Animator animator;

    public Stun(float stunTime) : base(stunTime)
    {
        
    }

    public override void MergeBuff(Stun other)
    {
    }

    public override void OnAdded(BuffSystem manager)
    {
        animator = manager.GetComponentInChildren<Animator>();
        animator?.SetInteger("MoveMode", -1);
    }

    public override void OnDeleted(BuffSystem manager)
    {
    }
}