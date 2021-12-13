using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

public class AmestecController : MonoBehaviour
{
    [SerializeField] private GameObject amestecViewPrefab;
    [SerializeField] private Transform amestecElemParent;

    private static Realm _realm;
    private List<AmestecView> _amestecViews; 
    private List<Amestec> _amestecuri;

    private async void OnEnable()
    {
        _realm = await RealmController.GetRealm(RealmController.SyncUser);
        _amestecViews = new List<AmestecView>();
        
        GenerateViewObjects(); //generate the nr of amestecuri we get from realm
    }
    
    public void AddAmestecToDB(string id, string name, float cantitateKg)
    {
        Amestec newAmestec = new Amestec(id, name, cantitateKg);

        _realm.Write(() => {
            _realm.Add(newAmestec);
        });
    }

    public async void RemoveAmestecFromDB(string id)
    {
        _amestecuri = await GetAmestecListFromDB();
        foreach (var currentAmestec in _amestecuri)
            if (currentAmestec.Id == id) {
                
                _amestecuri.Remove(currentAmestec);
                _realm.Write(() => {
                    _realm.Remove(currentAmestec);
                });
            }
    }
    
    public async void GenerateViewObjects() //it was private, better public I think!
    {
        ClearExistingViewObj();
        _amestecuri = await GetAmestecListFromDB();
        
        foreach(var currentAmestec in _amestecuri) {
            var newPrefab = Instantiate(amestecViewPrefab, amestecElemParent);
            AmestecView amestecView;
            if (!newPrefab.TryGetComponent(out amestecView))
                throw new Exception("No AmestecView Component is on the prefab GameObject");
            else {
                amestecView.SetAmestecValues(currentAmestec);
                _amestecViews.Add(amestecView);
            }
        }
    }
    
    private async Task<List<Amestec>> GetAmestecListFromDB()
    {
        _realm = await RealmController.GetRealm(RealmController.SyncUser); //sync 
        var amestecList = new List<Amestec>();
        
        var amestecuri = _realm.All<Amestec>().OrderBy(amestec => amestec.CantitateKg);
        for (int index = 0; index < amestecuri.Count(); index++) {
            amestecList.Add(amestecuri.ElementAt(index));
        }

        return amestecList;
    }

    
    private void ClearExistingViewObj()
    {
        _amestecViews.Clear();
        ClearChildrenOf(amestecElemParent);
    }
    
    private void ClearChildrenOf(Transform parentObj)
    {
        int i = 0;
        
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[parentObj.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in parentObj)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

}