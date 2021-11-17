using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Realms.Sync;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        start();
    }

    private async Task start() //should this rly be asynk?
    {
        var myRealmAppId = "vulcom-vxnqd";
        var app = App.Create(myRealmAppId);
        var user = await app.LogInAsync(Credentials.Anonymous());
    }
}
