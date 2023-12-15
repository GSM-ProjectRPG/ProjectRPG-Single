using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : StatManager
{
    public Action OnLevelUp;
    /// <summary>
    /// float : 더해진 exp
    /// </summary>
    public Action<float> OnAddedExp;

    //플레이어 데이터 저장 기능 만들면 수정필요
    public int Level { get; protected set; } = 1;
    public float Exp { get; protected set; } = 0;

    [SerializeField] protected PlayerLevelData levelData;

    public override Stat GetCurruntStat()
    {
        Stat stat = new Stat();
        stat.Health = levelData.GetLevelData(Level).maxHp;
        stat.Attack = levelData.GetLevelData(Level).attack;
        stat.MoveSpeed = MoveSpeed;
        return stat;
    }

    public void AddExp(float exp)
    {
        Exp += exp;
        OnAddedExp?.Invoke(exp);
        while (Exp >= levelData.GetLevelData(Level).totalExp)
        {
            Exp -= levelData.GetLevelData(Level).totalExp;
            Level++;
            OnLevelUp?.Invoke();
        }
    }
}