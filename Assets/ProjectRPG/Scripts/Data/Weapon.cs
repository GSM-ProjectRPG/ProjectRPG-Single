using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapon")]
public class Weapon : Equipment
{
    public enum WeaponType { SWORD, WAND };
    public WeaponType weaponType;
}