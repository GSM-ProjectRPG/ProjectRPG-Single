using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public static ActorManager Instance { get; private set; }

    public Action<GameObject> OnRegistedActor;
    public Action<GameObject> OnDeletedActor;
    public Action OnRegistedPlayer;
    public Action OnDeletedPlayer;

    public GameObject Player { get; set; }

    public List<GameObject> Actors { get; private set; } = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
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
            
            if(actor == Player)
            {
                Player = null;
                OnDeletedPlayer?.Invoke();
            }
        }
    }
}
