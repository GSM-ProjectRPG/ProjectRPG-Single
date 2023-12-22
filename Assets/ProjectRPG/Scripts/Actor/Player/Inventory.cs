using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> ItemList = new List<Item>();
    public List<Item> EquipmentList = new List<Item>() { null, null };

    public Action<int> OnEquipAction;
    public Action<int> OnUseAction;

    public Action<Item> OnAddItemAction;
    public Action<Item> OnReduceItemAction;

    public Action OnEquipItemAction;
    public Action UnEquipItemAction;

    private void Awake()
    {
        OnEquipAction += OnSwitchItem;
    }

    public void AddItem(Item compareItem)
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
        if (add) ItemList.Add(compareItem);
        OnAddItemAction?.Invoke(compareItem);
    }

    public void ReduceItem(Item compareItem)
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
        OnReduceItemAction?.Invoke(compareItem);
    }

    public void OnSwitchItem(int itemIndex)
    {
        int index = 0;

        if (ItemList[itemIndex].ItemType is not ItemType.Weapon && ItemList[itemIndex].ItemType is not ItemType.Accessory) return;
        if (ItemList[itemIndex].ItemType is ItemType.Weapon) index = 0;
        if (ItemList[itemIndex].ItemType is ItemType.Accessory) index = 1;

        UnEquipItem(index);
        OnEquipItem(index,itemIndex);
    }

    public void OnEquipItem(int equipmentItemindex, int inventoryItemIndex)
    {
        EquipmentList[equipmentItemindex] = new Item(ItemList[inventoryItemIndex].GetItemData(), 1);
        ReduceItem(new Item(ItemList[inventoryItemIndex].GetItemData(), 1));
        OnEquipItemAction?.Invoke();
    }

    public void UnEquipItem(int equipmentItemindex)
    {
        if (EquipmentList[equipmentItemindex] is null) return;
        AddItem(EquipmentList[equipmentItemindex]);
        EquipmentList[equipmentItemindex] = null;
        UnEquipItemAction?.Invoke();
    }

    public void UseItem(int index)
    {
        if (ItemList.Count <= index) return;
        if (ItemList[index] is null) return;

        if (ItemList[index].ItemType is ItemType.Weapon || ItemList[index].ItemType is ItemType.Accessory)
        {
            OnEquipAction?.Invoke(index);
        }
        else if (ItemList[index].ItemType is ItemType.Use)
        {
            OnUseAction?.Invoke(index);
        }
    }
}