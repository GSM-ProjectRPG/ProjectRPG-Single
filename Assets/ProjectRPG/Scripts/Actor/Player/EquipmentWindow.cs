using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentWindow : MonoBehaviour
{
    public InventoryUI InventoryUI;
    public Inventory Inventory;

    public List<Item> equipmentList = new List<Item>() { null, null };

    public Image[] equipmentImg;

    private void Start()
    {
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
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

    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
        InventoryUI.Inventory.OnEquip += (item) =>
        {
            OnEquip(item);
        };
    }

    public void OnEquip(Item item)
    {
        int index = 0;

        if (item.ItemType is not ItemType.Weapon && item.ItemType is not ItemType.Accessory) return;
        if (item.ItemType is ItemType.Weapon) index = 0;
        if (item.ItemType is ItemType.Accessory) index = 1;

        UnEquip(index);
        equipmentList[index] = new Item(item.GetItemData(), 1);
        InventoryUI.Inventory.ReduceItemData(new Item(item.GetItemData(), 1));
    }

    public void UnEquip(int index)
    {
        if (equipmentList[index] is null) return;
        InventoryUI.Inventory.AddItemData(equipmentList[index]);
        equipmentList[index] = null;
    }
}