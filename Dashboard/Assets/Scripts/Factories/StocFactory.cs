// using UnityEngine;
// using Random = System.Random;
//
// public class StocFactory : Factory
// {
//     public override Amestec CreateRandomAmestec()
//     {
//         Random random = new Random(); //make this random and unique somehow
//         string id = random.Next(1000).ToString();
//         var pItem = new Amestec(id, "", 0f);
//         return pItem;
//     }
//
//     public override Amestec CreateAmestec(string id, string name, float cantitateKg)
//     {
//         return new Amestec(id, name, cantitateKg);
//     }
//
//     public override Bara CreateRandomBara()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override Bara CreateBara(string id, string name, float diametru, float lungimeCm)
//     {
//         return new Bara(id, name, diametru, lungimeCm);
//     }
//
//     public override Alama CreateRandomAlama()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public override Alama CreateAlama(string id, string name, Bara bara, float lungimeCm)
//     {
//         return new Alama(id, name, bara, lungimeCm);
//     }
//
//     private void assignId()
//     {
//         
//     }
// }