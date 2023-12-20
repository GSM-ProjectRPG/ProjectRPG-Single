using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystemUI : MonoBehaviour
{
    public CoinSystem CoinSystem;
    public Text CoinTxt;
    private void CoinUIUpdate()
    {
        CoinTxt.text = CoinSystem.Coin.ToString();
    }
}
