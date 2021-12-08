using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

public class AmestecController : MonoBehaviour
{
    public List<AmestecView> AmestecViews; //TODO: this becomes factory? gets created by factory

    private static Realm realm;
    private List<Amestec> Amestecuri;
    
    
    private async void OnEnable()
    {
        realm = await RealmController.GetRealm(RealmController.SyncUser);
        GetAmestecList();
        var currAmestec = Amestecuri[0];
        var currView = AmestecViews[0];

        currView.SetAmestecValues(currAmestec.Id, currAmestec.Name, currAmestec.CantitateKg, currAmestec.CantitateM);
    }

    private void GetAmestecList() //not async .. yet?
    {
        var amestecuri = realm.All<Amestec>().OrderBy(amestec => amestec.CantitateKg);
        Amestecuri = new List<Amestec>();
        for (int index = 0; index < amestecuri.Count(); index++) {
            Amestecuri.Add(amestecuri.ElementAt(index));
        }
    }

    public void AddAmestecToRealm(string id, string name, float cantitateKg) //TODO: cantitateM
    {
        AmestecFactory amestecFactory = new AmestecFactory();
        var newAmestec = amestecFactory.CreateRandomAmestec();
        newAmestec.Id = id;
        newAmestec.Name = name;
        newAmestec.CantitateKg = cantitateKg;

        realm.Write(() => {
            realm.Add(newAmestec);
        });
    }
}