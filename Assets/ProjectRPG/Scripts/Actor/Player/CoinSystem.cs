using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public Action<int> SetCoinAction;

    private int _coin;
    public int Coin { 
        get
        {
            return _coin;
        }
        protected set
        {
            int origin = _coin;
            _coin = value;
            SetCoinAction?.Invoke(origin - value);
        }
    }
}