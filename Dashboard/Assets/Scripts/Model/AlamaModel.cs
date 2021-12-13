using System.Collections.Generic;
using Realms;
public class Alama : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    [Required]
    public string Id { get; set; }

    [MapTo("Name")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("Bara")]
    [Required]
    public Bara Bara { get; set; }
    
    [MapTo("LungimeTotalaCM")]
    [Required]
    public float LungimeTotalaCm { get; set; }


    public Alama() { }
    
    public Alama(string id, string name, Bara bara, float lungimeTotalaCm) //chip example
    {
        Id = id;
        Name = name;
        Bara = bara;
        LungimeTotalaCm = lungimeTotalaCm;
    }
}