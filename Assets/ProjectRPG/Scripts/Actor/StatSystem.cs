using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public string Name;
    public float Attack;
    public float Health;
    public float MoveSpeed;

    private BuffSystem _buffSystem;

    private void Awake()
    {
        _buffSystem = GetComponent<BuffSystem>();
    }

    public virtual Stat GetCurruntStat()
    {
        Stat stat = new Stat();
        stat.Health = Health;
        stat.Attack = Attack;
        stat.MoveSpeed = MoveSpeed;

        if (_buffSystem.ContainsBuff<ATKBuff>())
        {
            stat.Attack *= 1.2f;
        }

        return stat;
    }
}

public struct Stat
{
    public float Health;
    public float Attack;
    public float MoveSpeed;
}