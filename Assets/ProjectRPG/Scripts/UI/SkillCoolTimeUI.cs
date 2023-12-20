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
            Skill curruntSkill = ActorManager.Instance.Player.GetComponent<SkillSystem>().CurruntSkill;
            _skill = curruntSkill as CoolTimeSkill;
            if( _skill == null )
            {
                Image.sprite = curruntSkill.Sprite;
                Cover.sprite = curruntSkill.Sprite;
                Cover.fillAmount = 0;
            }
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
