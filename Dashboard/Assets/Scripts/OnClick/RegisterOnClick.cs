using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterOnClick : MonoBehaviour
{
    [SerializeField] private Button registerBtn;
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TMP_InputField passInput;

    private void OnEnable()
    {
        if (registerBtn == null && !gameObject.TryGetComponent(out registerBtn))
            throw new Exception("Assign a button to " + this);
        registerBtn.onClick.AddListener(RunRegister);
    }

    private void RunRegister()
    {
        AuthenticationController.RegisterUser(userInput.text, passInput.text);
    }
    
}