using System;
using MongoDB.Bson.IO;
using UnityEngine;
using Realms;
using Realms.Sync;
using TMPro;
using UnityEngine.UI;

public static class AuthenticationManager  
{
    private static App realmApp = App.Create(Constants.Realm.AppId);
    private static Realm realm;

    public static async void LoginUser(string _userInput, string _passInput)
    {
        {
            try
            {
                var currentUser = await RealmController.SetLoggedInUser(_userInput, _passInput);
                // if (currentPlayer != null)
                // {
                    // HideAuthenticationUI();
                // }
            }
            catch (Exception ex)
            {
                Debug.Log("an exception was thrown:" + ex.Message);
            }
        }   
    }
    
    public static async void RegisterUser(string _userInput, string _passInput)
    {
        try
        {
            var currentUser = await RealmController.OnPressRegister(_userInput, _passInput);
            // if (currentPlayer != null)
            // {
            //     HideAuthenticationUI();
            // }
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
}