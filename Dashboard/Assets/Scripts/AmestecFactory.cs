using UnityEngine;
using Random = System.Random;

public class AmestecFactory : Factory
{
    public override Amestec CreateRandomAmestec()
    {
        Random random = new Random(); //make this random and unique somehow
        string id = random.Next(1000).ToString();
        var pItem = new Amestec(id, "", 0f, 0f);
        return pItem;
    }

    public override Amestec CreateAmestec(string name, float cantitateKg, float cantitateM)
    {
        Random random = new Random(); //make this random and unique somehow
        string id = random.Next(1000).ToString();
        var pItem = new Amestec(id, name, cantitateKg, cantitateM);
        return pItem;
    }
}