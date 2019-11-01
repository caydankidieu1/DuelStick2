using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showVideo3 : MonoBehaviour
{
    private void OnEnable()
    {
        var rate = Random.Range(0, 100);
        if (rate >= 80)
        {
            ManagerAds.Ins.ShowInterstitial();
            Debug.Log("Show video inters on pause Pop - up");
        }
    }
}
