﻿using System;
using MongoDB.Bson.IO;
using UnityEngine;
using Realms;
using Realms.Sync;
using TMPro;
using UnityEngine.UI;

public class AuthenticationController  
{
    // private static App realmApp = App.Create(Constants.Realm.AppId);
    // private static Realm realm;

    public static async void LoginUser(string _userInput, string _passInput)
    {
        try
        {
            var currentUser = await RealmController.SetLoggedInUser(_userInput, _passInput);
            if (currentUser != null) {
                HideAuthenticationUI();
                ShowStocUI();
                //
                
                //
            }
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
    
    public static async void RegisterUser(string _userInput, string _passInput)
    {
        try
        {
            var currentUser = await RealmController.OnPressRegister(_userInput, _passInput);
            if (currentUser != null) {
                HideAuthenticationUI();
                ShowStocUI();
            }
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }

    private static void HideAuthenticationUI()
    {   
        if (RealmController.SyncUser != null) {
            UIController.Instance.RegisterPanel.SetActive(false);
            UIController.Instance.LoginPanel.SetActive(true);
            UIController.Instance.AuthenticationPanel.SetActive(false);
        }
        //else show wrong input message/ no existing account
    }

    private static void ShowStocUI()
    {
        UIController.Instance.InventarPanel.SetActive(true);
    }
}