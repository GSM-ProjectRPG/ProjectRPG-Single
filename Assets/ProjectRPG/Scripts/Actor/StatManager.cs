using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public string Name;
    public float Attack;
    public float Health;
    public float MoveSpeed;

    public virtual Stat GetCurruntStat()
    {
        Stat stat = new Stat();
        stat.Health = Health;
        stat.Attack = Attack;
        stat.MoveSpeed = MoveSpeed;
        return stat;
    }
}

public struct Stat
{
    public float Health;
    public float Attack;
    public float MoveSpeed;
}