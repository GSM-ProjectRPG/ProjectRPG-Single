using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    public Image Image;
    public Image Cover;

    private CoolTimeSkill _skill;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            SkillSystem skillSystem = ActorManager.Instance.Player.GetComponent<SkillSystem>();
            skillSystem.OnChangeSkill += (skill) =>
            {
                _skill = skill as CoolTimeSkill;

                Image.sprite = skill.SkillData.Sprite;
                Cover.sprite = skill.SkillData.Sprite;
                if (_skill == null)
                {
                    Cover.fillAmount = 0;
                }
            };
        };
    }

    private void Update()
    {
        if (_skill != null)
        {
            Cover.fillAmount = _skill.RemainingTime / _skill.CoolTime;
        }
    }
}
