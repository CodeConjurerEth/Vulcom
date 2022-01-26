using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditAmestecGreutateInDB : MonoBehaviour
{
    private AmestecView _amestecView;
    private void Awake()
    {
        if (!transform.parent.TryGetComponent(out _amestecView)) { //TODO: hardcoded, change
            throw new Exception(transform.parent + "does not have a AmestecView Component");
        }
    }

    public async void ChangeAmestecGreutateInDB(string txt)
    {
        if (!string.IsNullOrEmpty(txt)) {
            var realm = await RealmController.GetRealm(RealmController.SyncUser);
            var currAmestec = _amestecView.GetAmestec();

            realm.Write(() => {
                var amestec = realm.All<Amestec>().First(thisAmestec => thisAmestec.Id == currAmestec.Id);
                amestec.Kg = Double.Parse(txt);
            });

            //refresh amestec views
            var amestecController = AmestecController.Instance;
            await amestecController.GenerateViewObjectsTask();
        }
    }
}