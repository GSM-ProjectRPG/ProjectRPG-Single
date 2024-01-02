using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("스폰")]
    public MonsterSpawnData SpawnData;
    public float SpawnMonsterCool;
    public int MaxMonsterCount;
    [Header("스폰 범위 관련 설정")]
    public bool MonsterCanEscapeArea;
    public float SpawnMonsterRange;
    [Header("맵 터레인")]
    public Terrain terrain;

    private List<GameObject> _monsters = new();
    private float _curruntSpawnDelay;

    private void Update()
    {
        _curruntSpawnDelay -= Time.deltaTime;
        if (_monsters.Count < MaxMonsterCount)
        {
            if (_curruntSpawnDelay <= 0)
            {
                _curruntSpawnDelay = SpawnMonsterCool;
                HandleSpawn();
            }
        }
        for (int i = 0; i < _monsters.Count; i++)
        {
            if (!MonsterCanEscapeArea && Vector3.Distance(GetTerrainPos(0, 0), _monsters[i].transform.position) > SpawnMonsterRange)
            {
                MeshFilter filter = _monsters[i].GetComponent<MeshFilter>();
                if (filter != null)
                {
                    Collider[] cols;
                    Vector3 rand;
                    do
                    {
                        rand = GetRandomSpawnPos();
                        cols = Physics.OverlapBox(rand, filter.sharedMesh.bounds.size);
                    } while (cols.Length == 0);
                    _monsters[i].transform.position = rand;
                }
                else
                {
                    Vector3 rand = GetRandomSpawnPos();
                    _monsters[i].transform.position = rand;
                }
            }
        }
    }

    private void HandleSpawn()
    {
        GameObject g = Instantiate(SpawnData.GetRandomMonsterPrefab(), GetRandomSpawnPos(), Quaternion.identity);
        _monsters.Add(g);
        Health h = g.GetComponent<Health>();
        if (h != null)
        {
            h.OnDead += (_) => _monsters.Remove(g);
        }
    }

    private Vector3 GetRandomSpawnPos()
    {
        return GetTerrainPos(Random.Range(0f, 360f), Random.Range(0f, SpawnMonsterRange));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Vector3 v = GetTerrainPos(0, 0);
        for (int i = 0; i < 360; i++)
        {
            Vector3 c = GetTerrainPos(i, SpawnMonsterRange);
            Gizmos.DrawLine(v, c);
            v = c;
        }
    }

    Vector3 GetTerrainPos(float degree, float distance)
    {
        Vector2 addedPos = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)) * distance;
        Vector2 centorPos = new(transform.position.x, transform.position.z);
        Vector3 pos = new Vector3(centorPos.x + addedPos.x,
            terrain.SampleHeight(transform.position + new Vector3(addedPos.x, 0, addedPos.y)) + terrain.transform.position.y,
            centorPos.y + addedPos.y);
        return pos;
    }
}