using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TimerBuff
{
    public float RemainingTime { get; }
    public float RemainingTimeRate { get; }
}

public abstract class TimerBuff<T> : Buff<T>, TimerBuff where T : TimerBuff<T>
{
    public float RemainingTime { get; protected set; }
    public float RemainingTimeRate => 1 - RemainingTime / _originTime;

    private float _originTime;

    public TimerBuff(float remainingTime)
    {
        _originTime = remainingTime;
        RemainingTime = remainingTime;
    }

    public override void OnUpdate(BuffSystem manager)
    {
        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
        {
            manager.RemoveBuff<T>();
        }
    }
}
