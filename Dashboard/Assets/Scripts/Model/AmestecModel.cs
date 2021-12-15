using System.Collections.Generic;
using MongoDB.Bson;
using Realms;
public class Amestec : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("numeAmestec")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Kg")]
    public double Kg { get; set; }

    public Amestec() { }
     
    public Amestec(string name, double kg)
    {
        Name = name;
        Kg = kg;
    }
}