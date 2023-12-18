using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    public ItemData itemData;

    public Action<Item> OnEquip;
    public Action<Item> OnUse;

    public Image[] inventoryImg;
    public Text[] inventoryTxt;

    private void Update()
    {
        for (int i = 0; i < 16; i++)
        {
            if (i < itemList.Count)
            {
                inventoryImg[i].sprite = itemList[i].itemData.itemImg;
                inventoryTxt[i].text = itemList[i].itemData.itemName;
            }
            else
            {
                inventoryImg[i].sprite = null;
                inventoryTxt[i].text = null;
            }
        }
    }

    public void AddItemData(Item compareItem)
    {
        foreach (Item sourceItem in itemList)
        {
            if (sourceItem.itemData.itemID == compareItem.itemData.itemID)
            {
                sourceItem.count += compareItem.count;
                return;
            }
        }

        if (itemList.Count >= 16)
        {
            return;
        }

        itemList.Add(compareItem);
    }

    public void RemoveItemData(Item item)
    {
        foreach (Item data in itemList)
        {
            if (data.itemData.itemID == item.itemData.itemID)
            {
                if(data.count - item.count > 0)
                {
                    data.count -= item.count;
                    return;
                }
                else if(data.count - item.count == 0)
                {
                    itemList.Remove(item);
                    return;
                }
                else
                {
                    return;
                }
            }
        }
    }
    
    public void Use(int index)
    {
        if (itemList[index] is null) return;
        if (index > itemList.Count) return;

        if (itemList[index].itemData is Equipment)
        {
            OnEquip?.Invoke(itemList[index]);
        }
        else if (itemList[index].itemData is Use)
        {
            OnUse?.Invoke(itemList[index]);
        }
    }
}