using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "리소스 바인딩 데이터", menuName = "Scriptable Object/리소스 바인딩 데이터", order = int.MinValue)]
public class ResourceBindingData : ScriptableObject
{
    public BuffData MoveSpeedBuffData;
    public BuffData DamageBuffData;
    public BuffData FearData;
}
