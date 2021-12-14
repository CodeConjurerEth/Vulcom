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
    public double Diametru { get; set; }

    [MapTo("Kg")]
    public double Kg { get; set; }
    
    public Bara() { }
    
    public Bara(string name, double diametru, double kg)
    {
        Name = name;
        Diametru = diametru;
        Kg = kg;
    }
}