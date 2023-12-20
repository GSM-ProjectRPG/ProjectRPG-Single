using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int Coin;

    public Text CoinTxt;

    private void Update()
    {
        CoinTxt.text = Coin.ToString();
    }
}
