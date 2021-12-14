using System.Collections.Generic;
using MongoDB.Bson;
using Realms;
public class Bara : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("Name")]
    [Required]
    public string Name { get; set; }

    [MapTo("TipAlama")]
    public Alama TipAlama { get; set; }
    
    [MapTo("Diametru")]
    public float Diametru { get; set; }

    [MapTo("LungimeCM")]
    public float LungimeCm { get; set; }
    
    public Bara() { }
    
    public Bara(string name, float diametru, float lungimeCm)
    {
        Name = name;
        Diametru = diametru;
        LungimeCm = lungimeCm;
    }
}