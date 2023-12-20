using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;

    public Action<int> OnClickAction;

    public Image[] InventoryImg;
    public Text[] InventoryTxt;

    private void Update()
    {
        for (int i = 0; i < 16; i++)
        {
            if (i < Inventory.ItemList.Count)
            {
                InventoryImg[i].sprite = Inventory.ItemList[i].ItemImg;
                InventoryTxt[i].text = Inventory.ItemList[i].count.ToString();
            }
            else
            {
                InventoryImg[i].sprite = null;
                InventoryTxt[i].text = null;
            }
        }
    }

    public void OnClick(int index)
    {
        OnClickAction?.Invoke(index);
    }
}
