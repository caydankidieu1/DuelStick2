using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreUI : MonoBehaviour
{
    public Transform StoreWeapon; // noi vi tri tao vu khi trong store
    public GameObject PrefabButton; // cac slot weapon trong store
    public InfoStoreUI Info;
    public Coins coins;
    public ButtonStoreUI[] ButtonStoreUI;

    [Header("Get infomation")]
    public dataID dataButtonBuy;
    public string idToCheck;


    [Header("----------------------- Data Player -------------------")]
    public level level;

    [Header("----------------------- Data Weapon -------------------")]
    public Weapon[] weapon;

    [Header("----------------------- Info cost Upgrade -------------")]
    public int coinsUpgrade = 150;
    [Header("----------------------- Video -------------------------")]
    public int SumVideo;

    [Header("Multiplayer")]
    public StoreUI store;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CountVideo"))
        {
            SumVideo = PlayerPrefs.GetInt("CountVideo");
        }
        else
        {
            PlayerPrefs.SetInt("CountVideo", 0);
            SumVideo = 0;
        }
    }

    private void Start()
    {
        createNewSlot();
        ButtonStoreUI = GetComponentsInChildren<ButtonStoreUI>();
        Info = GetComponentInChildren<InfoStoreUI>();
        Loadweapon();
        DefaultWeapon();
    }
    private void Update()
    {
        SumVideo = PlayerPrefs.GetInt("CountVideo");
    }
    public void CheckVideoRewardWeapon2()
    {
        ManagerAds.Ins.ShowRewardedVideo(isSuccess =>
        {
            if (isSuccess)
            {
                SumVideo++;
                Debug.Log(SumVideo);
                PlayerPrefs.SetInt("CountVideo", SumVideo);

                foreach (ButtonStoreUI item1 in ButtonStoreUI)
                {
                    item1.videoUnlock = SumVideo;
                }

                foreach (Weapon item in weapon)
                {
                    if (item.levelUnlock > 50 && item.videoUnlock > 1)
                    {
                        if (SumVideo >= item.videoUnlock)
                        {
                            item.checkBuy = true;

                            foreach (ButtonStoreUI item1 in ButtonStoreUI)
                            {
                                if (item1.id == item.id)
                                {
                                    item1.checkBuy = true;
                                }
                            }
                        }
                    }
                }

                if (idToCheck != null)
                {
                    foreach (Weapon item in weapon)
                    {
                        if (item.id == idToCheck)
                        {
                            foreach (ButtonStoreUI button in ButtonStoreUI)
                            {
                                if (button.id == idToCheck)
                                {
                                    button.SetValues();
                                }
                            }
                        }
                    }
                }

                SaveWeapon();
            }
        });
    }
    public void CheckVideoRewardWeapon()
    {
        SumVideo++;
        Debug.Log(SumVideo);
        PlayerPrefs.SetInt("CountVideo", SumVideo);

        foreach (ButtonStoreUI item1 in ButtonStoreUI)
        {
            item1.videoUnlock = SumVideo;
        }

        foreach (Weapon item in weapon)
        {
            if (item.levelUnlock > 50 && item.videoUnlock > 1)
            {
                if (SumVideo >= item.videoUnlock)
                {
                    item.checkBuy = true;

                    foreach (ButtonStoreUI item1 in ButtonStoreUI)
                    {
                        if (item1.id == item.id)
                        {
                            item1.checkBuy = true;
                        }
                    }
                }
            }
        }

        if (idToCheck != null)
        {
            foreach (Weapon item in weapon)
            {
                if (item.id == idToCheck)
                {
                    foreach (ButtonStoreUI button in ButtonStoreUI)
                    {
                        if (button.id == idToCheck)
                        {
                            button.SetValues();
                        }
                    }
                }
            }
        }

        SaveWeapon();
    }
    public void CheckWeaponByLevel()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            var test = PlayerPrefs.GetInt("level");
            Debug.Log(test);

            foreach (Weapon item in weapon)
            {
                if (item.levelUnlock < 50 && item.levelUnlock >= 1)
                {
                    if (test >= item.levelUnlock)
                    {
                        Debug.Log("Name weapon :" + item.nameWeapon);
                        item.checkBuy = true;

                        foreach (ButtonStoreUI item1 in ButtonStoreUI)
                        {
                            if (item1.id == item.id)
                            {
                                item1.checkBuy = true;
                            }
                        }
                    }
                }
            }

            if (idToCheck != null)
            {
                foreach (Weapon item in weapon)
                {
                    if (item.id == idToCheck)
                    {
                        foreach (ButtonStoreUI button in ButtonStoreUI)
                        {
                            if (button.id == idToCheck)
                            {
                                button.SetValues();
                            }
                        }
                    }
                }
            }

            SaveWeapon();
        }
    }
    public void DefaultWeapon()
    {
        idToCheck = "44";
        CheckViewWeapon();

        foreach (ButtonStoreUI item in ButtonStoreUI)
        {
            if (item.id == idToCheck)
            {
                //Debug.Log(item.id);
                item.SetValues();
            }
        }
    }
    public void createNewSlot()
    {
        foreach (Weapon item in weapon)
        {
            if (item)
            {
                GameObject T;
                T = Instantiate(PrefabButton) as GameObject;
                T.transform.SetParent(StoreWeapon);
                T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                T.GetComponent<ButtonStoreUI>().text.text = item.nameWeapon;
                T.GetComponent<ButtonStoreUI>().image.sprite = item.Sprite;
                T.GetComponent<ButtonStoreUI>().image.SetNativeSize();
                // Tham so cua Item
                T.GetComponent<ButtonStoreUI>().id = item.id;
                T.GetComponent<ButtonStoreUI>().nameWeapon = item.nameWeapon;
                T.GetComponent<ButtonStoreUI>().damage = item.damage;
                T.GetComponent<ButtonStoreUI>().maxdamage = item.maxdamage;
                T.GetComponent<ButtonStoreUI>().skillDamage = item.skillDamage;
                T.GetComponent<ButtonStoreUI>().maxskillDamage = item.maxskillDamage;

                T.GetComponent<ButtonStoreUI>().Sprite = item.Sprite;
                T.GetComponent<ButtonStoreUI>().SpriteStore = item.SpriteStore; //

                T.GetComponent<ButtonStoreUI>().cost = item.cost;

                T.GetComponent<ButtonStoreUI>().videoUnlock = SumVideo;
                T.GetComponent<ButtonStoreUI>().maxVideoUnlock = item.videoUnlock;
                T.GetComponent<ButtonStoreUI>().levelUnlock = item.levelUnlock;
                T.GetComponent<ButtonStoreUI>().checkBuy = item.checkBuy;
                T.GetComponent<ButtonStoreUI>().checkUse = item.checkUseP1;
            }
        }
    }
    public void buy()
    {
        if (dataButtonBuy)
        {
            string id = dataButtonBuy.id;
            int local = 0;
            Debug.Log(id);

            for (int i = 0; i < weapon.Length; i++)
            {
                if (weapon[i].id == id)
                {
                    local = i;
                }
            }
            Debug.Log(local);
            if (level.levelplayer >= weapon[local].levelUnlock) // check level
            {
                if (coins.coins >= weapon[local].cost)
                {
                    coins.Buy(weapon[local].cost);
                    weapon[local].checkBuy = true;

                    foreach (ButtonStoreUI item in ButtonStoreUI)
                    {
                        if (item.id == id)
                        {
                            Debug.Log(item.id);
                            item.checkBuy = true;
                            item.SetValues();
                        }
                    }

                    SaveWeapon();
                }
                else
                {
                    Debug.Log("You don't have enough money");
                }
            }

            if (weapon[local].levelUnlock > 50 && weapon[local].checkVideoUnlock)
            {
                CheckVideoRewardWeapon2();
            }
        }
    }
