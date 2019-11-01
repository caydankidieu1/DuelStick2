using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPUI : MonoBehaviour
{
    public Coins coins;
    public StoreUI storeUI;

    [Header("-----------------------------------------------")]
    public string idToCheck;

    [Header("----------------------------------- Data IAP -----------------------------")]
    public IAP[] iap;
    public Weapon weaponIAP;

    public void BuyIAP()
    {
        foreach (IAP item in iap)
        {
            if (item.id == idToCheck)
            {
                weaponIAP.checkBuy = true;
                coins.Add(item.coins);
            }
        }
    }
}
