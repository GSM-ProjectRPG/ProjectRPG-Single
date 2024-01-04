using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKBuff : TimerBuff<ATKBuff>
{
    protected override BuffData _buffData => ResourceManager.ResourceBindingData.DamageBuffData;

    private float _endTime;

    public ATKBuff(float time) : base(time)
    {
        _endTime = Time.time + time;
    }

    public override void OnAdded(BuffSystem manager)
    {

    }

    public override void OnDeleted(BuffSystem manager)
    {

    }

    public override void MergeBuff(ATKBuff other)
    {
        _endTime = Mathf.Max(_endTime, other._endTime);
    }
}
