using System;
using UnityEngine;

public abstract class Factory
{
    public abstract Amestec CreateRandomAmestec();
    public abstract Amestec CreateAmestec(string id, string name, float cantitateKg);
}