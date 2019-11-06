using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner2 : MonoBehaviour
{
    void OnEnable()
    {
        ManagerAds.Ins.ShowBanner();
    }
}
