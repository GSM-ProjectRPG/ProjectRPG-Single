using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private TMP_InputField _userNameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    public void RequestLogin()
    {
        string userName = _userNameInputField.text;
        string password = _passwordInputField.text;

        // 로그인 요청 코드 작성
    }

    public void InitLoginInputField()
    {
        _userNameInputField.text = "";
        _passwordInputField.text = "";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
