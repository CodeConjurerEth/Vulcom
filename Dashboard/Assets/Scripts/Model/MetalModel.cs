using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Realms;
public class Metal : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("Name")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Bare")]
    [Backlink(nameof(Bara.TipMetal))]
    public IQueryable<Bara> Bare { get; }
    
    [MapTo("Densitate")]
    public double Densitate { get; set; }
    
    [MapTo("Kg")]
    public double Kg { get; set; }
    
    public Metal() { }
    
    public Metal(string name, double densitate)
    {
        Name = name;
        Densitate = densitate;
    }
}