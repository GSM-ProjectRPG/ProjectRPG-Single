using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindow : MonoBehaviour
{
    public Inventory inventory;

    public List<ItemData> equipmentWindowList = new List<ItemData>();

    public void SwitchEquiment(Item item)
    {
        int index;

        if (item.itemData is not Equipment) return;

        if (item.itemData is Weapon) index = 0;

        if (item.itemData is Accesory) index = 1;


    }
}