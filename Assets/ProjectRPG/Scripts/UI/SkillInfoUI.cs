using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUI : MonoBehaviour
{
    public Image SkillImage;
    public Text SkillNameText;
    public Text SkillDescriptionText;
    public Text SkillCoolTimeText;
    public Button SkillSelectButton;

    public void SetSkillInfo(Skill skill,Action onSelectedSkill)
    {
        SkillImage.sprite = skill.SkillData.Sprite;
        SkillNameText.text = skill.SkillData.Name;
        SkillDescriptionText.text = skill.SkillData.Description;

        CoolTimeSkill coolTimeSkill = skill as CoolTimeSkill;
        bool isCool = coolTimeSkill != null;
        SkillCoolTimeText.gameObject.SetActive(isCool);
        if(isCool)
        {
            SkillCoolTimeText.text = coolTimeSkill.CoolTime + "초";
        }

        SkillSelectButton.onClick.RemoveAllListeners();
        SkillSelectButton.onClick.AddListener(new UnityEngine.Events.UnityAction(onSelectedSkill));
    }
}
