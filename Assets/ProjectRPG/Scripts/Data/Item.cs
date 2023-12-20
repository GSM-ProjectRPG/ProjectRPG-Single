using UnityEngine;
using System;

public class Item
{
    public ItemData itemData;
    public int count;

    public Item(ItemData itemData, int count)
    {
        this.itemData = itemData;
        this.count = count;
    }
}
public class ItemData : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public int itemID;
    public int price;
}

public class Equipment : ItemData
{
    public int damage;
}