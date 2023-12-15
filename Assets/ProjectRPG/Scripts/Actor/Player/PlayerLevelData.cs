using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "플레이어 레벨별 데이터", menuName = "Scriptable Object/플레이어 레벨별 데이터", order = int.MinValue)]
public class PlayerLevelData : ScriptableObject
{
    private Level[] LevelDatas;

    [Serializable]
    public struct Level
    {
        public int maxHp;
        public float attack;
        public float totalExp;
    }

    public Level GetLevelData(int level)
    {
        return LevelDatas[Mathf.Min(level - 1, LevelDatas.Length - 1)];
    }
}