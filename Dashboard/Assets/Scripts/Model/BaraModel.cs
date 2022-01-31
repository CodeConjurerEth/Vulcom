using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Realms;
using UnityEngine;

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
    public long Forma { get; set; } = -1;
    
    [MapTo("Diametru")]
    public double Diametru { get; set; }
    
    [MapTo("LungimeBara")] //TODO: add LungimeInitiala, data intrarii, fiecarei iesiri
    public double LungimeBara { get; set; }
    
    [MapTo("LaturaSuprafataPatrat")]
    public double LaturaSuprafataPatrat { get; set; }

    [MapTo("LungimeSuprafata")]
    public double LungimeSuprafata { get; set; }
    
    [MapTo("LatimeSuprafata")]
    public double LatimeSuprafata { get; set; }
    
    [MapTo("LaturaHexagon")]
    public double LaturaHexagon { get; set; }

    [MapTo("Grame")]
    public double Grame { get; set; }

    [MapTo("DateTime")]
    public string Date { get; set; }
    
   public enum Forme
    {
        Cerc = 0,
        Patrat = 2,
        Dreptunghi = 4,
        Hexagon = 6
    }


   public static double GetAriaCerc(double raza)
   {
       return Mathf.PI * raza * raza;
   }
   
   public static double GetAriaPatrat(double latura)
   {
       return latura * latura;
   }

   public static double GetAriaDreptunghi(double lungimeSuprafata, double latimeSuprafata)
   {
       return lungimeSuprafata * latimeSuprafata;
   }

   public static double GetAriaHexagon(double laturaHexagon)
   {
       return 3d * Mathf.Sqrt(3) / 2d * (laturaHexagon * laturaHexagon);
   }
   
   public static double GetGreutate(double ariaSectiunii, double lungimeBara, double densitate)
   {
       return ariaSectiunii * lungimeBara * densitate;
   }

   public double GetAria()
   {
       var aria = -1d;
       switch (Forma) {
           case (int)Forme.Cerc:
               aria = GetAriaCerc(Diametru / 2);
               break;
           case (int)Forme.Dreptunghi:
               aria = GetAriaDreptunghi(LungimeSuprafata, LatimeSuprafata);
               break;
           case (int)Forme.Patrat:
               aria = GetAriaPatrat(LaturaSuprafataPatrat);
               break;
           case (int)Forme.Hexagon:
               aria = GetAriaHexagon(LaturaHexagon);
               break;
           default:
               throw new Exception("Cannot find Bara Forma and calculate its Arie");
               break;
       }
       return aria;
   }
   
    public Bara() { }
    
    public Bara(string name, Metal tipMetal, long forma, double lungimeBara)
    {
        Name = name;
        TipMetal = tipMetal;
        Forma = forma;
        LungimeBara = lungimeBara;
        Date = DateTime.Now.ToString();
    }
    
}