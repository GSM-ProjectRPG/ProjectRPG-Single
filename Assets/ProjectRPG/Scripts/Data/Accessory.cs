using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Scriptable Object/Accessory")]
public class Accessory : ItemData
{
    public int damage;
    public int health;
    public int speed;
    public GameObject ModelObject;
    Accessory() { itemType = ItemType.Accessory; }
}