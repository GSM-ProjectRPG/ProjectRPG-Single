using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuff : Buff
{
    public override string Name => "가벼운 발걸음";
    public override string Description => "이동속도가 30% 증가합니다.";
    public override Sprite Sprite => null;

    private float _endTime;

    public MoveSpeedBuff(float time)
    {
        _endTime = Time.time + time;
    }

    public override void OnAdded(BuffSystem manager)
    {

    }

    public override void OnUpdate(BuffSystem manager)
    {
        if (_endTime < Time.time)
        {
            manager.RemoveBuff<ATKBuff>();
        }
    }

    public override void OnDeleted(BuffSystem manager)
    {

    }

    public override void MergeBuff(Buff other)
    {
        var otherBuff = other as MoveSpeedBuff;
        _endTime = Mathf.Max(_endTime, otherBuff._endTime);
    }
}
