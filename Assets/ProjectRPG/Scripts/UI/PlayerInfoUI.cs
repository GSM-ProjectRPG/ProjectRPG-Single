using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Text NameText;
    public Slider HpSlider;
    public Text HpText;

    private Health _targetHealth;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            _targetHealth = ActorManager.Instance.Player.GetComponent<Health>();
            _targetHealth.OnHealthChanged += () =>
            {
                RefreshUI();
            };
        };
    }

    private void RefreshUI()
    {
        NameText.text = GameManager.Instance.NickName;
        HpSlider.value = _targetHealth.CurruntHealth / _targetHealth.MaxHealth;
        HpText.text = _targetHealth.CurruntHealth + "/" + _targetHealth.MaxHealth;
    }
}
