using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public List<weapon> W = new List<weapon>();

    public WeaponData (WeaponStoreUI store)
    {
        W.Clear();
        for (int i = 0; i < store.weapon.Length; i++)
        {
            weapon weapon = new weapon();
            weapon.id = store.weapon[i].id;
            weapon.checkBuy = store.weapon[i].checkBuy;
            weapon.checkUse = store.weapon[i].checkUse;

            weapon.checkUseP1 = store.weapon[i].checkUseP1;
            weapon.checkUseP2 = store.weapon[i].checkUseP2;

            weapon.damage = store.weapon[i].damage;
            weapon.damageSkill = store.weapon[i].skillDamage;

            W.Add(weapon);
        }
    }
}

[System.Serializable]
public class weapon
{
    public string id;
    public bool checkBuy;
    public bool checkUse;

    public bool checkUseP1;
    public bool checkUseP2;

    public float damage;
    public float damageSkill;
}
