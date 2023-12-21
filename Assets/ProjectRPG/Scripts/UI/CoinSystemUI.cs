using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystemUI : MonoBehaviour
{
    public CoinSystem CoinSystem;
    public Text CoinText;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += GetCoinSystem;
    }

    private void GetCoinSystem()
    {
        CoinSystem = ActorManager.Instance.Player.GetComponent<CoinSystem>();
        CoinSystem.SetCoinAction += (_) =>
        {
            CoinUIUpdate();
        };
    }

    private void CoinUIUpdate()
    {
        CoinText.text = CoinSystem.Coin.ToString();
    }
}
