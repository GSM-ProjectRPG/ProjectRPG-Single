using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    public bool IsOpeningUI => _skillSelectUILayer.activeSelf;

    [Header("스킬메뉴 전환 버튼")]
    [SerializeField] private GameObject _globalSkillTabButton;
    [SerializeField] private GameObject _meleeSkillTabButton;
    [SerializeField] private GameObject _rangeSkillTabButton;
    [SerializeField] private Button _UICloseButton;

    [SerializeField] private GameObject _skillSelectUILayer;
    [SerializeField] private RectTransform _content;
    [SerializeField] private Image _skillMenuBackGround;
    [SerializeField] private GameObject _skillInfoPrefab;

    private Image[] _skillTabImage;
    private SkillWeaponType _curruntSkillTabType;

    private void Awake()
    {
        SkillSystem.Instance.OnRegistedSkill += (registedSkill) =>
        {
            if (registedSkill.SkillData.SkillWeaponType == _curruntSkillTabType)
            {
                InstantiateSkillInfo(registedSkill);
            }
        };

        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            ActorManager.Instance.Player.GetComponent<PlayerController>().SkillSelectUI = this;
        };
    }

    // Start is called before the first frame update
    private void Start()
    {
        _skillTabImage = new Image[3];
        _skillTabImage[0] = _globalSkillTabButton.GetComponent<Image>();
        _skillTabImage[1] = _meleeSkillTabButton.GetComponent<Image>();
        _skillTabImage[2] = _rangeSkillTabButton.GetComponent<Image>();
        _skillTabImage[0].GetComponent<Button>().onClick.AddListener(() => ChangeTab(SkillWeaponType.Global));
        _skillTabImage[1].GetComponent<Button>().onClick.AddListener(() => ChangeTab(SkillWeaponType.Melee));
        _skillTabImage[2].GetComponent<Button>().onClick.AddListener(() => ChangeTab(SkillWeaponType.Range));
        _UICloseButton.onClick.AddListener(() => { HideSkillMenu(); });

        _skillMenuBackGround.color = _skillTabImage[0].color;
    }

    public void ShowSkillMenu()
    {
        _skillSelectUILayer.SetActive(true);
        RefreshSkillInfo();
    }

    public void HideSkillMenu()
    {
        _skillSelectUILayer.SetActive(false);
    }

    public void ChangeTab(SkillWeaponType tabType)
    {
        Debug.Log(tabType);
        _curruntSkillTabType = tabType;
        _skillMenuBackGround.color = _skillTabImage[(int)tabType].color;
        RefreshSkillInfo();
    }

    private void RefreshSkillInfo()
    {
        ClearSkillInfo();
        foreach (var skill in SkillSystem.Instance.HaveSkills)
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
            SkillSystem.Instance?.SelectSkill(skill);
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
