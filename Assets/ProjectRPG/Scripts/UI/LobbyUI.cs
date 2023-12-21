using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : SceneUI
{
    private void Awake()
    {
        ExitButton = GameObject.Find("ExitBtn").GetComponent<Button>();
    }
}
