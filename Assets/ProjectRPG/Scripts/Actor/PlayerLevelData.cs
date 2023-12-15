using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "플레이어 레벨별 데이터", menuName = "Scriptable Object/플레이어 레벨별 데이터", order = int.MinValue)]
public class PlayerLevelData : ScriptableObject
{
    public Level[] levelDatas;

    [Serializable]
    public struct Level
    {
        public int maxHp;
        public float attack;
        public float totalExp;
    }

    public void SetAttack()
    {
        for (int i = 0; i < levelDatas.Length; i++)
        {
            levelDatas[i].attack = levelDatas[i].maxHp / 10;
        }
    }

    public Level GetLevelData(int level)
    {
        return levelDatas[Mathf.Min(level - 1, levelDatas.Length - 1)];
    }
}