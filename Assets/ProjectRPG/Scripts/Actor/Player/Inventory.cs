using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> ItemList = new List<Item>();

    public Action<Item> OnEquip;
    public Action<Item> OnUse;
    public Action<Item> OnAddItem;
    public Action<Item> OnReduceItem;

    public GameObject InventoryUI;
    public GameObject EquipmentWindowUI;

    public ItemData itemData;
    public ItemData itemData1;

    public void OpenInventory()
    {
        InventoryUI.SetActive(true);
        EquipmentWindowUI.SetActive(true);
        PlayerInputManager.Instance.MouseLock = false;
        GetComponentInChildren<InventoryUI>().OnClickAction = Use;
    }

    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
        EquipmentWindowUI.SetActive(false);
        PlayerInputManager.Instance.MouseLock = true;
    }

    public void AddItemData(Item compareItem)
    {
        bool add = true;
        foreach (Item sourceItem in ItemList)
        {
            if (sourceItem.ItemID == compareItem.ItemID)
            {
                add = false;
                sourceItem.count += compareItem.count;
            }
        }

        if (ItemList.Count >= 16)
        {
            return;
        }

        if(add) ItemList.Add(compareItem);
        OnAddItem?.Invoke(compareItem);
    }

    public void ReduceItemData(Item compareItem)
    {
        foreach (Item sourceItem in ItemList)
        {
            if (sourceItem.ItemID == compareItem.ItemID)
            {
                if (sourceItem.count > compareItem.count)
                {
                    sourceItem.count -= compareItem.count;
                }
                else if (sourceItem.count == compareItem.count)
                {
                    ItemList.Remove(sourceItem);
                    break;
                }
                else if (sourceItem.count < compareItem.count)
                {

                }
            }
        }
        OnReduceItem?.Invoke(compareItem);
    }

    public void Use(int index)
    {
        if (ItemList.Count <= index) return;
        if (ItemList[index] is null) return;

        if (ItemList[index].ItemType is ItemType.Weapon || ItemList[index].ItemType is ItemType.Accessory)
        {
            OnEquip?.Invoke(ItemList[index]);
        }
        else if (ItemList[index].ItemType is ItemType.Use)
        {
            OnUse?.Invoke(ItemList[index]);
        }
    }
}