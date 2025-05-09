using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LoginScene : MonoBehaviour
{
    private const string PREFIX = "ID_";
    private const string SALT = "1004";
    
    [Header("패널")]
    [SerializeField]
    private GameObject _loginPanel;

    [SerializeField]
    private GameObject _registerPanel;

    [Header("로그인")]
    [SerializeField]
    private UI_AccountInputFields _loginInputField;

    [Header("회원가입")]
    [SerializeField]
    private UI_AccountInputFields _registerInputField;

    [Header("버튼")]
    [SerializeField]
    private Button _goToLoginButton;

    [SerializeField]
    private Button _goToRegisterButton;

    private void Start()
    {
        // PlayerPrefs.DeleteAll();
        
        GoToLogin();

        _goToLoginButton.onClick.AddListener(OnClickLoginButton);
        _goToRegisterButton.onClick.AddListener(OnClickRegisterButton);
        
        _registerInputField.ConfirmButton.onClick.AddListener(Register);
        _loginInputField.ConfirmButton.onClick.AddListener(Login);
        
        _loginInputField.IDInputField.onValueChanged.AddListener((_) => LoginCheck());
        _loginInputField.PWInputField.onValueChanged.AddListener((_) => LoginCheck());
    }

    private void OnClickLoginButton()
    {
        GoToLogin();
    }

    private void OnClickRegisterButton()
    {
        GoToRegister();
    }

    private void GoToLogin(string tempID = null)
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);

        _loginInputField.ResultText.text = string.Empty;
        _loginInputField.IDInputField.text = string.IsNullOrEmpty(tempID) ? string.Empty : tempID;
        _loginInputField.PWInputField.text = string.Empty;
    }

    private void GoToRegister()
    {
        _loginPanel.SetActive(false);
        _registerPanel.SetActive(true);
        
        _registerInputField.ResultText.text = string.Empty;
    }

    // 회원가입
    private void Register()
    {
        // 아이디 검사
        string id = _registerInputField.IDInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            _registerInputField.ResultText.text = "아이디를 입력해 주세요.";
            return;
        }

        // 비밀번호 검사
        string pw = _registerInputField.PWInputField.text;
        if (string.IsNullOrEmpty(pw))
        {
            _registerInputField.ResultText.text = "비밀번호를 입력해 주세요.";
            return;
        }

        // 2차 비밀번호 검사
        string pwConfirm = _registerInputField.PWConfirmInputField.text;
        if (string.IsNullOrEmpty(pwConfirm))
        {
            _registerInputField.ResultText.text = "비밀번호 확인을 입력해 주세요.";
            return;
        }

        if (!string.Equals(pw, pwConfirm))
        {
            _registerInputField.ResultText.text = "비밀번호 확인 결과가 다릅니다.";
            return;
        }

        PlayerPrefs.SetString(PREFIX + id, Encryption(pw + SALT));
        
        GoToLogin(id);
    }

    private void Login()
    {
        string id = _loginInputField.IDInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            _loginInputField.ResultText.text = "아이디를 입력해 주세요.";
            return;
        }
        
        string pw = _loginInputField.PWInputField.text;
        if (string.IsNullOrEmpty(pw))
        {
            _loginInputField.ResultText.text = "비밀번호를 입력해 주세요.";
            return;
        }
        
        if (!PlayerPrefs.HasKey(PREFIX + id))
        {
            _loginInputField.ResultText.text = "아이디와 비밀번호를 확인해 주세요.";
            return;
        }

        if (!string.Equals(PlayerPrefs.GetString(PREFIX + id), Encryption(pw + SALT)))
        {
            _loginInputField.ResultText.text = "아이디와 비밀번호를 확인해 주세요.";
            return;
        }

        // _loginInputField.ResultText.text = "로그인에 성공했습니다.";
        SceneManager.LoadScene(1);
    }

    private void LoginCheck()
    {
        string id = _loginInputField.IDInputField.text;
        string pw = _loginInputField.PWInputField.text;
        
        _loginInputField.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(pw);
    }

    private string Encryption(string pw)
    {
        SHA256 sha256 = SHA256.Create();
        
        // 운영체제 혹은 언어별로 string을 표현하는 방식이 다 다르므로 UTF-8버전의 바이트 배열로 바꿔야한다.
        byte[] bytes = Encoding.UTF8.GetBytes(pw);
        byte[] hash = sha256.ComputeHash(bytes);
        
        string resultText = string.Empty;
        foreach (var b in hash)
        {
            resultText += b.ToString("X2");
        }
        
        return resultText;
    }
}