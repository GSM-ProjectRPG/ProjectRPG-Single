using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : SceneUI
{
    public InventoryUI InventoryUI;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            ActorManager.Instance.Player.GetComponent<PlayerController>().InventoryUI = InventoryUI;
        };
    }
}
