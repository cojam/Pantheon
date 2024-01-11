using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string name;
    public float range;
    public float attackSpeed;
    public int baseDamage;
    public string spritePath;
    public Attack.DamageType attackType; 

    public enum WeaponType{
        MELEE,
        RANGED
    }
    public WeaponType type;
        
    public Weapon(string name, float range, float attackSpeed, int baseDamage, WeaponType type,Attack.DamageType attackType, string spritePath)
    {
        this.name = name;
        this.range = range;
        this.attackSpeed = attackSpeed;
        this.baseDamage = baseDamage;
        this.type = type;
        this.attackType = attackType;
        this.spritePath = spritePath;

    }


    public Attack GetAttack(){
        return new Attack(baseDamage, attackSpeed, attackType);
    }

}
