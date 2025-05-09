using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_AccountInputFields
{
    [SerializeField]
    private TextMeshProUGUI _resultField;

    public TextMeshProUGUI ResultText => _resultField;

    [SerializeField]
    private TMP_InputField _idInputField;

    public TMP_InputField IDInputField => _idInputField;

    [SerializeField]
    private TMP_InputField _pwInputField;

    public TMP_InputField PWInputField => _pwInputField;

    [SerializeField]
    private TMP_InputField _pwConfirmInputField;

    public TMP_InputField PWConfirmInputField => _pwConfirmInputField;
    
    [SerializeField]
    private Button _confirmButton;
    public Button ConfirmButton => _confirmButton;
}