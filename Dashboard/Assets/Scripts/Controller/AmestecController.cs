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
        _amestecuri = new List<Amestec>();

        GetAmestecList();   //get amestecList from realm ->
        GenerateViewObjects(_amestecuri.Count); //generate the nr of amestecuri we get from realm
    }

    private void GenerateViewObjects(int nrAmestecuri)
    {
        for (int index = 0; index < nrAmestecuri; index++) {
            var newPrefab = Instantiate(amestecViewPrefab, amestecElemParent);
            AmestecView amestecView;
            if (!newPrefab.TryGetComponent(out amestecView))
                throw new Exception("No AmestecView Component is on the prefab GameObject");
            else {
                amestecView.SetAmestecValues(_amestecuri[index]);
                _amestecViews.Add(amestecView);
            }
        }
    }
    
    private void GetAmestecList() //not async .. yet?
    {
        var amestecuri = _realm.All<Amestec>().OrderBy(amestec => amestec.CantitateKg);
        for (int index = 0; index < amestecuri.Count(); index++) {
            _amestecuri.Add(amestecuri.ElementAt(index));
        }
    }

    public void AddAmestecToRealm(string id, string name, float cantitateKg)
    {
        AmestecFactory amestecFactory = new AmestecFactory();
        var newAmestec = amestecFactory.CreateAmestec(id, name, cantitateKg);

        _realm.Write(() => {
            _realm.Add(newAmestec);
        });
    }
}