using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewControllerBtnWeapons : MonoBehaviour
{
    public Weapon weapons;
    public GameObject btnSelect;
    public Image mySeft;
    public Image imageWeapons;
    public Sprite borderDefault;
    public Sprite borderBuy;
    public Text name;

    [SerializeField] public bool checkP1;
    [SerializeField] public bool checkP2;

    [Header("Skins UI")]
    public WeaponStoreUI weaponUI;

    [Header("image parent")]
    public Image parent;
    public Text nameSkinParent;

    // Update is called once per frame
    void Update()
    {
        if (checkP1)
        {
            if (weapons.checkBuy)
            {
                mySeft.sprite = borderBuy;
                name.text = weapons.name;

                if (weapons.checkUseP1)
                {
                    btnSelect.SetActive(true);
                }
                else
                {
                    btnSelect.SetActive(false);
                }
            }
            else
            {
                mySeft.sprite = borderDefault;
                btnSelect.SetActive(false);
                name.text = "LOCK";
            }

            imageWeapons.sprite = weapons.SpriteStore;
        }

        if (checkP2)
        {
            if (weapons.checkBuy)
            {
                mySeft.sprite = borderBuy;
                name.text = weapons.name;

                if (weapons.checkUseP2)
                {
                    btnSelect.SetActive(true);
                }
                else
                {
                    btnSelect.SetActive(false);
                }
            }
            else
            {
                mySeft.sprite = borderDefault;
                btnSelect.SetActive(false);
                name.text = "LOCK";
            }

            imageWeapons.sprite = weapons.SpriteStore;
        }

        /*   if (checkP1 && skins.checkBuy && skins.checkUseP1)
           {
               parent.sprite = skins.Head;

               var info = parent.GetComponent<RectTransform>();
               info.localScale = new Vector3(1, 1, 1);

               var item = skins.id;
               if (item == 33 || item == 32 || item == 36)
               {
                   info.localScale = new Vector3(0.8f, 0.8f, 0.8f);
               }
           }*/
    }

    public void UseWeapons()
    {
        if (checkP1)
        {
            if (weapons.checkBuy)
            {
                weaponUI.ResetAllWeaponP1();
                weapons.checkUseP1 = true;
                weaponUI.SaveWeapon();
            }
            else
            {
                weaponUI.DefaultWeaponP1();
            }

            if (checkP1)
            {
                parent.sprite = weapons.SpriteStore;
                nameSkinParent.text = weapons.nameWeapon;

                var info = parent.GetComponent<RectTransform>();
                info.localScale = new Vector3(1, 1, 1);

                parent.type = Image.Type.Simple;
                parent.SetNativeSize();
                parent.type = Image.Type.Sliced;
            }
        }

        if (checkP2)
        {
            if (weapons.checkBuy)
            {
                weaponUI.ResetAllWeaponP2();
                weapons.checkUseP2 = true;
                weaponUI.SaveWeapon();
            }
            else
            {
                weaponUI.DefaultWeaponP2();
            }


            if (checkP2)
            {
                parent.sprite = weapons.SpriteStore;
                nameSkinParent.text = weapons.nameWeapon;

                var info = parent.GetComponent<RectTransform>();
                info.localScale = new Vector3(1, 1, 1);

                parent.type = Image.Type.Simple;
                parent.SetNativeSize();
                parent.type = Image.Type.Sliced;
            }
        }
    }

    private void OnEnable()
    {
        imageWeapons.type = Image.Type.Simple;
    }
}
