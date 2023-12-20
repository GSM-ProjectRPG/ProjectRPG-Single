using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public Action<int> SetCoinAction;

    private int currentCoin;
    public int Coin { 
        get
        {
            return currentCoin;
        }
        set
        {
            int origin = currentCoin;
            currentCoin = value;
            SetCoinAction?.Invoke(origin - value);
        }
    }
}