using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShownBannerPOPUP : MonoBehaviour
{
    public bool activelInter;

    void OnEnable()
    {
        ManagerAds.Ins.ShowBanner();

        var test = Random.Range(0, 100);
        if (test >= 75 && activelInter)
        {
            ManagerAds.Ins.ShowInterstitial();
        }
    }

    void OnDisable()
    {
        ManagerAds.Ins.HideBanner();
    }
}
