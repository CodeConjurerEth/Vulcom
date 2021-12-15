﻿// using System.Collections.Generic;
// using System.Linq;
// using MongoDB.Bson;
// using Realms;
// public class Oteluri : RealmObject
// {
//     [PrimaryKey]
//     [MapTo("_id")]
//     public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
//
//     [MapTo("Name")]
//     [Required]
//     public string Name { get; set; }
//     
//     [MapTo("Bars")]
//     [Backlink(nameof(Bara.TipAlama))]
//     public IQueryable<Bara> Bare { get;}
//     
//     [MapTo("Kg")]
//     public double Kg { get; set; }
//     
//     public Oteluri() { }
//     
//     public Oteluri(string name, IQueryable<Bara> bare, double kg)
//     {
//         Name = name; 
//         Bare = bare;
//         Kg = kg;
//     }
// }