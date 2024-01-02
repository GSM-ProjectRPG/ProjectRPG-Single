using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuff : TimerBuff<MoveSpeedBuff>
{
    private float _endTime;

    public MoveSpeedBuff(BuffData buffData, float time) : base(buffData, time)
    {
    }

    public override void OnAdded(BuffSystem manager)
    {

    }

    public override void OnUpdate(BuffSystem manager)
    {
        if (_endTime < Time.time)
        {
            manager.RemoveBuff<MoveSpeedBuff>();
        }
    }

    public override void OnDeleted(BuffSystem manager)
    {

    }

    public override void MergeBuff(MoveSpeedBuff other)
    {
        _endTime = Mathf.Max(_endTime, other._endTime);
    }
}
