using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public virtual string Name => _name;

    [SerializeField] protected string _name;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _health;
    [SerializeField] protected float _moveSpeed;

    public virtual Stat GetCurruntStat()
    {
        Stat stat = new Stat();
        stat.Health = _health;
        stat.Attack = _attack;
        stat.MoveSpeed = _moveSpeed;
        return stat;
    }
}

public struct Stat
{
    public float Health;
    public float Attack;
    public float MoveSpeed;
}