using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnALLpackageDiscount : MonoBehaviour
{
    public Coins coins;
    public LoadData loadData;

    public IAP iap;
    public PackageStore package;
    public Weapon weaponIAP;

    public void buy()
    {
        if (iap)
        {
            weaponIAP.checkBuy = true;
            coins.Add(iap.coins);
        }

        if (package)
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

}
