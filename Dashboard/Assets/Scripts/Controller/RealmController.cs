using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using System.Linq;
using MongoDB.Bson;
using UnityEngine;
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
        SyncUser = null;
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
    
    public static void AddToDB(Bara bara)
    {
        _realm.Write(() => {
            _realm.Add(bara);
        });
    }
    
    public static void AddToDB(Metal metal)
    {
        _realm.Write(() => {
            _realm.Add(metal);
        });
    }
    
    
    public static void RemoveAmestecFromDB(ObjectId id)
    {
        var amestec = _realm.Find<Amestec>(id);
        _realm.Write(() => {
            _realm.Remove(amestec);
        });
    }
    
    public static void RemoveBaraFromDB(ObjectId id)
    {
        var bara = _realm.Find<Bara>(id);
        _realm.Write(() => {
            _realm.Remove(bara);
        });
    }
    
    public static void RemoveMetalFromDB(ObjectId id)
    {
        var metal = _realm.Find<Metal>(id);
        _realm.Write(() => {
            _realm.Remove(metal);
        });
    }
    //

    // public static async Task<List<string>> GetAmestecNamesListFromDB()
    // {
    //     _realm = await GetRealm(SyncUser); //sync
    //     var amestecNamesList = new List<string>();
    //     
    //     var amestecNames = _realm.All<Amestec>().OrderBy()
    // }
    
    public static async Task<List<Amestec>> GetAmestecListFromDB()
    {
        _realm = await GetRealm(SyncUser); //sync 
        var amestecList = new List<Amestec>();
        
        var amestecuri = _realm.All<Amestec>().OrderBy(amestec => amestec.Grame);
        for (int index = 0; index < amestecuri.Count(); index++) {
            amestecList.Add(amestecuri.ElementAt(index));
        }
    
        return amestecList;
    }
    
    public static async Task<List<Bara>> GetBaraListFromDB()
    {
        _realm = await GetRealm(SyncUser); //sync 
        var baraList = new List<Bara>();
        
        var bare = _realm.All<Bara>().OrderBy(bara => bara.Grame);
        for (int index = 0; index < bare.Count(); index++) {
            baraList.Add(bare.ElementAt(index));
        }
    
        return baraList;
    }
    
    public static async Task<List<Metal>> GetMetalListFromDB()
    {
        _realm = await GetRealm(SyncUser); //sync 
        var metalList = new List<Metal>();
        
        var metale = _realm.All<Metal>().OrderBy(metal => metal.Densitate);
        for (int index = 0; index < metale.Count(); index++) {
            metalList.Add(metale.ElementAt(index));
        }
    
        return metalList;
    }
    
}
