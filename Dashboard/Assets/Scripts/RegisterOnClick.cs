using System;
using TMPro;
using UnityEngine;

public class RegisterOnClick : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField userInput;
    [SerializeField]
    private TMP_InputField passInput;

    public void RunRegister()
    {
        AuthenticationManager.RegisterUser(userInput.text, passInput.text);
    }

    // public void RunLogout()
    // {
    //     RealmController.LogOutBackend();
    // }
}