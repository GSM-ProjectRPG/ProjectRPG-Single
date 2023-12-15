using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action equipAction;
    public Action useAction;

    public List<Item> itemList = new();

    public void AddItemData(Item itemData)
    {
        foreach (Item data in itemList)
        {
            if (data.itemData.itemID == itemData.itemData.itemID)
            {
                Countable countableResource = data as Countable;
                Countable countableAdd = itemData as Countable;

                if (countableResource is not null && countableAdd is not null)
                {
                    countableResource.count += countableAdd.count;
                    return;
                }
            }
        }
        itemList.Add(itemData);
    }

    public void RemoveItemData(int index)
    {
        itemList.RemoveAt(index);
    }

    public void Use(int index)
    {
        if (itemList[index] is Equipment)
        {
            EquipItem(index);

            equipAction += () =>
            {

            };

            equipAction?.Invoke();
        }
        else if (itemList[index] is Use)
        {
            useAction?.Invoke();
        }
    }

    void EquipItem(int index)
    {
        if (itemList[index] is Weapon)
        {
            AddItemData(itemList[0]);
            itemList[0] = itemList[index];
        }
        else if(itemList[index] is Accesory)
        {
            AddItemData(itemList[1]);
            itemList[1] = itemList[index];

        }
    }

    void UseItem()
    {

    }

    int[] GetStat()
    {
        int[] stat = new int[3];

        Weapon weapon = itemList[0] as Weapon;
        Accesory accesory = itemList[1] as Accesory;

        int damage = weapon.damage + accesory.damage;
        int health = accesory.health;
        int speed = accesory.speed;

        stat[0] = damage;
        stat[1] = health;
        stat[2] = speed;

        return stat;
    }
}