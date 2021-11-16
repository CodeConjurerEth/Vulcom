using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using MongoDB.Driver;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(typeof(string).Assembly.ImageRuntimeVersion);
        
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://c2w:2seasm7sy@cluster0.tztqn.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
        var client = new MongoClient(settings);
        var database = client.GetDatabase("test");

        // mongodb+srv:c2w:<password>@cluster0.tztqn.mongodb.net/myFirstDatabase?retryWrites=true&w=majority
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
