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
        OnEquip(new Item(itemData, 1));
        OnEquip(new Item(itemData1, 2));

        inventory.OnEquip += (item) =>
        {
            OnEquip(item);
        };
    }

    private void Update()
    {
        for (int i = 0; i < equipmentList.Count; i++)
        {
            if (equipmentList[i] is not null)
                equipmentImg[i].sprite = equipmentList[i].itemData.itemImg;
            else
                equipmentImg[i].sprite = null;
        }
    }

    public void OnEquip(Item item)
    {
        int index = 0;

        if (item.itemData is not Equipment) return;
        if (item.itemData is Weapon) index = 0;
        if (item.itemData is Accessory) index = 1;

        if (equipmentList[index] is not null)
        {
            UnEquip(index);
        }

        equipmentList[index] = item;
        inventory.RemoveItemData(item);
    }

    public void UnEquip(int index)
    {
        inventory.AddItemData(equipmentList[index]);
        equipmentList[index] = null;
    }
}