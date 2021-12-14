using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Realms;
public class Alama : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("Name")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Bars")]
    [Backlink(nameof(Bara.TipAlama))]
    public IQueryable<Bara> Bare { get;}
    
    [MapTo("LungimeTotalaCM")]
    public double LungimeTotalaCm { get; set; }
    
    public Alama() { }
    
    public Alama(string name, IQueryable<Bara> bare, double lungimeTotalaCm)
    {
        Name = name; 
        Bare = bare;
        LungimeTotalaCm = lungimeTotalaCm;
    }
}