using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatSystem : StatSystem
{
    public float AttackSpeed { get; private set; }
    public float AttackRange { get; private set; }
    public float ChaseRange { get; private set; }
    public float DropExp { get; private set; }

    public MonsterStatData _statData;

    private void Awake()
    {
        Name = _statData.Name;
        Health = _statData.Health;
        Attack = _statData.Attack;
        MoveSpeed = _statData.MoveSpeed;
        AttackSpeed = _statData.AttackSpeed;
        AttackRange = _statData.AttackRange;
        ChaseRange = _statData.ChaseRange;
        DropExp = _statData.DropExp;
    }
}
