using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIAPUI : MonoBehaviour
{
    public IAP iap;
    public Coins coins;

    public Weapon weaponIAP;

    public void BuyIAP()
    {
        weaponIAP.checkBuy = true;
        coins.Add(iap.coins);
    }
}
