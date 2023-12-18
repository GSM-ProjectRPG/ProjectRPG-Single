using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { HEAL };

[CreateAssetMenu(fileName = "Use", menuName = "ScriptableObject/Use")]
public class Use : ItemData
{
    public EffectType effectType;
    public int effectValue;
    public int effectRemainTime;
}