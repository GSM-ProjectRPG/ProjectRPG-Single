using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Skill CurruntSkill { get; protected set; }

    public void RegistSkill(Skill skill)
    {
        CurruntSkill = skill;
    }

    public virtual void UseSkill()
    {
        CurruntSkill?.Invoke(this);
    }
}

[CreateAssetMenu(fileName = "스킬 데이터", menuName = "Scriptable Object/스킬 데이터", order = int.MinValue)]
public class SkillData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
}

public class Skill
{
    public virtual string Name => _skillData.Name;
    public virtual string Description => _skillData.Description;
    public virtual Sprite Sprite => _skillData.Sprite;

    protected SkillData _skillData;
    protected Action<SkillSystem> _onUseSkill;

    public Skill(SkillData skill, Action<SkillSystem> onUseSkill)
    {
        _skillData = skill;
        _onUseSkill = onUseSkill;
    }

    public void Invoke(SkillSystem skillSystem)
    {
        _onUseSkill?.Invoke(skillSystem);
    }
}
