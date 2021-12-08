using System.Collections.Generic;
using Realms;
public class Amestec : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    [Required]
    public string Id { get; set; }

    [MapTo("numeAmestec")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("CantitateKg")]
    public float CantitateKg { get; set; }

    [MapTo("CantitateM")]
    public float CantitateM { get; set; }

    public Amestec() { }
    
    public Amestec(string id, string name, float cantitateKg, float cantitateM)
    {
        Id = id;
        Name = name;
        CantitateKg = cantitateKg;
        CantitateM = cantitateM;
    }
}