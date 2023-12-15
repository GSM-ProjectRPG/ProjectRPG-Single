using UnityEngine;
using System;

public class ItemData : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    public string itemDescription;
    public int itemID;
    public int maxItemCount;
    public int price;
}

public abstract class Item
{
    public ItemData itemData;
}


[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon")]
public class Equipment : Item
{
    public int damage;
}

public class Weapon : Equipment
{
    
}

[CreateAssetMenu(fileName = "Accessory", menuName = "ScriptableObject/Accessory")]
public class Accesory : Equipment
{
    public int health;
    public int speed;
}

public class Countable : Item
{
    public int count;
}

[CreateAssetMenu(fileName = "Use", menuName = "ScriptableObject/Use")]
public class Use : Countable
{
    public Action action;
}

[CreateAssetMenu(fileName = "Etc", menuName = "ScriptableObject/Etc")]
public class Etc : Countable
{

}