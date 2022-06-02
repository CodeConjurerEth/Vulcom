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
    public string IstorieCantitatiCuData { get; set; }

    public Amestec() { }
     
    public Amestec(string name, double grame, string culoare, double duritate, string lot, string presaProfil, string dataAchizitie, string dataExpirare, string istorieCantitaticuData)
    {
        Name = name;
        Grame = grame;
        CantitateInitiala = grame;
        Culoare = culoare;
        Duritate = duritate;
        Lot = lot;
        PresaProfil = presaProfil;
        DataAchizitie = dataAchizitie;
        DataExpirare = dataExpirare;
        IstorieCantitatiCuData = istorieCantitaticuData;
    }
}