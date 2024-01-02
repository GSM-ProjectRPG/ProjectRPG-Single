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
    public ItemType ItemType => _itemData.itemType;
    public Sprite ItemImg => _itemData.itemImg;
    public string ItemName => _itemData.itemName;
    public string ItemDescription => _itemData.itemDescription;
    public int ItemID => _itemData.itemID;
    public int Price => _itemData.price;
    public int count;

    ItemData _itemData;

    public Item(ItemData itemData, int count)
    {
        _itemData = itemData;
        this.count = count;
    }

    public ItemData GetItemData()
    {
        return _itemData;
    }

    public Stat GetStat()
    {
        switch (ItemType)
        {
            case ItemType.Weapon:
                Weapon weapon = _itemData as Weapon;
                if (weapon != null)
                {
                    Stat stat = new Stat();
                    stat.Attack = weapon.damage;
                    return stat;
                }
                break;
            case ItemType.Accessory:
                Accessory accessory = _itemData as Accessory;
                if (accessory != null)
                {
                    Stat stat = new Stat();
                    stat.Attack = accessory.damage;
                    stat.Health = accessory.health;
                    stat.MoveSpeed = accessory.speed;
                    return stat;
                }
                break;
        }
        return new Stat();
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