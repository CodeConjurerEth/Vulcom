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

    [MapTo("TipMetal")]
    public Metal TipMetal { get; set; }
    
    [MapTo("Forma")]
    public long Forma { get; set; }
    
    [MapTo("Diametru")]
    public double Diametru { get; set; }
    
    [MapTo("Latura")]
    public double Latura { get; set; }
    
    [MapTo("Lungime")]
    public double Lungime { get; set; }
    
    [MapTo("Latime")]
    public double Latime { get; set; }

    [MapTo("Kg")]
    public double Kg { get; set; }

   public enum Forme
    {
        Cerc = 0,
        Patrat = 2,
        Dreptunghi = 4,
        Hexagon = 6
    }
    
    public Bara() { }
    
    public Bara(string name, Metal tipMetal, double diametru, double kg)
    {
        Name = name;
        TipMetal = tipMetal;
        Diametru = diametru;
        Kg = kg;
    }
    //
    // public Bara(string name, Metal tipMetal, double latura)
    // {
    //     Name = name;
    //     TipMetal = tipMetal;
    //     Latime = latura;
    // }
    //
    // public Bara(string name, Metal tipMetal, double lungime, double latime)
    // {
    //     Name = name;
    //     TipMetal = tipMetal;
    //     Lungime = lungime;
    //     Latime = latime;
    // }
    //
    // +HEXAGONAL
}