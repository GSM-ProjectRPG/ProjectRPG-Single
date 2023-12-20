using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public Action<int> SetCoinAction;

    public int currentCoin;
    public int Coin { 
        get
        {
            return currentCoin;
        }
        protected set
        {
            int origin = currentCoin;
            currentCoin = value;
            SetCoinAction?.Invoke(origin - value);
        }
    }
}