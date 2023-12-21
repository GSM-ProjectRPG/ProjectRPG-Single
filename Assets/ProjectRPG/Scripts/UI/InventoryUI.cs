using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventoryView;
    public GameObject EquipmentView;
    public Inventory Inventory;

    public Action<int> OnClickAction;

    public Sprite NullImg;
    public Image[] InventoryImg;
    public Text[] InventoryTxt;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
        ActorManager.Instance.OnRegistedPlayer += OpenInventory;
    }

    public void OpenInventory()
    {
        InventoryView.SetActive(true);
        EquipmentView.SetActive(true);
        PlayerInputManager.Instance.MouseLock = false; 
        OnClickAction = Inventory.Use;
    }

    public void CloseInventory()
    {
        InventoryView.SetActive(false);
        EquipmentView.SetActive(false);
        PlayerInputManager.Instance.MouseLock = true;
    }

    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
        SetInventory();
        Inventory.OnAddItem += (_) =>
        {
            SetInventory();
        };

        Inventory.OnReduceItem += (_) =>
        {
            SetInventory();
        };
    }

    private void SetInventory()
    {
        for (int i = 0; i < 16; i++)
        {
            if (i < Inventory.ItemList.Count)
            {
                InventoryImg[i].sprite = Inventory.ItemList[i].ItemImg;
                InventoryTxt[i].text = Inventory.ItemList[i].count.ToString();
            }
            else
            {
                InventoryImg[i].sprite = NullImg;
                InventoryTxt[i].text = null;
            }
        }
    }

    public void OnClick(int index)
    {
        OnClickAction?.Invoke(index);
    }
}
