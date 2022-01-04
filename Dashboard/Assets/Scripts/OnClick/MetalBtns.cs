using System;
using Realms;
using UnityEngine;

public class MetalBtns : MonoBehaviour
{
    private Realm realm;
    public async void Start()
    {
        realm = await RealmController.GetRealm(RealmController.SyncUser);
    }

    public void BackBtn()
    {
        // realm.All<Metal>().
    }
}