using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeapon : MonoBehaviour
{
    public bool checkWeapon; // kiem tra xem co weapon o gan co the nhat khong
    public bool destroyweapon;
    public Weapon weapon;
    [SerializeField]private Weapon weaponClone;
    public WeaponManager weaponManager;
    public Transform head; // vi tri vu khi o dau
    public Transform hand; // vi tri vu khi o canh tay trai
    public Transform handRight; // vi tri vu khi o canh tay phai
    public Transform leg; // vi tri vu khi o ban chan

    [Header("------------------------------------")]
    public SetSkillController setJoy;
    public SetSkillController setJoy2;
    public ControllerPvP PVP;
    private PlayerManager player;
    public WeaponController weaponController;
    public string idItem;

    [Header("Button")]
    public Button skillWeapon;
    public Button throwWeapon;
    public Button pickWeapon;
    public Text text;

    [Header("Quest")]
    public Quest quest;

    private void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
        createWeapon();

        setJoy.skill.onClick.RemoveAllListeners();
        setJoy.skill.onClick.AddListener(setSkill);

        setJoy.thowAndPick.onClick.RemoveAllListeners();
        setJoy.thowAndPick.onClick.AddListener(setThrow);
        setJoy.thowAndPick.onClick.AddListener(pickUpWeapon);
        //---------------------------------------------------------------------------------------
        setJoy2.skill.onClick.RemoveAllListeners();
        setJoy2.skill.onClick.AddListener(setSkill);

        setJoy2.thowAndPick.onClick.RemoveAllListeners();
        setJoy2.thowAndPick.onClick.AddListener(setThrow);
        setJoy2.thowAndPick.onClick.AddListener(pickUpWeapon);
        //----------------------------------------------------------------------------------------
        PVP.P1Skill.onClick.RemoveAllListeners();
        PVP.P1Skill.onClick.AddListener(setSkill);

        PVP.P1Thow.onClick.RemoveAllListeners();
        PVP.P1Thow.onClick.AddListener(setThrow);
        PVP.P1Thow.onClick.AddListener(pickUpWeapon);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            setSkill();
        }
    }
    public void createWeapon() // khoi tao vu khi lan dau chon
    {
        int sum = 0;
        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            //Debug.Log(weaponManager.weaponItem[i].name);
            if (weaponManager.weaponItem[i].checkUseP1)
            {
                weapon = weaponManager.weaponItem[i];
                sum++;
            }

            if (weaponManager.weaponItem[i].id == "01")
            {
                weaponClone = weaponManager.weaponItem[i];
            }
        }

        if (sum == 0)
        {
            weapon = weaponClone;
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
            CloneWeapon.tag = "weapon";
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
        mainWeapon.tag = "weapon";
        mainWeapon.layer = 8;

        mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;
        mainWeapon.GetComponent<WeaponController>().DamageSkill = weapon.skillDamage;

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
                    CloneWeapon.tag = "weapon";
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
                mainWeapon.tag = "weapon";
                mainWeapon.layer = 8;

                mainWeapon.GetComponent<WeaponController>().DamageWeapon = weapon.damage;
                mainWeapon.GetComponent<WeaponController>().DamageSkill = weapon.skillDamage;

                weaponController = GetComponentInChildren<WeaponController>();
                destroyweapon = true;

                quest.number++;

                if (GetComponentInParent<LocalForPVP>())
                {
                    GetComponentInParent<LocalForPVP>().VFX1.SetActive(false);
                    GetComponentInParent<LocalForPVP>().VFX2.SetActive(false);
                }
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

            if (weaponController)
            {
                idItem = weaponController.id;
            }
        } 
    }
}
