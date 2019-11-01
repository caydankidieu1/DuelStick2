using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageButtonUI : MonoBehaviour
{
    public PackageStore package;
    public LoadData loadData;

    [Header("")]
    public Text namePack;
    public Image imagePack;
    public Text Description;
    public Text costPack;

    private void Start()
    {
        namePack.text = package.name;
        imagePack.sprite = package.icon;
        var arrayTest = package.description.Split('-');
        if (arrayTest.Length < 2)
        {
            Description.alignment = TextAnchor.MiddleCenter;
            Description.fontSize = 60;
        }
        Description.text = package.description;
       // costPack.text = package.cost + " $";
    }

    public void BuyPackage()
    {
        if (package.weapons != null)
        {
            for (int i = 0; i < package.weapons.Length; i++)
            {
                package.weapons[i].checkBuy = true;
            }

            loadData.SaveAndLoadWeapon();
        }

        if (package.skins != null)
        {
            for (int i = 0; i < package.skins.Length; i++)
            {
                package.skins[i].checkBuy = true;
            }

            loadData.SaveAndLoadSkins();
        }
    }
}
