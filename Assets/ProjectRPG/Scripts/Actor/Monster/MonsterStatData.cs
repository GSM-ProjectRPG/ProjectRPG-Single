using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Stat Data", menuName = "Scriptable Object/Monster Stat Data")]
public class MonsterStatData : ScriptableObject
{
    [Header("Moster Stat Datas")]
    public string Name;
    public float Attack;
    public float Health;
    public float MoveSpeed;
    public float AttackSpeed;
    public float AttackRange;
    public float ChaseRange;
    public float DropExp;
}
