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
    
    // [MapTo("Lot")] 
    // public string Lot { get; set; }
    
    [MapTo("CantitateInitiala")]
    public double CantitateInitiala { get; set; }

    [MapTo("DataAchizitie")] 
    public string DataAchizitie { get; set; }
    
    [MapTo("DataValabilitate")] 
    public string DataValabilitate { get; set; }

    [MapTo("Culoare")] 
    public string Culoare { get; set; }

    public Amestec() { }
     
    public Amestec(string name, double grame)
    {
        Name = name;
        Grame = grame;
        
        
        //TODO: data from InputField
        DataAchizitie = DateTime.Now.ToString(); 
        // DataValabilitate  TODO: data from InputField
        //Culoare 
    }
}