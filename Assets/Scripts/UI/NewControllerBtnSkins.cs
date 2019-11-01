using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewControllerBtnSkins : MonoBehaviour
{
    public Cotsume skins;
    public GameObject btnSelect;
    public Image mySeft;
    public Image imageSkins;
    public Sprite borderDefault;
    public Sprite borderBuy;
    public Text name;

    [SerializeField] public bool checkP1;
    [SerializeField] public bool checkP2;

    [Header("Skins UI")]
    public SkinUI skinsUI;

    [Header("image parent")]
    public Image parent;
    public Text nameSkinParent;

    // Update is called once per frame
    void Update()
    {
        if (checkP1)
        {
            if (skins.checkBuy)
            {
                mySeft.sprite = borderBuy;
                name.text = skins.name;

                if (skins.checkUseP1)
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

            imageSkins.sprite = skins.Head;
        }

        if (checkP2)
        {
            if (skins.checkBuy)
            {
                mySeft.sprite = borderBuy;
                name.text = skins.name;

                if (skins.checkUseP2)
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

            imageSkins.sprite = skins.Head;
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

    public void UseCotsume()
    {
        if (checkP1)
        {
            if (skins.checkBuy)
            {
                skinsUI.ResetAllWeaponP1();
                skins.checkUseP1 = true;
                skinsUI.SaveSkin();
                skinsUI.AutoEquipSkin();
            }
            else
            {
                skinsUI.SelectDefaultWeapon();
                skinsUI.AutoEquipSkin();
            }


            if (checkP1)
            {
                parent.sprite = skins.Head;
                nameSkinParent.text = skins.nameCotsume;

                var info = parent.GetComponent<RectTransform>();
                info.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                parent.type = Image.Type.Simple;
                parent.SetNativeSize();
                parent.type = Image.Type.Sliced;

                var item = skins.id;
                if (item == 33 || item == 32 || item == 36)
                {
                    info.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }

        if (checkP2)
        {
            if (skins.checkBuy)
            {
                skinsUI.ResetAllWeaponP2();
                skins.checkUseP2 = true;
                skinsUI.SaveSkin();
                skinsUI.AutoEquipSkin();
            }
            else
            {
                skinsUI.SelectDefaultWeapon2();
                skinsUI.AutoEquipSkin();
            }


            if (checkP2)
            {
                parent.sprite = skins.Head;
                nameSkinParent.text = skins.nameCotsume;

                var info = parent.GetComponent<RectTransform>();
                info.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                parent.type = Image.Type.Simple;
                parent.SetNativeSize();
                parent.type = Image.Type.Sliced;

                var item = skins.id;
                if (item == 33 || item == 32 || item == 36)
                {
                    info.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
    }

    private void OnEnable()
    {
        imageSkins.type = Image.Type.Simple;
    }
}
