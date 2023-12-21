using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static ActorManager _instance;
    public static ActorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ActorManager>();
                if (_instance == null)
                {
                    GameObject g = new GameObject("ActorManager");
                    _instance = g.AddComponent<ActorManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    public Action<GameObject> OnRegistedActor;
    public Action<GameObject> OnDeletedActor;
    public Action OnRegistedPlayer;
    public Action OnDeletedPlayer;

    public GameObject Player { get; set; }

    public List<GameObject> Actors { get; private set; } = new();

    private void Awake()
    {
        if(Instance != this)
        {
            Debug.LogError("ActorManager가 2개 이상 존재합니다.\nGameObject : " + gameObject.name);
            Destroy(this);
        }
    }

    public void RegistActor(GameObject actor)
    {
        Actors.Add(actor);
        OnRegistedActor?.Invoke(actor);
    }

    public void RegistPlayer(GameObject player)
    {
        RegistActor(player);
        Player = player;
        OnRegistedPlayer?.Invoke();
    }

    public void DeleteActor(GameObject actor)
    {
        if (Actors.Contains(actor))
        {
            Actors.Remove(actor);
            OnDeletedActor?.Invoke(actor);

            if (actor == Player)
            {
                Player = null;
                OnDeletedPlayer?.Invoke();
            }
        }
    }
}
