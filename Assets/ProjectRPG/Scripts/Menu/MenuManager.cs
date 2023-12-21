using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            Initialize();
            return _instance;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void Initialize()
    {
        if(_instance == null)
        {
            var menuManager = GameObject.Find("MenuManager");
            if (menuManager == null)
            {
                menuManager = new GameObject("ManuManager");
            }

            _instance = menuManager.GetOrAddComponent<MenuManager>();
            DontDestroyOnLoad(menuManager);
        }
    }

    public void ExitGame()
    {
        Debug.Log("EXIT_GAME");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
        var obj = Resources.Load<GameObject>($"Prefabs/SceneUI/{scene.name}SceneUI");
        var sceneUi = Instantiate(obj);
        var sceneComp = sceneUi.GetComponent<SceneUI>();

        if(sceneComp is LobbyUI)
        {
            LobbyUI lobbyUi = (LobbyUI)sceneComp;
            lobbyUi.ExitButton.onClick.AddListener(ExitGame);
            lobbyUi.StartButton.onClick.AddListener(StartGame);
        }
    }    
}
