using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using System.Linq;
using UnityEngine.SceneManagement;

public class RealmController
{
    public static User SyncUser;
    private static App realmApp = App.Create(Constants.Realm.AppId);
    private static Realm _realm;

    public static async Task<User> SetLoggedInUser(string userInput, string passInput)
    {
        SyncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        if (SyncUser != null) {
            _realm = await GetRealm(SyncUser);
        }
        return SyncUser;
    }
    
    public static async Task<User> OnPressRegister(string userInput, string passInput)
    {
        await realmApp.EmailPasswordAuth.RegisterUserAsync(userInput, passInput);
        SyncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        _realm = await GetRealm(SyncUser);
        
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
    
     public static void AddToDB(Amestec amestec)
    {
        _realm.Write(() => {
            _realm.Add(amestec);
        });
    }
    
    // public static void AddToDB(Bara bara)
    // {
    //     _realm.Write(() => {
    //         _realm.Add(bara);
    //     });
    // }
    //
    // public static void AddToDB(Alama alama)
    // {
    //     _realm.Write(() => {
    //         _realm.Add(alama);
    //     });
    // }
    
    /**    USE SOMETHING ELSE THAN ID TO DELETE
    public static async void RemoveAmestecFromDB(string id)
    {
        var amestecs = await GetAmestecListFromDB();
        foreach (var currentAmestec in amestecs)
            if (currentAmestec.Id.ToString() == id) {
                
                amestecs.Remove(currentAmestec);
                _realm.Write(() => {
                    _realm.Remove(currentAmestec);
                });
            }
    }
    
    public static async void RemoveBaraFromDB(string id)
    {
        var bare = await GetBaraListFromDB();
        foreach (var currentBara in bare)
            if (currentBara.Id.ToString() == id) {
                
                bare.Remove(currentBara);
                _realm.Write(() => {
                    _realm.Remove(currentBara);
                });
            }
    }
    
    public static async void RemoveAlamaFromDB(string id)
    {
        var alamuri = await GetAlamaListFromDB();
        foreach (var currentAlama in alamuri)
            if (currentAlama.Id.ToString() == id) {
                
                alamuri.Remove(currentAlama);
                _realm.Write(() => {
                    _realm.Remove(currentAlama);
                });
            }
    }
    */
    
    public static async Task<List<Amestec>> GetAmestecListFromDB()
    {
        _realm = await GetRealm(SyncUser); //sync 
        var amestecList = new List<Amestec>();
        
        var amestecuri = _realm.All<Amestec>().OrderBy(amestec => amestec.Kg);
        for (int index = 0; index < amestecuri.Count(); index++) {
            amestecList.Add(amestecuri.ElementAt(index));
        }
    
        return amestecList;
    }
    
    // public static async Task<List<Bara>> GetBaraListFromDB()
    // {
    //     _realm = await GetRealm(SyncUser); //sync 
    //     var baraList = new List<Bara>();
    //     
    //     var bare = _realm.All<Bara>().OrderBy(bara => bara.Kg);
    //     for (int index = 0; index < bare.Count(); index++) {
    //         baraList.Add(bare.ElementAt(index));
    //     }
    //
    //     return baraList;
    // }
    //
    // public static async Task<List<Alama>> GetAlamaListFromDB()
    // {
    //     _realm = await GetRealm(SyncUser); //sync 
    //     var alamaList = new List<Alama>();
    //     
    //     var alamuri = _realm.All<Alama>().OrderBy(alama => alama.Kg);
    //     for (int index = 0; index < alamuri.Count(); index++) {
    //         alamaList.Add(alamuri.ElementAt(index));
    //     }
    //
    //     return alamaList;
    // }
}
