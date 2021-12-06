using System.Collections.Generic;
using Realms;
public class Amestec : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    [Required]
    public string Id { get; set; }

    [MapTo("amestec")]
    [Required]
    public string Name { get; set; }
    
    [MapTo("CantitateKg")]
    public float CantitateKg { get; set; }

    [MapTo("CantitateM")]
    public float CantitateM { get; set; }
}