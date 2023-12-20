using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int Coin { get; protected set; }

    public Text CoinTxt;

    public void AddCoin(int value)
    {
        Coin += value;
        CoinUIUpdate();
    }

    public void ReduceCoin(int value)
    {
        Coin -= value;
        CoinUIUpdate();
    }

    private void CoinUIUpdate()
    {
        CoinTxt.text = Coin.ToString();
    }
}
