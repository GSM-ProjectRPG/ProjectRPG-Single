using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystemUI : MonoBehaviour
{
    public CoinSystem CoinSystem;
    public Text CoinText;

    private void Update()
    {
        CoinUIUpdate();
    }

    private void CoinUIUpdate()
    {
        CoinText.text = CoinSystem.Coin.ToString();
    }
}
