using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public Action<int> SetCoinAction;

    public int Coin { 
        get
        {
            return GameManager.Instance.Coin;
        }
        set
        {
            int origin = GameManager.Instance.Coin;
            GameManager.Instance.Coin = value;
            SetCoinAction?.Invoke(origin - value);
        }
    }

    private void Start()
    {
        if(Coin != 0)
        {
            SetCoinAction?.Invoke(Coin);
        }
    }
}