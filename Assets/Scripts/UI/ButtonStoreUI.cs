using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStoreUI : MonoBehaviour
{
    public Image mySeft;
    public Sprite spriteCheckbuy;
    public Sprite spriteDefault;

    public Text text;
    public Image image;

    public InfoStoreUI info;
    [Header("tham so cua Item")]
    public string id;
    public string nameWeapon;
    public float damage;
    public float maxdamage;
    public float skillDamage;
    public float maxskillDamage;
    public Sprite Sprite;
    public Sprite SpriteStore;
    public int cost;
    public int levelUnlock;
    public int videoUnlock;
    public int maxVideoUnlock;
    public bool checkBuy;
    public bool checkUse;

    public WeaponStoreUI weaponStoreUI;
    public dataID data;
    [Header("-------------------- Border -------------------")]
    public Image BorderChoice;
    public Image BorderSelect;


    private void Start()
    {
        weaponStoreUI = GetComponentInParent<WeaponStoreUI>();
        info = GameObject.FindGameObjectWithTag("storeWeapon").GetComponent<InfoStoreUI>();
        mySeft = GetComponent<Image>();
    }

    private void Update()
    {
        if (checkUse)
        {
            BorderChoice.enabled = true;
        }
        else
        {
            BorderChoice.enabled = false;
        }

        for (int i = 0; i < weaponStoreUI.weapon.Length; i++)
        {
            if (weaponStoreUI.weapon[i].id == id)
            {
                if (weaponStoreUI.weapon[i].checkBuy)
                {
                    mySeft.sprite = spriteCheckbuy;
                }
                else
                {
                    mySeft.sprite = spriteDefault;
                }
            }
        }
    }

    public void SetValues()
    {
        if (info == null)
        {
            info = GameObject.FindGameObjectWithTag("storeWeapon").GetComponent<InfoStoreUI>();
        }

        info.damage.value = damage;
        info.damage.maxValue = maxdamage;
        info.skill.value = skillDamage;
        info.skill.maxValue = maxskillDamage;

        info.cloneValueDamage = damage;
        info.cloneValueMaxDamage = maxdamage;
        info.cloneValueSkill = skillDamage;
        info.cloneValueMaxSkill = maxskillDamage;


        info.buttonUP.GetComponent<dataID>().id = id;
        info.nameItem.text = nameWeapon;

        info.imageItem.sprite = SpriteStore;

        info.imageItem.type = Image.Type.Simple;
        info.imageItem.SetNativeSize();
        info.imageItem.type = Image.Type.Sliced;

        if (levelUnlock > 50)
        {
            if (id == "42")
            {
                info.levelUnlock.text = "Daily Gift on 15th";
            }
            else if (id == "11")
            {
                info.levelUnlock.text = "Daily Gift on 7th";
            }
            else if (id == "26")
            {
                info.levelUnlock.text = "Daily Gift on 10th";
            }
            else if (id == "03")
            {
                info.levelUnlock.text = "Daily Gift on 2th";
            }
            else if (id == "23")
            {
                info.levelUnlock.text = "Gift From IAP";
            }
            else
            {
                info.levelUnlock.text = "From View Video";
            }

        }
        else
        {
            if (levelUnlock > 0)
            {
                info.levelUnlock.text = "Level " + levelUnlock;
            }
            else
            {
                info.levelUnlock.text = "Level 1";
            }
        }
        
        if (checkBuy)
        {
            info.check = true;
            info.buttonUP.SetActive(false);
            info.btnUpgrade.SetActive(true);       
        }
        else
        {
            info.check = false;
            info.buttonUP.SetActive(true);
            info.btnUpgrade.SetActive(false);
         
            if (levelUnlock >= 50)
            {
                info.imgCoins.enabled = false;
                
                if (maxVideoUnlock > 0 && id != "23")
                {
                    info.buttonUPText.text = videoUnlock  + " / " + maxVideoUnlock;
                    info.imgVideo.enabled = true;
                }
                else
                {
                    info.buttonUPText.text = "Can't Buy";
                    info.imgVideo.enabled = false;
                }
            }
            else
            {
                info.imgCoins.enabled = true;
                info.imgVideo.enabled = false;
                info.buttonUPText.text = cost.ToString();

                if (levelUnlock >= 1 && levelUnlock < 50)
                {
                    info.buttonUPText.text = "Free";
                }
            }
        }
    }
    public void setIdtoSkinUI()
    {
        weaponStoreUI.idToCheck = id;
        weaponStoreUI.CheckViewWeapon();
    }
}
