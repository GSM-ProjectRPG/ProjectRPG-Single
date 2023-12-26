using UnityEngine;
using System;

public enum ItemType
{
    Etc,
    Weapon,
    Accessory,
    Use
}

public class Item
{
    public ItemType ItemType { get; set; }
    public Sprite ItemImg { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemID { get; set; }
    public int Price { get; set; }
    public int count;

    public Item(ItemData itemData, int count)
    {
        ItemType = itemData.itemType;
        ItemImg = itemData.itemImg;
        ItemName = itemData.itemName;
        ItemDescription = itemData.itemDescription;
        ItemID = itemData.itemID;
        Price = itemData.price;
        this.count = count;
    }

    public ItemData GetItemData()
    {
        return new ItemData()
        {
            itemType = ItemType,
            itemImg = ItemImg,
            itemName = ItemName,
            itemDescription = ItemDescription,
            itemID = ItemID,
            price = Price
        };
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemImg;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public int itemID;
    public int price;
}