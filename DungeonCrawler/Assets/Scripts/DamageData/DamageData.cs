using UnityEngine;
using System;

[Serializable]
public class DamageData
{
    public float damage;
    public float slowDown;
    public float slowDownTime;
    public DamageType damageType;
    public GameObject source;

    public DamageData(float damage, DamageType type, float slowDown = 0f, float slowDownTime = 0f, GameObject source = null)
    {
        this.damage = damage;
        this.damageType = type;
        this.slowDown = slowDown;
        this.slowDownTime = slowDownTime;
        this.source = source;
    }
}

public enum DamageType
{
    Physical,
    Fire,
    Ice,
    Poison
}