using System;
using UnityEngine;

public enum PotionType 
{
    Health,
    Strength,
    Speed
}

[Serializable]
public class PotionEffect
{
    public PotionType potionType;
    public int duration;
    public int power;
}
