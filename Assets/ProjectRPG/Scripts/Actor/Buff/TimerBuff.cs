using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimerBuff<T> : Buff<T> where T : TimerBuff<T>
{
    public float RemainingTime { get; protected set; }

    public TimerBuff(BuffData buffData, float remainingTime) : base(buffData)
    {
        RemainingTime = remainingTime;
    }

    public override void OnUpdate(BuffSystem manager)
    {
        RemainingTime -= Time.deltaTime;
        if(RemainingTime <= 0)
        {
            manager.RemoveBuff<T>();
        }
    }
}
