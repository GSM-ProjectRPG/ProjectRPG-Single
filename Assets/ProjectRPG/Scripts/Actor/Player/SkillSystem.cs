using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public static SkillSystem Instance { get; private set; }

    public Action<Skill> OnRegistedSkill;
    public Action<Skill> OnChangeSkill;
    public Action<Skill> OnUseSkill;

    public List<Skill> HaveSkills { get; protected set; } = new();
    public Skill CurruntSkill { get; protected set; }

    [SerializeField] private SkillData _basicSkillData;
    [SerializeField] private SkillData _healSkillData;
    [SerializeField] private SkillData _moveSpeedBuffSkillData;
    [SerializeField] private SkillData _damageBuffSkillData;
    [SerializeField] private SkillData _slashSkillData;
    [SerializeField] private SkillData _fireBallSkillData;
    [SerializeField] private SkillData _fearSkillData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError(typeof(SkillSystem).Name + "의 인스턴스가 2개 이상 존재합니다.");
            Destroy(this);
        }
    }

    private void Start()
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
    public SkillData SkillData { get; protected set; }
    protected Action _onUseSkill;

    public Skill(SkillData skillData, Action onUseSkill)
    {
        SkillData = skillData;
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