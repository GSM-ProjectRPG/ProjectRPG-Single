using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : SceneUI
{
    public Button ExitButton { get; private set; }
    public Button StartButton { get; private set; }

    private void Awake()
    {
        ExitButton = GameObject.Find("ExitBtn").GetComponent<Button>();
        StartButton = GameObject.Find("StartBtn").GetComponent<Button>();
    }
}
