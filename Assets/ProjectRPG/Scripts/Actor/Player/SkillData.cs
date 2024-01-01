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
    public float CoolTime;

    public Skill GetSkillInstance(Action useAction)
    {
        if (CoolTime > 0)
        {
            return new CoolTimeSkill(this, useAction, CoolTime);
        }
        else
        {
            return new Skill(this, useAction);
        }
    }
}