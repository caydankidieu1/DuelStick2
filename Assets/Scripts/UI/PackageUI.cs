using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PackageUI : MonoBehaviour
{
    public StoreUI storeUI;
    public LoadData loadData;

    [Header("-----------------------------------------------")]
    public GameObject prefab;
    public Transform local; // noi sinh slot IAP
    public string idToCheck;

    [Header("----------------------------------- Data IAP -----------------------------")]
    public PackageStore[] packages;

    public void BuyPackage()
    {
        foreach (PackageStore item in packages)
        {
            if (item.id == idToCheck)
            {
                if (item.weapons != null)
                {
                    for (int i = 0; i < item.weapons.Length; i++)
                    {
                        item.weapons[i].checkBuy = true;
                    }

                    loadData.SaveAndLoadWeapon();
                }

                if (item.skins != null)
                {
                    for (int i = 0; i < item.skins.Length; i++)
                    {
                        item.skins[i].checkBuy = true;
                    }

                    loadData.SaveAndLoadSkins();
                }
            }
        }
    }
}
