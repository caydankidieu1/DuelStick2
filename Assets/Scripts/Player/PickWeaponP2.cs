using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeaponP2 : MonoBehaviour
{
    public bool checkWeapon; // kiem tra xem co weapon o gan co the nhat khong
    public bool destroyweapon;
    public Weapon weapon;
    public WeaponManager weaponManager;
    public Transform head; // vi tri vu khi o dau
    public Transform hand; // vi tri vu khi o canh tay
    public Transform handRight; // vi tri vu khi o canh tay phai
    public Transform leg; // vi tri vu khi o ban chan

    [Header("------------------------------------")]
    public ControllerPvP PVP;
    private EnemyManager player;
    private WeaponController weaponController;
    public string idItem;

    [Header("Button")]
    public Text text;

    private void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
        createWeapon();

        PVP.P2Skill.onClick.RemoveAllListeners();
        PVP.P2Skill.onClick.AddListener(setSkill);

        PVP.P2Thow.onClick.RemoveAllListeners();
        PVP.P2Thow.onClick.AddListener(setThrow);
        PVP.P2Thow.onClick.AddListener(pickUpWeapon);
    }
    public void createWeapon() // khoi tao vu khi lan dau chon
    {
        int sum = 0;
        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            if (weaponManager.weaponItem[i].checkUseP2)
            {
                weapon = weaponManager.weaponItem[i];
                sum++;
            }
        }

        if (sum == 0)
        {
            weapon = weaponManager.weaponItem[0];
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

        mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;

        weaponController = GetComponentInChildren<WeaponController>();
    }
    public void pickUpWeapon() //kiem tra xem co cam vu khi khong neu co thi set pickup cua vu khi vao button
    {
        if (weaponController)
        {
            checkWeapon = false;
        }

        if (!weaponController)
        {
            if (checkWeapon)
            {
                for (int i = 0; i < weaponManager.weaponItem.Length; i++)
                {
                    if (weaponManager.weaponItem[i].id == idItem)
                    {
                        weapon = weaponManager.weaponItem[i];
                    }
                }
                text.text = "Throw";
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
                    CloneWeapon.layer = 8;

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

                mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;

                weaponController = GetComponentInChildren<WeaponController>();
                destroyweapon = true;
            }
        }
        else Debug.Log("don't see any weapon close!");
    }
    public void setSkill() // kiem tra xem co cam vu khi khong neu co thi set skill cua vu khi vao button
    {
        if (weaponController)
        {
            weaponController.Skill();
        }
        else Debug.Log("don't have weapon");
    }
    public void setThrow() // kiem tra xem co cam vu khi khong neu co thi set throw cua vu khi vao button
    {
        if (weaponController)
        {
            text.text = "Pick";
            weaponController.Throw();
        }
        else Debug.Log("don't have weapon");
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("unweapon"))
        {
            checkWeapon = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("unweapon"))
        {
            checkWeapon = true;
            if (destroyweapon)
            {
                Destroy(col.gameObject);
                destroyweapon = false;
            }

            idItem = col.GetComponent<WeaponController>().id;
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("unweapon"))
        {
            checkWeapon = true;
            if (destroyweapon)
            {
                Destroy(col.gameObject);
                destroyweapon = false;
            }

            idItem = weaponController.id;
        }
    }
}
