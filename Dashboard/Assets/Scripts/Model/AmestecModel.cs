using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;
using Realms;
public class Amestec : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("numeAmestec")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Grame")]
    public double Grame { get; set; }

    [MapTo("DateTime")] 
    public string Date { get; set; }

    public Amestec() { }
     
    public Amestec(string name, double grame)
    {
        Name = name;
        Grame = grame;
        Date = DateTime.Now.ToString();
    }
}