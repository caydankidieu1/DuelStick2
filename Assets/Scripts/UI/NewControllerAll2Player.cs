using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewControllerAll2Player : MonoBehaviour
{
    [Header("Info P1")]
    public Transform localCreate1;
    public GameObject prefabSlots1;
    public Image imagePlayerSelect1;
    public Text namePlayerSelect1;

    [Header("Info P2")]
    public Transform localCreate2;
    public GameObject prefabSlots2;
    public Image imagePlayerSelect2;
    public Text namePlayerSelect2;

    [Header("Info From SkinUI")]
    public SkinUI skinsUI;

    public Sprite b1;
    public Sprite b2;

    [Header("Info Pop - Up Skins")]
    public bool checkSelect1;
    public GameObject POPUP1;
    public Image mainImageSelectSKinsP1;
    public bool checkSelect2;
    public GameObject POPUP2;
    public Image mainImageSelectSKinsP2;

    [Header("Info Weapon P1")]
    public Transform localCreateW1;
    public GameObject prefabSlotsW1;
    public Image imagePlayerSelectW1;
    public Text namePlayerSelectW1;

    [Header("Info Weapon P2")]
    public Transform localCreateW2;
    public GameObject prefabSlotsW2;
    public Image imagePlayerSelectW2;
    public Text namePlayerSelectW2;

    [Header("Info From SkinUI")]
    public WeaponStoreUI weaponUI;

    [Header("Info Pop - Up Weaponw")]
    public bool checkSelectW1;
    public GameObject POPUPW1;
    public Image mainImageSelectSKinsPW1;
    public bool checkSelectW2;
    public GameObject POPUPW2;
    public Image mainImageSelectSKinsPW2;
    public GameObject panelHide;

    [Header("Animation Box")]
    public GameObject animBoxSkinsP1;
    public GameObject animBoxSkinsP2;
    public GameObject animBoxWeaponsP1;
    public GameObject animBoxWeaponsP2;

    void Start()
    {
        CreateNewOne();
        CreateNewTwo();

        CreateNewWeapon1();
        CreateNewWeapon2();
    }

    private void Update()
    {
        if (checkSelect1)
        {
            mainImageSelectSKinsP1.sprite = b2;
            animBoxSkinsP1.SetActive(true);
        }
        else
        {
            mainImageSelectSKinsP1.sprite = b1;
            animBoxSkinsP1.SetActive(false);
        }

        if (checkSelect2)
        {
            mainImageSelectSKinsP2.sprite = b2;
            animBoxSkinsP2.SetActive(true);
        }
        else
        {
            mainImageSelectSKinsP2.sprite = b1;
            animBoxSkinsP2.SetActive(false);
        }
        //-----------------------------------------------------------
        if (checkSelectW1)
        {
            mainImageSelectSKinsPW1.sprite = b2;
            animBoxWeaponsP1.SetActive(true);
        }
        else
        {
            mainImageSelectSKinsPW1.sprite = b1;
            animBoxWeaponsP1.SetActive(false);
        }

        if (checkSelectW2)
        {
            mainImageSelectSKinsPW2.sprite = b2;
            animBoxWeaponsP2.SetActive(true);
        }
        else
        {
            mainImageSelectSKinsPW2.sprite = b1;
            animBoxWeaponsP2.SetActive(false);
        }
    }

    void CreateNewOne()
    {
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            GameObject T = Instantiate(prefabSlots1);
            T.transform.SetParent(localCreate1);
            T.gameObject.SetActive(true);
            T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            var info = T.GetComponent<NewControllerBtnSkins>();

            info.checkP1 = true;
            info.checkP2 = false;

            info.skins = skinsUI.cotsume[i];
            info.skinsUI = skinsUI;
            info.parent = imagePlayerSelect1;
            info.nameSkinParent = namePlayerSelect1;

            info.imageSkins.sprite = skinsUI.cotsume[i].Head;
            info.imageSkins.GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);

            var item = skinsUI.cotsume[i].id;
            if (item == 33 || item == 32 || item == 36)
            {
                info.imageSkins.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }

            info.imageSkins.type = Image.Type.Simple;
            info.imageSkins.SetNativeSize();
            info.imageSkins.type = Image.Type.Sliced;
        }
    }
    void CreateNewTwo()
    {
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            GameObject T = Instantiate(prefabSlots2);
            T.transform.SetParent(localCreate2);
            T.gameObject.SetActive(true);
            T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            var info = T.GetComponent<NewControllerBtnSkins>();

            info.checkP1 = false;
            info.checkP2 = true;

            info.skins = skinsUI.cotsume[i];
            info.skinsUI = skinsUI;
            info.parent = imagePlayerSelect2;
            info.nameSkinParent = namePlayerSelect2;

            info.imageSkins.sprite = skinsUI.cotsume[i].Head;
            info.imageSkins.GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);

            var item = skinsUI.cotsume[i].id;
            if (item == 33 || item == 32 || item == 36)
            {
                info.imageSkins.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }

            info.imageSkins.type = Image.Type.Simple;
            info.imageSkins.SetNativeSize();
            info.imageSkins.type = Image.Type.Sliced;
        }
    }

    void CreateNewWeapon1()
    {
        for (int i = 0; i < weaponUI.weapon.Length; i++)
        {
            GameObject T = Instantiate(prefabSlotsW1);
            T.transform.SetParent(localCreateW1);
            T.gameObject.SetActive(true);
            T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            var info = T.GetComponent<NewControllerBtnWeapons>();

            info.checkP1 = true;
            info.checkP2 = false;

            info.weapons = weaponUI.weapon[i];
            info.weaponUI = weaponUI;
            info.parent = imagePlayerSelectW1;
            info.nameSkinParent = namePlayerSelectW1;

            info.imageWeapons.sprite = weaponUI.weapon[i].SpriteStore;
            info.imageWeapons.GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);

            info.imageWeapons.type = Image.Type.Simple;
            info.imageWeapons.SetNativeSize();
            info.imageWeapons.type = Image.Type.Sliced;
        }
    }

    void CreateNewWeapon2()
    {
        for (int i = 0; i < weaponUI.weapon.Length; i++)
        {
            GameObject T = Instantiate(prefabSlotsW2);
            T.transform.SetParent(localCreateW2);
            T.gameObject.SetActive(true);
            T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            var info = T.GetComponent<NewControllerBtnWeapons>();

            info.checkP1 = false;
            info.checkP2 = true;

            info.weapons = weaponUI.weapon[i];
            info.weaponUI = weaponUI;
            info.parent = imagePlayerSelectW2;
            info.nameSkinParent = namePlayerSelectW2;

            info.imageWeapons.sprite = weaponUI.weapon[i].SpriteStore;
            info.imageWeapons.GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);

            info.imageWeapons.type = Image.Type.Simple;
            info.imageWeapons.SetNativeSize();
            info.imageWeapons.type = Image.Type.Sliced;
        }
    }

    public void hidePOPUPSkinsP1()
    {
        checkSelect1 = !checkSelect1;
        if (checkSelect1)
        {
            activelPOPUP1();
        }
        else
        {
            UnActivelAll();
        }
    }
    public void hidePOPUPSkinsP2()
    {
        checkSelect2 = !checkSelect2;
        if (checkSelect2)
        {
            activelPOPUP2();
        }
        else
        {
            UnActivelAll();
        }
    }
    public void hidePOPUPWeaponP1()
    {
        checkSelectW1 = !checkSelectW1;
        if (checkSelectW1)
        {
            activelPOPUPWeapon1();
        }
        else
        {
            UnActivelAll();
        }
    }
    public void hidePOPUPWeaponP2()
    {
        checkSelectW2 = !checkSelectW2;
        if (checkSelectW2)
        {
            activelPOPUPWeapon2();
        }
        else
        {
            UnActivelAll();
        }
    }


    void activelPOPUP1()
    {
        POPUP1.SetActive(true);
        POPUP2.SetActive(false);
        POPUPW1.SetActive(false);
        POPUPW2.SetActive(false);
        checkSelect1 = true;
        checkSelect2 = false;
        checkSelectW1 = false;
        checkSelectW2 = false;

        panelHide.SetActive(true);
    }
    void activelPOPUP2()
    {
        POPUP1.SetActive(false);
        POPUP2.SetActive(true);
        POPUPW1.SetActive(false);
        POPUPW2.SetActive(false);
        checkSelect1 = false;
        checkSelect2 = true;
        checkSelectW1 = false;
        checkSelectW2 = false;

        panelHide.SetActive(true);
    }
    void activelPOPUPWeapon1()
    {
        POPUP1.SetActive(false);
        POPUP2.SetActive(false);
        POPUPW1.SetActive(true);
        POPUPW2.SetActive(false);
        checkSelect1 = false;
        checkSelect2 = false;
        checkSelectW1 = true;
        checkSelectW2 = false;

        panelHide.SetActive(true);
    }
    void activelPOPUPWeapon2()
    {
        POPUP1.SetActive(false);
        POPUP2.SetActive(false);
        POPUPW1.SetActive(false);
        POPUPW2.SetActive(true);
        checkSelect1 = false;
        checkSelect2 = false;
        checkSelectW1 = false;
        checkSelectW2 = true;

        panelHide.SetActive(true);
    }

    void UnActivelAll()
    {
        POPUP1.SetActive(false);
        POPUP2.SetActive(false);
        POPUPW1.SetActive(false);
        POPUPW2.SetActive(false);
        checkSelect1 = false;
        checkSelectW1 = false;
        checkSelect2 = false;
        checkSelectW2 = false;

        panelHide.SetActive(false);
    }
    private void OnEnable()
    {
        checkSelect1 = false;
        checkSelect2 = false;
        checkSelectW1 = false;
        checkSelectW2 = false;
        POPUP1.SetActive(false);
        POPUP2.SetActive(false);
        POPUPW1.SetActive(false);
        POPUPW2.SetActive(false);

        panelHide.SetActive(false);
    }
    private void OnDisable()
    {
        panelHide.SetActive(false);
    }
}
