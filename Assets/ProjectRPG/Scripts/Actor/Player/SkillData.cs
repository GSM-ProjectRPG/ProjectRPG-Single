using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "스킬 데이터", menuName = "Scriptable Object/스킬 데이터", order = int.MinValue)]
public class SkillData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public SkillWeaponType SkillWeaponType;
}

public enum SkillWeaponType
{
    Global = 0,
    Melee = 1,
    Range = 2
}