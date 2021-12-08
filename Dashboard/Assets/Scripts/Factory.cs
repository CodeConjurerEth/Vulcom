using System;
using UnityEngine;

public abstract class Factory
{
    public abstract Amestec CreateRandomAmestec();
    public abstract Amestec CreateAmestec(string name, float cantitateKg, float cantitateM);
}