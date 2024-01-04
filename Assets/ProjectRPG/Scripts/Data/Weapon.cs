using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapon")]
public class Weapon : ItemData
{
    public enum WeaponType { SWORD, WAND };
    public WeaponType weaponType;
    public int damage;
    public GameObject ModelObject;
    Weapon() { itemType = ItemType.Weapon; }
}