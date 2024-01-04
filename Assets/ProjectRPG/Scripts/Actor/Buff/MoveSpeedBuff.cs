using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuff : TimerBuff<MoveSpeedBuff>
{
    protected override BuffData _buffData => ResourceManager.ResourceBindingData.MoveSpeedBuffData;

    private float _endTime;

    public MoveSpeedBuff(float time) : base(time)
    {
    }

    public override void OnAdded(BuffSystem manager)
    {

    }

    public override void OnDeleted(BuffSystem manager)
    {

    }

    public override void MergeBuff(MoveSpeedBuff other)
    {
        _endTime = Mathf.Max(_endTime, other._endTime);
    }
}
