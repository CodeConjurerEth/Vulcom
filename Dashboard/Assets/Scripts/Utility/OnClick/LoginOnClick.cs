using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginOnClick : MonoBehaviour
{
    [SerializeField] private Button loginBtn;
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TMP_InputField passInput;


    private void OnEnable()
    {
        if (loginBtn == null && !gameObject.TryGetComponent(out loginBtn))
            throw new Exception("Assign a button to " + this);
        else {
            loginBtn.onClick.AddListener(RunLogin);
        }
    }

    private void RunLogin()
    {
        AuthenticationController.LoginUser(userInput.text, passInput.text);
    }
    
}
