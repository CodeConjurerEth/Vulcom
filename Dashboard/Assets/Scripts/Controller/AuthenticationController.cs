using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
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
                //--------------------------------
                // var alama = new Metal("Alama", 1d, 1d);
                // var bara = new Bara("Bara", alama, 10d, 0.5d);
                //
                // RealmController.AddToDB(bara);
                // RealmController.AddToDB(alama);

                var metalList = RealmController.GetMetalListFromDB();
                Debug.Log("ayy");
                Debug.Log(metalList.Result[0].Bare.Count());

                //--------------------------------
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