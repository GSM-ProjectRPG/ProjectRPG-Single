using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if(_instance != null)
                {
                    return _instance;
                }
                GameObject g = new GameObject("GameManager");
                DontDestroyOnLoad(g);
                return g.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public string NickName { get; set; }

    public int Coin { get; set; }
}
