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
    
    [MapTo("Duritate")]
    public double Duritate { get; set; }
    
    [MapTo("Lot")] 
    public string Lot { get; set; }

    [MapTo("Presa/Profil")]
    public string PresaProfil { get; set; }
    
    [MapTo("CantitateInitiala")]
    public double CantitateInitiala { get; set; }

    [MapTo("DataAchizitie")] 
    public string DataAchizitie { get; set; }
    
    [MapTo("DataExpirare")] 
    public string DataExpirare { get; set; }

    [MapTo("Culoare")] 
    public string Culoare { get; set; }
    
    [MapTo("IstorieCantitate")]
    public string IstorieCantitate { get; set; }

    public Amestec() { }
     
    public Amestec(string name, double grame)
    {
        Name = name;
        Grame = grame;
        CantitateInitiala = grame;
        IstorieCantitate = grame.ToString();
    }
}