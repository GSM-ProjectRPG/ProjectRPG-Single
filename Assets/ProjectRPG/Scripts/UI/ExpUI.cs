using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    public Text Text;
    public Slider Slider;

    private PlayerStatSystem playerStatSystem;

    // Start is called before the first frame update
    private void Start()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            playerStatSystem = ActorManager.Instance.Player?.GetComponent<PlayerStatSystem>();
            playerStatSystem.OnAddedExp += (_) => UpdateUI();
            playerStatSystem.OnLevelUp += () => UpdateUI();
            UpdateUI();
        };

    }

    private void UpdateUI()
    {
        if (playerStatSystem == null) return;
        Slider.value = playerStatSystem.Exp / playerStatSystem.NeededExp;
        int  percentText = (int)(playerStatSystem.Exp / playerStatSystem.NeededExp * 100f);
        string text = percentText + "% (" + playerStatSystem.Exp + "/" + playerStatSystem.NeededExp + ")";
        Text.text = text;
    }
}
