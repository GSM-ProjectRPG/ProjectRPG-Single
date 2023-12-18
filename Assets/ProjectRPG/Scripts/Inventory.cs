using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    public ItemData itemData;
    public Sprite testImg;

    public Action equipAction;
    public Action useAction;

    public Image[] inventoryImage;
    public Text[] inventoryText;
    public Text descriptionText;


    private void Update()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryImage[i].sprite = itemList[i].itemData.itemImg;
            inventoryText[i].text = itemList[i].itemData.itemName;
        }
    }

    public void AddItemData(Item item)
    {
        foreach (Item data in itemList)
        {
            if(itemList.Count > 16)
            {
                
            }    

            if (data.itemData.itemID == item.itemData.itemID)
            {
                data.count += item.count;
                return;
            }
        }
        itemList.Add(item);
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
                }
                else if(data.count - item.count == 0)
                {
                    itemList.Remove(item);
                }
                else
                {

                }
            }
        }
    }

    public void Use(int index)
    {
        if (itemList[index].itemData is Equipment)
        {
            equipAction?.Invoke();
        }
        else if (itemList[index].itemData is Use)
        {
            useAction?.Invoke();
        }
    }
}