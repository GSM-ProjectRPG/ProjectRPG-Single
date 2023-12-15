using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public Image[] buttonImgs;

    void Update()
    {
        for(int i = 0; i < 18; i++)
        {
            buttonImgs[i].sprite = inventory.itemList[i].itemData.itemImg;
        }
    }
}
