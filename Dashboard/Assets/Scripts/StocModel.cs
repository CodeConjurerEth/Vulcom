using System.Collections.Generic;
using Realms;
public class Stoc : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    [Required]
    public string Id { get; set; }

    [MapTo("name")]
    [Required]
    public string Name { get; set; }
}