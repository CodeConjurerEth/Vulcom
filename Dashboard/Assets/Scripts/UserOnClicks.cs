using System;
using TMPro;
using UnityEngine;

public class LoginRegisterOnClicks : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField userInput;
    [SerializeField]
    private TMP_InputField passInput;
    
    public void RunLogin()
    {
        AuthenticationManager.LoginUser(userInput.text, passInput.text);
    }

    public void RunRegister()
    {
        AuthenticationManager.RegisterUser(userInput.text, passInput.text);
    }

    public void RunLogout()
    {
        RealmController.LogOutBackend();
    }
}
