using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

 public class DBController //:MonoBehaviour
{
    public static DBController Instance;
    private static Realm _realm;

    private async void OnEnable()
    {
        // if (Instance == null) {
        //     Instance = this;
        // }
        // else {
        //     Destroy(Instance);
        //     Instance = this;
        //     Debug.Log("There can't be 2 DBControllers in a Scene! Current one is on " + this.gameObject + " old one has been destroyed!");
        // }
        _realm = await RealmController.GetRealm(RealmController.SyncUser);
    }
    
    public void AddToDB(Amestec amestec)
    {
        _realm.Write(() => {
            _realm.Add(amestec);
        });
    }
    
    public void AddToDB(Bara bara)
    {
        _realm.Write(() => {
            _realm.Add(bara);
        });
    }

    public void AddToDB(Alama alama)
    {
        _realm.Write(() => {
            _realm.Add(alama);
        });
    }


    public async void RemoveAmestecFromDB(string id)
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
    
    public async void RemoveBaraFromDB(string id)
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
    
    public async void RemoveAlamaFromDB(string id)
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

    public async Task<List<Amestec>> GetAmestecListFromDB()
    {
        _realm = await RealmController.GetRealm(RealmController.SyncUser); //sync 
        var amestecList = new List<Amestec>();
        
        var amestecuri = _realm.All<Amestec>().OrderBy(amestec => amestec.CantitateKg);
        for (int index = 0; index < amestecuri.Count(); index++) {
            amestecList.Add(amestecuri.ElementAt(index));
        }

        return amestecList;
    }

    public async Task<List<Bara>> GetBaraListFromDB()
    {
        _realm = await RealmController.GetRealm(RealmController.SyncUser); //sync 
        var baraList = new List<Bara>();
        
        var bare = _realm.All<Bara>().OrderBy(bara => bara.LungimeCm);
        for (int index = 0; index < bare.Count(); index++) {
            baraList.Add(bare.ElementAt(index));
        }

        return baraList;
    }
    
    public async Task<List<Alama>> GetAlamaListFromDB()
    {
        _realm = await RealmController.GetRealm(RealmController.SyncUser); //sync 
        var alamaList = new List<Alama>();
        
        var alamuri = _realm.All<Alama>().OrderBy(alama => alama.LungimeTotalaCm);
        for (int index = 0; index < alamuri.Count(); index++) {
            alamaList.Add(alamuri.ElementAt(index));
        }

        return alamaList;
    }
    
}