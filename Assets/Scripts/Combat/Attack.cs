using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Attack 
{
    public int baseDamage = 1;
    public float attackSpeed = 1.0f;

    public enum DamageType {
        BLUNT,
        SLASHING,
        PIERCING,
        EXPLOSION
    }

    public DamageType damageType = DamageType.BLUNT;    

    public Attack(int baseDamage, float attackSpeed, DamageType damageType ){
        this.baseDamage = baseDamage;
        this.attackSpeed = attackSpeed;
        this.damageType = damageType;
    }


}
