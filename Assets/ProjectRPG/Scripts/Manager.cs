using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    DBConnector _connector = new DBConnector();

    public static DBConnector DB { get { return Instance._connector; } }


    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            var manager = GameObject.Find("@Manager");
            if (manager == null)
            {
                manager = new GameObject("@Manager");
                manager.AddComponent<Manager>();
            }

            DontDestroyOnLoad(manager);
            _instance = manager.GetComponent<Manager>();

            DB.Connect("server=172.28.148.84;port=3305;database=projectrpg-db;uid=rpgdb");
        }
    }
}
