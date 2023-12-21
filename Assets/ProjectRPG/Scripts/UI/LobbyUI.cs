using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using InputField = TMPro.TMP_InputField;

public class LobbyUI : SceneUI
{
    public Button ExitButton { get; private set; }
    public Button StartButton { get; private set; }
    public Button StartGameButton { get; private set; }
    public GameObject Popup { get; private set; }
    public InputField NicknameInput { get; private set; }

    private void Awake()
    {
        ExitButton = GameObject.Find("ExitBtn").GetComponent<Button>();
        StartButton = GameObject.Find("StartBtn").GetComponent<Button>();
        Popup = GameObject.Find("LobbyPopup");
        StartGameButton = Popup.transform.GetChild(3).GetComponent<Button>();
        NicknameInput = Popup.transform.GetChild(2).GetComponent<InputField>();

        // if exists `savedata.json`, don't show popup and game start.
        StartButton.onClick.AddListener(() =>
        {
            if (File.Exists("savedata.json"))
            {
                //TODO: load inven data, user data.
            }
            else 
                Popup.SetActive(true);
        });

        // Exit button
        Popup.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            Popup.SetActive(false);
        });

        Popup.SetActive(false);
    }
}
