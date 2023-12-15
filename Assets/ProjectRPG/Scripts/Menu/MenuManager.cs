using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private TMP_InputField _idInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    public void RequestLogin()
    {
        string id = _idInputField.text;
        string password = _passwordInputField.text;

        // 로그인 요청
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
