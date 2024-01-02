using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKBuff : Buff
{
    public override string Name => "날카로운 칼날";
    public override string Description => "공격력이 20% 증가합니다.";
    public override Sprite Sprite => null;

    private float _endTime;

    public ATKBuff(float time)
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
        ATKBuff otherBuff = other as ATKBuff;
        _endTime = Mathf.Max(_endTime, otherBuff._endTime);
    }
}
