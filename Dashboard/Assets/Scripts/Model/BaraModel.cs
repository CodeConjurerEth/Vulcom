using System.Collections.Generic;
using Realms;
public class Bara : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    [Required]
    public string Id { get; set; }

    [MapTo("Name")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Diametru")]
    [Required]
    public float Diametru { get; set; }

    [MapTo("LungimeCM")]
    [Required]
    public float LungimeCm { get; set; }
    
    public Bara() { }
    
    public Bara(string id, string name, float diametru, float lungimeCm)
    {
        Id = id;
        Name = name;
        Diametru = diametru;
        LungimeCm = lungimeCm;
    }
}