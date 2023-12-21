using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Action<Skill> OnRegistedSkill;
    public Action<Skill> OnChangeSkill;
    public Action<Skill> OnUseSkill;

    public List<Skill> HaveSkills { get; protected set; } = new();
    public Skill CurruntSkill { get; protected set; }

    [SerializeField] private SkillData _basicSkillData;

    private void Awake()
    {
        SelectSkill(new Skill(_basicSkillData, null));
    }

    public void RegistSkill(Skill skill)
    {
        HaveSkills.Add(skill);
        OnRegistedSkill?.Invoke(skill);
    }

    public void SelectSkill(Skill skill)
    {
        CurruntSkill = skill;
        OnChangeSkill?.Invoke(skill);
    }

    public void UseSkill()
    {
        if (CurruntSkill != null)
        {
            CurruntSkill.Invoke();
            OnUseSkill?.Invoke(CurruntSkill);
        }
    }
}

public class Skill
{
    public virtual string Name => _skillData.Name;
    public virtual string Description => _skillData.Description;
    public virtual Sprite Sprite => _skillData.Sprite;

    protected SkillData _skillData;
    protected Action _onUseSkill;

    public Skill(SkillData skillData, Action onUseSkill)
    {
        _skillData = skillData;
        _onUseSkill = onUseSkill;
    }

    public void Invoke()
    {
        _onUseSkill?.Invoke();
    }
}

public class CoolTimeSkill : Skill
{
    public float CoolTime { get; protected set; }
    public float RemainingTime => Mathf.Max(0, lastUsedTime + CoolTime - Time.time);

    private float lastUsedTime;

    public CoolTimeSkill(SkillData skillData, Action onUseSkill, float coolTime) : base(skillData, onUseSkill)
    {
        CoolTime = coolTime;
        _onUseSkill = () =>
         {
             if (RemainingTime == 0)
             {
                 lastUsedTime = Time.time;
                 onUseSkill?.Invoke();
             }
         };
    }
}