using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKBuff : TimerBuff<ATKBuff>
{
    private float _endTime;

    public ATKBuff(BuffData buffData, float time) : base(buffData, time)
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
