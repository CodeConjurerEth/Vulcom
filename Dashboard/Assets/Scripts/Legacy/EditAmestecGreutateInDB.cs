using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditAmestecGreutateInDB : MonoBehaviour
{
    private AmestecViewData _amestecViewData;
    private void Awake()
    {
        if (!transform.parent.parent.TryGetComponent(out _amestecViewData)) { //TODO: hardcoded, change
            throw new Exception(transform.parent + "does not have a AmestecView Component");
        }
    }

    public async void ChangeAmestecGreutateInDB(string txt)
    {
        if (!string.IsNullOrEmpty(txt)) {
            var realm = await RealmController.GetRealm(RealmController.SyncUser);
            var currAmestec = _amestecViewData.GetAmestec();

            realm.Write(() => {
                var amestec = realm.All<Amestec>().First(thisAmestec => thisAmestec.Id == currAmestec.Id);
                amestec.Grame = float.Parse(txt);
            });

            //refresh amestec views
            var amestecController = AmestecController.Instance;
            await amestecController.GenerateViewNamesTask();
        }
    }
}