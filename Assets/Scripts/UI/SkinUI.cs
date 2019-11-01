using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SkinUI : MonoBehaviour
{
    public Coins coins;

    [Header("Info Cotsume and Button in Info")]
    public Text nameCotsumeInfo;
    public Image Head;
    public Image Hip;
    public Image Chest;
    public Image UpperArm_L;
    public Image LowerArm_L;
    public Image UpperArm_R;
    public Image LowerArm_R;
    public Image UpperLeg_L;
    public Image LowerLeg_L;
    public Image UpperLeg_R;
    public Image LowerLeg_R;

    [Space]
    private int CloneValueHP;
    private int CloneValueMaxHP;
    public Slider HP;
    public GameObject BuyCotsume;
    public Button Buy;
    public Text costBuy;
    public Image imgCoins;
    public GameObject UpgradeCotsume;
    public Text costUpgrade;
    public Button Equip;
    public Button UnEquip;

    [Header("-----------------------------------------------")]
    public Sprite spriteDefault;
    public Sprite spriteBuyCotsume;
    public GameObject prefab;
    public Transform local; // noi sinh slot Skins
    public int idToCheck;

    [Header("-----------------------------------------------")]
    public ControllerSystemSkins systemSkins;
    public CotsumeController controller;
    public CotsumeController controllerP2;
    public ButtonSkinUI[] buttonSkinUI;

    [Header("----------------------------------- Data Cotsume -----------------------------")]
    public Cotsume[] cotsume;

    [Header("----------------------------------- cost Upgrade ------------------------------")]
    public int coinUpgrade = 200;

    void Start()
    {
        defaultInfo();
        CreateNewSlotSkin();
     
        buttonSkinUI = GetComponentsInChildren<ButtonSkinUI>();

        LoadSkin();
    }

    private void Update()
    {
        HP.value = CloneValueHP;
        HP.maxValue = CloneValueMaxHP;
    }
    void defaultInfo()
    {
        for (int i = 0; i < systemSkins.cotsume.Length; i++)
        {
            if (systemSkins.cotsume[i].checkUse)
            {
                systemSkins.hpForPVP = systemSkins.cotsume[i].HP;
            }
        }

        idToCheck = 25;
        CheckViewCotsume();
        Debug.Log("ok");
    }
    public void CreateNewSlotSkin()
    {
        foreach (Cotsume item in cotsume)
        {
            if (item)
            {
                GameObject T = Instantiate(prefab) as GameObject;
                T.transform.SetParent(local);
                T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                T.GetComponent<ButtonSkinUI>().idCotsume = item.id;
                T.GetComponent<ButtonSkinUI>().nameSkin.text = item.nameCotsume;
                T.GetComponent<ButtonSkinUI>().imageSkin.sprite = item.icon;
                T.GetComponent<ButtonSkinUI>().imageSkin.SetNativeSize();
                if (item.id == 33 || item.id == 32 || item.id == 36)
                {
                    T.GetComponent<ButtonSkinUI>().imageSkin.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
                if (item.checkBuy == false)
                {
                    T.GetComponent<ButtonSkinUI>().GetComponent<Image>().sprite = spriteDefault;
                }
                else if (item.checkBuy == true)
                {
                    T.GetComponent<ButtonSkinUI>().GetComponent<Image>().sprite = spriteBuyCotsume;

                    if (item.checkUse == true)
                    {
                        T.GetComponent<ButtonSkinUI>().ChoiceBorder.enabled = true;
                    }
                }
            }
        }
    }
    public void CheckViewCotsume()
    {
        if (idToCheck != null)
        {
            foreach (Cotsume item in cotsume)
            {
                if (item.id == idToCheck)
                {
                    //Debug.Log(item.id);
                    nameCotsumeInfo.text = item.nameCotsume;
                    Head.sprite = item.picture;

                    if (item.id == 32)
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.39f);
                    }
                    else if (item.id == 33) // vegeta
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.48f, 0.37f);
                    }
                    else if (item.id == 43)
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.36f, 0.4f);
                    }
                    else if (item.id == 36)
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.35f);
                        Head.canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(207.3f, 180f);
                    }
                    else if (item.id == 17) // venom
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.38f, 0.5f);
                    }
                    else if (item.id == 3)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(-7, 140f);
                    }
                    else if (item.id == 47)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 156f);
                    }
                    else if (item.id == 30)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 129f);
                    }
                    else if (item.id == 46)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 160);
                    }
                    else if (item.id == 20)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 160);
                    }
                    else if (item.id == 48)
                    {
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 161);
                    }
                    else
                    {
                        Head.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                        Head.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 140f);
                    }

                    Head.type = Image.Type.Sliced;
                    Head.SetNativeSize();

                    Chest.sprite = item.Chest;
                    Hip.sprite = item.Hip;
                    UpperArm_R.sprite = item.UpperArm_R;
                    LowerArm_R.sprite = item.LowerArm_R;
                    UpperArm_L.sprite = item.UpperArm_L;
                    LowerArm_L.sprite = item.LowerArm_L;
                    UpperLeg_L.sprite = item.UpperLeg_L;
                    LowerLeg_L.sprite = item.LowerLeg_L;
                    UpperLeg_R.sprite = item.UpperLeg_R;
                    LowerLeg_R.sprite = item.LowerLeg_R;

                    //
                    HP.maxValue = item.maxHPCotsume;
                    HP.value = item.HP;

                    CloneValueHP = item.HP;
                    CloneValueMaxHP = item.maxHPCotsume;

                    if (item.checkBuy == true)
                    {
                        BuyCotsume.SetActive(false);
                        UpgradeCotsume.SetActive(true);

                    }
                    else if (item.checkBuy == false)
                    {
                        BuyCotsume.SetActive(true);
                        UpgradeCotsume.SetActive(false);

                        if (item.levelUnlock > 50)
                        {
                            costBuy.text = "Daily Gift or IAP";
                            imgCoins.enabled = false;
                        }
                        else
                        {
                            costBuy.text = item.cost.ToString();
                            imgCoins.enabled = true;
                        }
            
                        Buy.enabled = true;
                    }

                    foreach (ButtonSkinUI button in buttonSkinUI)
                    {
                        if (button.idCotsume == idToCheck)
                        {
                            button.SelectBorder.enabled = true;
                        }
                        else
                        {
                            button.SelectBorder.enabled = false;
                        }
                    }
                }
            }  
        }
    }
    public void changeBorder()
    {
        if (idToCheck != null)
        {
            foreach (Cotsume item in cotsume)
            {
                if (item.id == idToCheck)
                {
                    foreach (ButtonSkinUI button in buttonSkinUI)
                    {
                        if (button.idCotsume == idToCheck)
                        {
                            button.SelectBorder.enabled = true;
                        }
                        else
                        {
                            button.SelectBorder.enabled = false;
                        }
                    }
                }
            }
        }
    }
    public void changeBorder2()
    {
        if (idToCheck != null)
        {
            foreach (Cotsume item in cotsume)
            {
                if (item.id == idToCheck)
                {
                    foreach (ButtonSkinUI button in buttonSkinUI)
                    {
                        if (button.idCotsume == idToCheck)
                        {
                            button.SelectBorder.enabled = false;
                        }
                        else
                        {
                            button.SelectBorder.enabled = false;
                        }
                    }
                }
            }
        }
    }
    public void BuyCotsumeNew()
    {
        if (idToCheck != null) //check lai gui qua nhieu goi tin
        {
            foreach (Cotsume item in cotsume)
            {
                if (item.id == idToCheck)
                {
                    if (item.levelUnlock < 50)
                    {
                        if (coins.coins >= item.cost)
                        {
                            //Debug.Log("You can buy");
                            coins.Buy(item.cost);
                            item.checkBuy = true;

                            if (item.checkBuy == true)
                            {
                                BuyCotsume.SetActive(false);
                                UpgradeCotsume.SetActive(true);

                               /* if (item.checkUse == true)
                                {
                                    Equip.gameObject.SetActive(false);
                                    UnEquip.gameObject.SetActive(true);
                                }
                                else if (item.checkUse == false)
                                {
                                    Equip.gameObject.SetActive(true);
                                    UnEquip.gameObject.SetActive(false);
                                }*/
                            }

                            foreach (ButtonSkinUI button in buttonSkinUI)
                            {
                                if (button.idCotsume == idToCheck)
                                {
                                    button.GetComponent<Image>().sprite = spriteBuyCotsume;
                                }
                            }

                            SaveSkin();
                        
                        }
                        else
                        {
                            Debug.Log("You don't have enough money");
                        }
                    }
                }
            }
        }
    }
    public void AutoEquipSkin()
    {
        foreach (Cotsume item in cotsume)
        {
            if (item.checkUseP1)
            {
                controller.cotsume = item; // set cho player mac trang phuc!
            }
            if (item.checkUseP2)
            {
                controllerP2.cotsume = item;
            }
        }
    }
    public void UpgradeCotsumeNew()
    {
        if (idToCheck != null)
        {
            foreach (Cotsume item in cotsume)
            {
                if (item.id == idToCheck)
                {
                    if (coins.coins >= coinUpgrade)
                    {
                        if (item.HP < item.maxHPCotsume)
                        {
                            coins.Buy(coinUpgrade);

                            var testValueHP = (int)(item.HP * 10 / 100);
                            Debug.Log("value HP level up: " + testValueHP);
                            item.HP += testValueHP;
                            if (item.HP >= item.maxHPCotsume)
                            {
                                item.HP = item.maxHPCotsume;
                            }

                            HP.value = item.HP;
                            CloneValueHP = item.HP;
                            CloneValueMaxHP = item.maxHPCotsume;
                        }
                    }
                }
            }

            SaveSkin();
        }
    }
    void check()
    {
        foreach (Cotsume item in cotsume)
        {
            /*if (item.id == idToCheck && item.checkBuy && item.checkUse)
            {
                Equip.gameObject.SetActive(false);
                UnEquip.gameObject.SetActive(true);
            }*/

            if (item.id == idToCheck && (item.checkUseP1 || item.checkUseP2))
            {
                Equip.gameObject.SetActive(false);
                UnEquip.gameObject.SetActive(true);
            }
        }
    }
    void checkAllCotsumeUse() // do something when don't equip any cotsume
    {
        int sum = 0;
        for (int i = 0; i < cotsume.Length; i++)
        {
            if (cotsume[i].checkUse == false)
            {
                sum++;
            }
        }

        if (sum >= cotsume.Length -1)
        {
            Debug.Log("You don't use any Cotsume");
        }
        else
        {
            Debug.Log("Great You have Cotsume");
        }
    }
    public void ResetAllWeaponP1()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            cotsume[i].checkUseP1 = false;
        }

        SaveSkin();
    }
    public void ResetAllWeaponP2()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            cotsume[i].checkUseP2 = false;
        }

        SaveSkin();
    }
    public void SelectDefaultWeapon()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            if (cotsume[i].id == 1)
            {
                cotsume[i].checkUseP1 = true;
            }
            else
            {
                cotsume[i].checkUseP1 = false;
            }
        }

        SaveSkin();
    }
    public void SelectDefaultWeapon2()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            if (cotsume[i].id == 1)
            {
                cotsume[i].checkUseP2 = true;
            }
            else
            {
                cotsume[i].checkUseP2 = false;
            }
        }

        SaveSkin();
    }
    public void SaveSkin()
    {
        SaveSystem.SaveSkin(this);
    }
    public void LoadSkin()
    {
        string pathSkin = Application.persistentDataPath + "/Skin.dat";
        if (File.Exists(pathSkin))
        {
            SkinData data = SaveSystem.LoadSkin();

            for (int i = 0; i < cotsume.Length; i++)
            {
                for (int k = 0; k < data.W.Count; k++)
                {
                    if (cotsume[i].id == data.W[k].id)
                    {
                        cotsume[i].checkBuy = data.W[k].checkBuy;
                        cotsume[i].checkUse = data.W[k].checkUse;
                        cotsume[i].checkUseP1 = data.W[k].checkUseP1;
                        cotsume[i].checkUseP2 = data.W[k].checkUseP2;
                        cotsume[i].HP = data.W[k].HP;
                    }
                }
            }

            AutoEquipSkin();
        }
    }
    public void Reset()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            cotsume[i].checkBuy = false;
            cotsume[i].checkUse = false;
            cotsume[i].checkUseP1 = false;
            cotsume[i].checkUseP2 = false;
        }

        SaveSkin();
        LoadSkin();
    }
}
