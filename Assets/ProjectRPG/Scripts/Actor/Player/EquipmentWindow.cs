using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentWindow : MonoBehaviour
{
    public Inventory inventory;

    public List<Item> equipmentList = new List<Item>() { null, null };

    public Image[] equipmentImg;

    public ItemData itemData;
    public ItemData itemData1;

    private void Awake()
    {
        inventory.OnEquip += (item) =>
        {
            OnEquip(item);
        };
    }

    private void Start()
    {
        OnEquip(new Item(itemData, 1));
        OnEquip(new Item(itemData1, 1));
    }

    private void Update()
    {
        for (int i = 0; i < equipmentList.Count; i++)
        {
            if (equipmentList[i] is not null)
                equipmentImg[i].sprite = equipmentList[i].ItemImg;
            else
                equipmentImg[i].sprite = null;
        }
    }

    public void OnEquip(Item item)
    {
        int index = 0;

        if (item.ItemType is not ItemType.Weapon && item.ItemType is not ItemType.Accessory) return;
        if (item.ItemType is ItemType.Weapon) index = 0;
        if (item.ItemType is ItemType.Accessory) index = 1;

        UnEquip(index);
        equipmentList[index] = new Item(item.GetItemData(), 1);
        inventory.ReduceItemData(new Item(item.GetItemData(), 1));
    }

    public void UnEquip(int index)
    {
        if (equipmentList[index] is null) return;
        inventory.AddItemData(equipmentList[index]);
        equipmentList[index] = null;
    }
}