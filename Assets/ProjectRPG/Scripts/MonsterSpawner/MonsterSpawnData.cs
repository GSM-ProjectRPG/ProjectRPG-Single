using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "스폰 풀", menuName = "Scriptable Object/스폰 풀", order = int.MinValue)]
public class MonsterSpawnData : ScriptableObject
{
    public MonsterSpawnRate[] SpawnRates;

    public GameObject GetRandomMonsterPrefab()
    {
        float sum = 0;
        for (int i = 0; i < SpawnRates.Length; i++)
        {
            sum += SpawnRates[i].SpawnRate;
        }

        float rand = Random.Range(0f, sum);
        for (int i = 0; i < SpawnRates.Length; i++)
        {
            rand -= SpawnRates[i].SpawnRate;
            if (rand < 0f)
            {
                return SpawnRates[i].MonsterPrefab;
            }
        }

        return null;
    }
}

[Serializable]
public struct MonsterSpawnRate
{
    public float SpawnRate;
    public GameObject MonsterPrefab;
}