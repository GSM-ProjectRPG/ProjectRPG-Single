using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventoryView;
    public GameObject EquipmentView;
    public GameObject CoinView;

    public ShopUI ShopUI;
    public Inventory Inventory;

    public Action<int> OnClickInventoryAction;
    public Action<int> OnClickEquipmentAction;

    public Sprite NullImg;

    public Image[] InventoryImg;
    public Text[] InventoryTxt;

    public Image[] EquipmentImg;
    public Text[] EquipmentTxt;

    public bool isOpenInventory;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
    }

    public void OpenInventory()
    {
        if (ShopUI.isOpenShop) return;
        InventoryView.SetActive(true);
        EquipmentView.SetActive(true);
        CoinView.SetActive(true);
        PlayerInputManager.Instance.MouseLock = false;
        OnClickInventoryAction = Inventory.UseItem;
        isOpenInventory = true;
    }

    public void CloseInventory()
    {
        if (ShopUI.isOpenShop) return;
        InventoryView.SetActive(false);
        EquipmentView.SetActive(false);
        CoinView.SetActive(false);
        PlayerInputManager.Instance.MouseLock = true;
        isOpenInventory = false;
    }

    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
        SetInventory();
        Inventory.OnAddItemAction += (_) =>
        {
            SetInventory();
        };

        Inventory.OnReduceItemAction += (_) =>
        {
            SetInventory();
        };
        Inventory.OnEquipItemAction += SetEquipment;
        Inventory.UnEquipItemAction += SetEquipment;
        OnClickEquipmentAction = Inventory.UnEquipItem;
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

    private void SetEquipment()
    {
        for (int i = 0; i < Inventory.EquipmentList.Count; i++)
        {
            if (Inventory.EquipmentList[i] is not null)
                EquipmentImg[i].sprite = Inventory.EquipmentList[i].ItemImg;
            else
                EquipmentImg[i].sprite = NullImg;
        }
    }

    public void OnClickInventory(int index)
    {
        OnClickInventoryAction?.Invoke(index);
    }

    public void OnClickEquipment(int index)
    {
        OnClickEquipmentAction?.Invoke(index);
    }
}
