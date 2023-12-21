using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    public Text Text;
    public Slider Slider;

    private PlayerStatSystem _playerStatSystem;

    // Start is called before the first frame update
    private void Start()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            _playerStatSystem = ActorManager.Instance.Player?.GetComponent<PlayerStatSystem>();
            _playerStatSystem.OnAddedExp += (_) => UpdateUI();
            _playerStatSystem.OnLevelUp += () => UpdateUI();
            UpdateUI();
        };

    }

    private void UpdateUI()
    {
        if (_playerStatSystem == null) return;
        Slider.value = _playerStatSystem.Exp / _playerStatSystem.NeededExp;
        int  percentText = (int)(_playerStatSystem.Exp / _playerStatSystem.NeededExp * 100f);
        string text = percentText + "% (" + _playerStatSystem.Exp + "/" + _playerStatSystem.NeededExp + ")";
        Text.text = text;
    }
}
