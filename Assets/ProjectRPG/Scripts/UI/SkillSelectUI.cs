using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [Header("스킬메뉴 전환 버튼")]
    [SerializeField] private GameObject _globalSkillTabButton;
    [SerializeField] private GameObject _meleeSkillTabButton;
    [SerializeField] private GameObject _rangeSkillTabButton;

    [SerializeField] private GameObject skillSelectUILayer;
    [SerializeField] private RectTransform _content;
    [SerializeField] private Image _skillMenuBackGround;
    [SerializeField] private GameObject _skillInfoPrefab;

    private Image[] skillTabImage;
    private SkillSystem _skillSystem;
    private SkillWeaponType _curruntSkillTabType;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            _skillSystem = ActorManager.Instance.Player.GetComponent<SkillSystem>();
            _skillSystem.OnRegistedSkill += (registedSkill) => {
                if (registedSkill.SkillData.SkillWeaponType == _curruntSkillTabType)
                {
                    InstantiateSkillInfo(registedSkill);
                }
            };
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        skillTabImage = new Image[3];
        skillTabImage[0] = _globalSkillTabButton.GetComponent<Image>();
        skillTabImage[1] = _meleeSkillTabButton.GetComponent<Image>();
        skillTabImage[2] = _rangeSkillTabButton.GetComponent<Image>();
        skillTabImage[0].GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => ChangeTab(SkillWeaponType.Global)));
        skillTabImage[1].GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => ChangeTab(SkillWeaponType.Melee)));
        skillTabImage[2].GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => ChangeTab(SkillWeaponType.Range)));


        _skillMenuBackGround.color = skillTabImage[0].color;
    }

    public void ShowSkillMenu()
    {
        skillSelectUILayer.SetActive(true);
        RefreshSkillInfo();
    }

    public void HideSkillMenu()
    {
        skillSelectUILayer.SetActive(false);
    }

    public void ChangeTab(SkillWeaponType tabType)
    {
        Debug.Log(tabType);
        _curruntSkillTabType = tabType;
        _skillMenuBackGround.color = skillTabImage[(int)tabType].color;
        RefreshSkillInfo();
    }

    private void RefreshSkillInfo()
    {
        ClearSkillInfo();
        foreach (var skill in _skillSystem.HaveSkills)
        {
            if (_curruntSkillTabType == skill.SkillData.SkillWeaponType)
            {
                InstantiateSkillInfo(skill);
            }
        }
    }

    private void InstantiateSkillInfo(Skill skill)
    {
        GameObject g = Instantiate(_skillInfoPrefab, _content);
        g.GetComponent<SkillInfoUI>()?.SetSkillInfo(skill, () =>
        {
            _skillSystem?.SelectSkill(skill);
        });
    }

    private void ClearSkillInfo()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
