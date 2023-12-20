using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private TMP_InputField _loginUserName;
    [SerializeField] private TMP_InputField _loginPassword;

    [Space()]
    [Header("Create Account")]
    [SerializeField] private TMP_InputField _createAccountUserName;
    [SerializeField] private TMP_InputField _createAccountPassword;
    [SerializeField] private TMP_InputField _createAccountConfirmPassword;

    public void RequestLogin()
    {
        string userName = _loginUserName.text;
        string password = _loginPassword.text;

        Debug.Log("Request Login");
        // 로그인 요청 코드 작성
    }

    public void RequestCreateAccount()
    {
        string userName = _createAccountUserName.text;
        string password = _createAccountPassword.text;
        string confirmPassword = _createAccountConfirmPassword.text;

        Debug.Log("Request CA");
        // 계정생성 요청 코드 작성
    }

    public void InitLoginAndCreateAccountInputField()
    {
        _loginUserName.text = "";
        _loginPassword.text = "";

        _createAccountUserName.text = "";
        _createAccountPassword.text = "";
        _createAccountConfirmPassword.text = "";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
