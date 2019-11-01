using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PickWeapon weapon;
    public Weapon[] weaponItem;

    [SerializeField]
    private string id;

    [Range(1, 5)]
    public int level;

    [SerializeField]
    List<Weapon> testWeapon = new List<Weapon>();

    void Start()
    {

        if (level > weaponItem.Length)
        {
            level = weaponItem.Length;
        }

        //weapon.weapon = weaponItem[level - 1];
    }
    public void activeWeapon()
    {
        for (int i = 0; i < weaponItem.Length; i++)
        {
            if (weaponItem[i].checkUse)
            {
                weapon.weapon = weaponItem[i];
            }
        }
    }
    void test()
    {
        for (int i = 0; i < weaponItem.Length; i++)
        {
            if (weaponItem[i].id == weapon.idItem)
            {
                weapon.weapon = weaponItem[i];
            }
        }
    }
    public void test1()
    {
        
        for (int i = 0; i < weaponItem.Length; i++)
        {
            if (weaponItem[i].checkBuy && weaponItem[i].checkUse)
            {
                testWeapon.Add(weaponItem[i]);
            }
           
        }
        weapon.weapon = testWeapon[Random.Range(0, testWeapon.Count)];
    }
}
