using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconUILayer : MonoBehaviour
{
    public GameObject BuffIconPrefab;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            BuffSystem buffSystem = ActorManager.Instance.Player.GetComponent<BuffSystem>();
            
            
        };
    }
}
