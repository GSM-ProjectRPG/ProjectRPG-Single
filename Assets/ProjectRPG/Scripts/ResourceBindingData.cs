using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "리소스 바인딩 데이터", menuName = "Scriptable Object/리소스 바인딩 데이터", order = int.MinValue)]
public class ResourceBindingData : ScriptableObject
{
    [Header("이동속도 버프")]
    public BuffData MoveSpeedBuffData;
    [Header("공격력 버프")]
    public BuffData DamageBuffData;
    [Header("스턴 디버프")]
    public BuffData StunData;
}
