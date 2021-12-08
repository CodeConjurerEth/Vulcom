using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealmController
{
    public static User SyncUser;
    private static App realmApp = App.Create(Constants.Realm.AppId); // (Part 2 Sync): realmApp represents the MongoDB Realm backend application
    private static Realm realm;

    public static async Task<User> SetLoggedInUser(string userInput, string passInput)
    {
        SyncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        if (SyncUser != null) {
            realm = await GetRealm(SyncUser);
        }
        return SyncUser;
    }
    
    public static async Task<User> OnPressRegister(string userInput, string passInput)
    {
        await realmApp.EmailPasswordAuth.RegisterUserAsync(userInput, passInput);
        SyncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        realm = await GetRealm(SyncUser);
        
        return SyncUser;
    }
    
    public static async void LogOutBackend()
    {
        await SyncUser.LogOutAsync();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static async Task<Realm> GetRealm(User loggedInUser)
    {
        var syncConfiguration = new SyncConfiguration("UnityTutorialPartition", loggedInUser);
        return await Realm.GetInstanceAsync(syncConfiguration);
    }
}
