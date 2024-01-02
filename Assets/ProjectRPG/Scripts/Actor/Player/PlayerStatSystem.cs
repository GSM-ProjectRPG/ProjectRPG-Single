using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatSystem : StatSystem
{
    public Action OnLevelUp;
    /// <summary>
    /// float : 더해진 exp
    /// </summary>
    public Action<float> OnAddedExp;

    //플레이어 데이터 저장 기능 만들면 수정필요
    public int Level { get; protected set; } = 1;
    public float Exp { get; protected set; } = 0;
    public float NeededExp => levelData.GetLevelData(Level).totalExp;

    [SerializeField] protected PlayerLevelData levelData;

    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnEquipItemAction += () => OnStatChanged?.Invoke();
        _inventory.UnEquipItemAction += () => OnStatChanged?.Invoke();
    }

    public override Stat GetCurruntStat()
    {
        Stat stat = _inventory.GetTotalStat();
        stat.Health += levelData.GetLevelData(Level).maxHp;
        stat.Attack += levelData.GetLevelData(Level).attack;
        stat.MoveSpeed += MoveSpeed;
        return stat;
    }

    public void AddExp(float exp)
    {
        Exp += exp;
        OnAddedExp?.Invoke(exp);
        while (Exp >= NeededExp)
        {
            Exp -= NeededExp;
            Level++;
            OnLevelUp?.Invoke();
        }
    }
}