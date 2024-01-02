using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform SpawnPoint;

    public GameObject PlayerPrefab;

    private GameObject _player;

    void Start()
    {
        HandleSpawn();
    }
    public IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        HandleSpawn();
    }

    private void HandleSpawn()
    {
        _player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<Health>().OnDead += (_) => { StartCoroutine(Spawn(2)); };
    }
}
