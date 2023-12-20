using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Etc", menuName = "Scriptable Object/Etc")]
public class Etc : ItemData
{
    Etc() { itemType = ItemType.Etc; }
}
