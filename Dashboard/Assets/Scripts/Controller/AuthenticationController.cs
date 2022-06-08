using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using UnityEngine;
using Realms;
using Realms.Sync;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationController  
{

    public static async void LoginUser(string userInput, string passInput)
    {
        try {
            // legacy -> UIController
            // var uiController = UIController.Instance;
            
            var currentUser = await RealmController.SetLoggedInUser(userInput, passInput);
            if (currentUser != null) {

                SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);

                // uiController.HideAuthenticationUIOnLogin();
                // uiController.ShowInventarPanel();

                /*
                --------------------------------
                
                
                 --------------------------------
                */
            }
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
    
    public static async void RegisterUser(string userInput, string passInput)
    {
        try
        {
            //legacy -> UIController
            // var uiController = UIController.Instance;
            var currentUser = await RealmController.OnPressRegister(userInput, passInput);
            if (currentUser != null) {

                SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
                
                // uiController.HideAuthenticationUIOnLogin();
                // uiController.ShowInventarPanel();
            }
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }

    
}