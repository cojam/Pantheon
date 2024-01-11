using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer 
{

    public static Dictionary<string, Weapon> weapons;

    public static void LoadWeapons(){
        weapons = new Dictionary<string, Weapon>();
        weapons.Add("Sword", new Weapon("Sword", 2.0f, 1.0f, 20, Weapon.WeaponType.MELEE, Attack.DamageType.SLASHING, "Ripoff/Weapons/LongSword"));
        weapons.Add("Bow", new Weapon("Bow", 10.0f, 1.0f, 10, Weapon.WeaponType.RANGED, Attack.DamageType.PIERCING, "Ripoff/Weapons/BowShort"));
        weapons.Add("Sniper", new Weapon("Sniper", 20.0f, 1.0f, 50, Weapon.WeaponType.RANGED, Attack.DamageType.PIERCING, "Ripoff/Weapons/SniperRifle"));

    }

    public static Weapon getWeapon(string weaponName){
        if(weapons == null){
            LoadWeapons();
        }
        return weapons[weaponName]; 
    }

}
