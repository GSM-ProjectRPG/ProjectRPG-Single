using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "버프 데이터", menuName = "Scriptable Object/버프 데이터", order = int.MinValue)]
public class BuffData : ScriptableObject
{
    public string BuffName;
    public string BuffDescription;
    public Sprite BuffImage;
}