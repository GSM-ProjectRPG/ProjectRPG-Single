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

    private void Awake()
    {
        InventoryUI.GetComponent<InventoryUI>().OnClickAction = Use;
    }

    public void OpenInventory()
    {
        InventoryUI.SetActive(true);
        EquipmentWindowUI.SetActive(true);
        GetComponentInChildren<InventoryUI>().OnClickAction = Use;
    }

    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
        EquipmentWindowUI.SetActive(false);
    }

    public void AddItemData(Item compareItem)
    {
        foreach (Item sourceItem in ItemList)
        {
            if (sourceItem.itemData.itemID == compareItem.itemData.itemID)
            {
                sourceItem.count += compareItem.count;
                return;
            }
        }

        if (ItemList.Count >= 16)
        {
            return;
        }

        ItemList.Add(compareItem);
        CheckQuest?.Invoke();
    }

    public void ReduceItemData(Item compareItem)
    {
        foreach (Item sourceItem in ItemList)
        {
            if (sourceItem.itemData.itemID == compareItem.itemData.itemID)
            {
                if (sourceItem.count > compareItem.count)
                {
                    sourceItem.count -= compareItem.count;
                    return;
                }
                else if (sourceItem.count == compareItem.count)
                {
                    ItemList.Remove(sourceItem);
                    return;
                }
                else if (sourceItem.count < compareItem.count)
                {
                    return;
                }
            }
        }
        CheckQuest?.Invoke();
    }

    public void Use(int index)
    {
        if (ItemList.Count <= index) return;
        if (ItemList[index] is null) return;

        if (ItemList[index].itemData is Equipment)
        {
            OnEquip?.Invoke(ItemList[index]);
        }
        else if (ItemList[index].itemData is Use)
        {
            OnUse?.Invoke(ItemList[index]);
        }
    }
}