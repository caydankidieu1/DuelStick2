using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAll1P : MonoBehaviour
{
    [Header("Info P1")]
    public Transform localCreate;
    public GameObject prefabSlots;
    public Image imagePlayerSelect;
    public Text namePlayerSelect;

    [Header("Info From SkinUI")]
    public SkinUI skinsUI;

    public WeaponStoreUI weaponUI;

    [Header("Info Pop - Up")]
    public bool checkSelect;
    public GameObject POPUP;
    public Image mainImageSelectSKins;
    public Sprite b1;
    public Sprite b2;
    public GameObject animationTest;
    public GameObject panelHide;
    public CheckPhase checkPhase;

    void Start()
    {
        CreateNewOne();
        checkSelect = false;
        animationTest.SetActive(false);
    }
    private void Update()
    {
        if (checkSelect)
        {
            mainImageSelectSKins.sprite = b2;
        }
        else
        {
            mainImageSelectSKins.sprite = b1;
        }
    }
    void CreateNewOne()
    {
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            GameObject T = Instantiate(prefabSlots);
            T.transform.SetParent(localCreate);
            T.gameObject.SetActive(true);
            T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            var info = T.GetComponent<NewControllerBtnSkins>();
            info.skins = skinsUI.cotsume[i];
            info.skinsUI = skinsUI;
            info.parent = imagePlayerSelect;
            info.nameSkinParent = namePlayerSelect;

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
    public void hidePOPUP()
    {
        checkSelect = !checkSelect;
        if (checkSelect)
        {
            activelPOPUP();
        }
        else
        {
            UnActivelPOPUP();
        }
    }
    void activelPOPUP()
    {
        POPUP.SetActive(true);
        panelHide.SetActive(true);
        animationTest.SetActive(true);
        checkSelect = true;
    }
    void UnActivelPOPUP()
    {
        POPUP.SetActive(false);
        panelHide.SetActive(false);
        animationTest.SetActive(false);
        checkSelect = false;
    }
    public void StartRandom()
    {
        List<Cotsume> All = new List<Cotsume>();
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            if (skinsUI.cotsume[i].checkBuy)
            {
                All.Add(skinsUI.cotsume[i]);
            }
        }

        skinsUI.ResetAllCotsumeP1();

        var value = Random.Range(0, All.Count - 1);
        var id = All[value].id;
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            if (skinsUI.cotsume[i].id == id)
            {
                skinsUI.cotsume[i].checkUseP1 = true;
            }
        }

        skinsUI.AutoEquipSkin();
        skinsUI.SaveSkin();

        //------------------------------------------------------------------------------------
        List<Weapon> AllWeapon = new List<Weapon>();
        for (int i = 0; i < weaponUI.weapon.Length; i++)
        {
            if (weaponUI.weapon[i].checkBuy)
            {
                AllWeapon.Add(weaponUI.weapon[i]);
            }
        }

        weaponUI.ResetAllWeaponP1();

        var valueW = Random.Range(0, AllWeapon.Count - 1);
        var idW = AllWeapon[valueW].id;
        for (int i = 0; i < weaponUI.weapon.Length; i++)
        {
            if (weaponUI.weapon[i].id == idW)
            {
                weaponUI.weapon[i].checkUseP1 = true;
            }
        }
        weaponUI.SaveWeapon();

        //---------------------------------------------------------------------------------------

        var randomMap = Random.Range(0, checkPhase.btnWaver.Length - 1);
        checkPhase.btnWaver[randomMap].GetComponent<Phase>().ReloadWave();
    }
    void showFirstLog()
    {
        for (int i = 0; i < skinsUI.cotsume.Length; i++)
        {
            if (skinsUI.cotsume[i].checkUseP1)
            {
                imagePlayerSelect.sprite = skinsUI.cotsume[i].Head;
                namePlayerSelect.text = skinsUI.cotsume[i].nameCotsume;
            }
        }

        imagePlayerSelect.type = Image.Type.Simple;
        imagePlayerSelect.SetNativeSize();
        imagePlayerSelect.type = Image.Type.Sliced;
    }
    private void OnEnable()
    {
        panelHide.SetActive(false);
        showFirstLog();
        UnActivelPOPUP();
    }
    private void OnDisable()
    {
        panelHide.SetActive(false);
    }
}
