using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWeaponE : MonoBehaviour
{
    public string idItem;
    [Header("---------------------------------")]
    public Weapon weapon;
    public WeaponManager weaponManager;
    [Space]
    public int damageWeaponBase;
    public int damageWeaponSkill;
    [Space]
    public Transform head; // vi tri vu khi o dau
    public Transform hand; // vi tri vu khi o canh tay
    public Transform handRight; // vi tri vu khi o canh tay phai
    public Transform leg; // vi tri vu khi o ban chan

    [Header("------------------------------------")]
    public WeaponController weaponController;
    private AI ai;

    private void Start()
    {
        ai = GetComponent<AI>();
        weaponController = GetComponentInChildren<WeaponController>();
        createWeapon();
    }

    private void Update()
    {
        if (damageWeaponBase != 0)
        {
            weaponController.DamageWeapon = damageWeaponBase;
        }
        else
        {
            weaponController.DamageWeapon = weapon.damage;
        }

        if (damageWeaponSkill != 0)
        {
            weaponController.DamageSkill = damageWeaponSkill;
        }
        else
        {
            weaponController.DamageSkill = weapon.skillDamage;
        }
    }

    public void createWeapon() // khoi tao vu khi lan dau chon
    {
        if (idItem != null)
        {
            for (int i = 0; i < weaponManager.weaponItem.Length; i++)
            {
                if (weaponManager.weaponItem[i].id == idItem)
                {
                    weapon = weaponManager.weaponItem[i];
                }
            }

            GameObject mainWeapon;
            mainWeapon = Instantiate(weapon.weaponPrefab, weapon.position, Quaternion.identity) as GameObject;

            if (weapon.checkTwoHands)
            {
                GameObject CloneWeapon;
                CloneWeapon = Instantiate(weapon.weaponPrefabClone, weapon.position, Quaternion.identity) as GameObject;

                CloneWeapon.transform.SetParent(handRight);
                CloneWeapon.transform.localPosition = weapon.position2;
                CloneWeapon.transform.localRotation = Quaternion.Euler(weapon.rotation2);
                CloneWeapon.transform.localScale = weapon.Scale2;
                CloneWeapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                CloneWeapon.tag = "weaponEnemy";
                CloneWeapon.layer = 10;
                var tes = CloneWeapon.GetComponentsInChildren<Collider2D>();
                for (int i = 0; i < tes.Length; i++)
                {
                    tes[i].isTrigger = true;
                }

                mainWeapon.GetComponent<WeaponController>().rightHands = CloneWeapon.GetComponent<CloneWeaponController>();
            }

            switch (weapon.equipSlots)
            {
                case EquipmentSlot.Head:
                    mainWeapon.transform.SetParent(head);
                    break;
                case EquipmentSlot.Hands:
                    mainWeapon.transform.SetParent(hand);
                    break;
                case EquipmentSlot.Legs:
                    mainWeapon.transform.SetParent(leg);
                    break;
                default:
                    break;
            }

            mainWeapon.transform.localPosition = weapon.position;
            mainWeapon.transform.localRotation = Quaternion.Euler(weapon.rotation);
            mainWeapon.transform.localScale = weapon.Scale;
            mainWeapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            mainWeapon.tag = "weaponEnemy";
            mainWeapon.layer = 10;

            var tes1 = mainWeapon.GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < tes1.Length; i++)
            {
                tes1[i].isTrigger = true;
            }

            if (damageWeaponBase != 0)
            {
                mainWeapon.GetComponent<WeaponController>().DamageWeapon = damageWeaponBase;
            }
            else
            {
                mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;
            }

            if (damageWeaponSkill != 0)
            {
                mainWeapon.GetComponent<WeaponController>().DamageSkill = damageWeaponSkill;
            }
            else
            {
                mainWeapon.GetComponent<WeaponController>().DamageSkill = weapon.skillDamage;
            }
      
            weaponController = GetComponentInChildren<WeaponController>();
            if (ai)
            {
                ai.setWeaponController();
            }
            
        }
        else
        {
            weapon = weaponManager.weaponItem[0];

            GameObject mainWeapon;
            mainWeapon = Instantiate(weapon.weaponPrefab, weapon.position, Quaternion.identity) as GameObject;

            switch (weapon.equipSlots)
            {
                case EquipmentSlot.Head:
                    mainWeapon.transform.SetParent(head);
                    break;
                case EquipmentSlot.Hands:
                    mainWeapon.transform.SetParent(hand);
                    break;
                case EquipmentSlot.Legs:
                    mainWeapon.transform.SetParent(leg);
                    break;
                default:
                    break;
            }

            mainWeapon.transform.localPosition = weapon.position;
            mainWeapon.transform.localRotation = Quaternion.Euler(weapon.rotation);
            mainWeapon.transform.localScale = weapon.Scale;
            mainWeapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            mainWeapon.tag = "weaponEnemy";
            mainWeapon.layer = 10;

            var tes = mainWeapon.GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < tes.Length; i++)
            {
                tes[i].isTrigger = true;
            }

            /*mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;
            mainWeapon.GetComponent<WeaponController>().DamageSkill = weapon.skillDamage;*/

            if (damageWeaponBase != 0)
            {
                mainWeapon.GetComponent<WeaponController>().DamageWeapon = damageWeaponBase;
            }
            else
            {
                mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;
            }

            if (damageWeaponSkill != 0)
            {
                mainWeapon.GetComponent<WeaponController>().DamageSkill = damageWeaponSkill;
            }
            else
            {
                mainWeapon.GetComponent<WeaponController>().DamageSkill = weapon.skillDamage;
            }

            weaponController = GetComponentInChildren<WeaponController>();
            if (ai)
            {
                ai.setWeaponController();
            }
            
        }
    }
}
