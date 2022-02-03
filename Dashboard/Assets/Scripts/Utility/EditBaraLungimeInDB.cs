using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class EditBaraLungimeInDB : MonoBehaviour 
{
    private BaraView _baraView;
    private void Awake()
    {
        if (!transform.parent.TryGetComponent(out _baraView)) { //TODO: hardcoded, change
            throw new Exception(transform.parent + "does not have a BaraView Component");
        }
    }

    public async void ChangeBaraLengthInDB(string txt)
    {
        if (!string.IsNullOrEmpty(txt)) {
            var realm = await RealmController.GetRealm(RealmController.SyncUser);
            var currBara = _baraView.GetBara();
            realm.Write(() => {
                var bara = realm.All<Bara>().First(thisbara => thisbara.Id == currBara.Id);
                bara.LungimeBaraCM = Double.Parse(txt);
                bara.Grame = Bara.GetGreutate(bara.GetAria(), bara.LungimeBaraCM, bara.TipMetal.Densitate);
            });

            //refresh bara view
            var metalController = MetalController.Instance;
            await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]);
            MetalView.Instance.InstantiateOpenBaraMenuBtn();
        }
    }
}