/*    public void EquipWeapon()
    {
        if (dataButtonBuy)
        {
            string id = dataButtonBuy.id;
            int local = 0;
            //Debug.Log(id);

            for (int i = 0; i < weapon.Length; i++)
            {
                if (weapon[i].id == id)
                {
                    local = i;
                }
                weapon[i].checkUse = false;

                if (checkP1)//
                {
                    weapon[i].checkUseP1 = false;//
                }

                if (checkP2)//
                {
                    weapon[i].checkUseP2 = false;//
                }//
            }
            weapon[local].checkUse = true;
       
            if (checkP1)//
            {
                weapon[local].checkUseP1 = true;//
            }

            if (checkP2)//
            {
                weapon[local].checkUseP2 = true;//
            }//

            foreach (ButtonStoreUI item in ButtonStoreUI)
            {
                if (item.id == id)
                {
                    //item.checkUse = true;

                    if (checkP1)//
                    {
                        item.checkP1 = true;
                        item.checkUseP1 = true;
                        item.checkP2 = false;
                    }//

                    if (checkP2)//
                    {
                        item.checkP2 = true;
                        item.checkUseP2 = true;
                        item.checkP1 = false;
                    }//

                    item.SetValues();
                }
                else
                {
                    item.checkUse = false;

                    if (checkP1)//
                    {
                        item.checkP1 = false;
                        item.checkUseP1 = false;
                    }//

                    if (checkP2)//
                    {
                        item.checkP2 = false;
                        item.checkUseP2 = false;
                    }//
                }
            }

            Isequip = true;
            SaveWeapon();
        }
    }
    public void UnEquipWeapon()
    {
        if (dataButtonBuy)
        {
            string id = dataButtonBuy.id;
            int local = 0;
            Debug.Log(id);

            for (int i = 0; i < weapon.Length; i++)
            {
                if (weapon[i].id == id)
                {
                    local = i;
                    weapon[local].checkUse = false;

                    if (checkP1)//
                    {
                        weapon[local].checkUseP1 = false;
                    }

                    if (checkP2)
                    {
                        weapon[local].checkUseP2 = false;
                    }//
                }

                if (weapon[i].id == "01")
                {
                    weapon[i].checkUse = true;

                    if (checkP1)//
                    {
                        weapon[i].checkUseP1 = true;
                    }

                    if (checkP2)
                    {
                        weapon[i].checkUseP2 = true;
                    }//
                }
            }

            foreach (ButtonStoreUI item in ButtonStoreUI)
            {
                if (item.id == id)
                {
                    Debug.Log(item.id);
                    item.checkUse = false;

                    if (checkP1)//
                    {
                        item.checkUseP1 = false;
                        item.checkP1 = false;
                    }

                    if (checkP2)
                    {
                        item.checkUseP2 = false;
                        item.checkP2 = false;
                    }//

                    item.SetValues();
                }

                if (item.id == "01")
                {
                    item.checkUse = true;

                    if (checkP1)//
                    {
                        item.checkUseP1 = true;
                    }

                    if (checkP2)
                    {
                        item.checkUseP2 = true;
                    }
                }
            }

            equip.gameObject.SetActive(true);
            unequip.gameObject.SetActive(false);
            IsUnEquip = true;
            SaveWeapon();
        }
    }*/
    public void UpgradeWeapon()
    {
        if (dataButtonBuy)
        {
            string id = dataButtonBuy.id;
            int local = 0;

            for (int i = 0; i < weapon.Length; i++)
            {
                if (weapon[i].id == id)
                {
                    local = i;
                }
            }

            if (weapon[local].checkBuy) // check buy
            {
                if (coins.coins >= coinsUpgrade)
                {
                    if (weapon[local].damage < weapon[local].maxdamage || weapon[local].skillDamage < weapon[local].maxskillDamage)
                    {
                        coins.Buy(coinsUpgrade);

                        /*var testValueDamage = (int)(weapon[local].damage * 10 / 100);
                        Debug.Log("10% weapon :" + testValueDamage);
                        weapon[local].damage += testValueDamage;
                        if (weapon[local].damage >= weapon[local].damage)
                        {
                            weapon[local].damage = weapon[local].damage;
                        }*/

                        var testValueDamage = 3;
                        Debug.Log("10% weapon :" + testValueDamage);
                        weapon[local].damage += testValueDamage;
                        if (weapon[local].damage >= weapon[local].damage)
                        {
                            weapon[local].damage = weapon[local].damage;
                        }

                        var testValueDamageSkill = (int)(weapon[local].skillDamage * 10 / 100);
                        Debug.Log("10% weapon :" + testValueDamageSkill);
                        weapon[local].skillDamage += testValueDamageSkill;
                        if (weapon[local].skillDamage >= weapon[local].maxskillDamage)
                        {
                            weapon[local].skillDamage = weapon[local].maxskillDamage;
                        }

                        foreach (ButtonStoreUI item in ButtonStoreUI)
                        {
                            if (item.id == id)
                            {
                                Debug.Log(item.id);
                                item.damage = weapon[local].damage;
                                item.skillDamage = weapon[local].skillDamage;
                                item.SetValues();
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("You don't have enough money");
                }
            }
        }
    }
    public void CheckViewWeapon()
    {
        if (idToCheck != null)
        {
            foreach (Weapon item in weapon)
            {
                if (item.id == idToCheck)
                {
                    
                    string id = dataButtonBuy.id;
                    int local = 0;

                    for (int i = 0; i < weapon.Length; i++)
                    {
                        if (weapon[i].id == id)
                        {
                            local = i;
                        }
                    }

                    foreach (ButtonStoreUI button in ButtonStoreUI)
                    {
                        if (button.id == idToCheck)
                        {
                            if (item.checkBuy)
                            {
                                button.checkBuy = true;
                            }

                            button.BorderSelect.enabled = true;
                            button.SpriteStore = weapon[local].SpriteStore;
                        }
                        else
                        {
                            button.BorderSelect.enabled = false;
                        }
                    }
                }
            }
        }
    }
    public void ResetAllWeaponP1()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].checkUseP1 = false;
        }

        SaveWeapon();
    }
    public void DefaultWeaponP1()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].id == "01")
            {
                weapon[i].checkUseP1 = true;
            }
            else
            {
                weapon[i].checkUseP1 = false;
            }
        }

        SaveWeapon();
    }
    public void DefaultWeaponP2()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].id == "01")
            {
                weapon[i].checkUseP2 = true;
            }
            else
            {
                weapon[i].checkUseP2 = false;
            }
        }

        SaveWeapon();
    }
    public void ResetAllWeaponP2()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].checkUseP2 = false;
        }

        SaveWeapon();
    }
    public void changeBorder()
    {
        if (idToCheck != null)
        {
            foreach (Weapon item in weapon)
            {
                if (item.id == idToCheck)
                {
                    //
                    string id = dataButtonBuy.id;
                    int local = 0;

                    for (int i = 0; i < weapon.Length; i++)
                    {
                        if (weapon[i].id == id)
                        {
                            local = i;
                        }
                    }//

                    foreach (ButtonStoreUI button in ButtonStoreUI)
                    {
                        if (button.id == idToCheck)
                        {
                            button.BorderSelect.enabled = true;
                            button.SetValues();
                        }
                        else
                        {
                            button.BorderSelect.enabled = false;
                        }
                    }
                }
            }
        }
    }
    public void SaveWeapon()
    {
        SaveSystem.SaveWeapon(this);
    }
    public void Loadweapon()
    {
        string path = Application.persistentDataPath + "/Weapon.dat";
        if (File.Exists(path))
        {
            WeaponData data = SaveSystem.LoadWeapon();

            for (int i = 0; i < weapon.Length; i++)
            {
                for (int k = 0; k < data.W.Count; k++)
                {
                    if (weapon[i].id == data.W[k].id)
                    {
                        weapon[i].checkBuy = data.W[k].checkBuy;
                        weapon[i].checkUse = data.W[k].checkUse;

                        weapon[i].checkUseP1 = data.W[k].checkUseP1;
                        weapon[i].checkUseP2 = data.W[k].checkUseP2;

                        weapon[i].damage = data.W[k].damage;
                        weapon[i].skillDamage = data.W[k].damageSkill;
                    }
                }
            }
        }
    }
    public void Reset()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].checkUse = false;
            weapon[i].checkUseP1 = false;
            weapon[i].checkUseP2 = false;

            weapon[i].checkBuy = false;
        }

        SaveWeapon();
        Loadweapon();
    }
    public void ResetVideo()
    {
        PlayerPrefs.DeleteKey("CountVideo");
    }
    public void ResetLevel()
    {
        PlayerPrefs.DeleteKey("level");
    }
    private void OnEnable()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].checkUseP1)
            {
                for (int j = 0; j < ButtonStoreUI.Length; j++)
                {
                    if (weapon[i].id == ButtonStoreUI[j].id)
                    {
                        ButtonStoreUI[j].checkUse = true;
                    }
                    else
                    {
                        ButtonStoreUI[j].checkUse = false;
                    }
                }
            }
        }
    }
}
