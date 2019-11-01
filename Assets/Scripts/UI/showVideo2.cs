using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showVideo2 : MonoBehaviour
{
    [SerializeField] private SystemDamage system;
    [SerializeField] private BattleUI battle;

    private void OnEnable()
    {
        if (!battle.activelSecondChange && system.valueTime >= 10f)
        {
            ManagerAds.Ins.ShowInterstitial();
            Debug.Log("Show Video Inter for Over");
        }
    }
}
