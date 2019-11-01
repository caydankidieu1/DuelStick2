using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSystemSkins : MonoBehaviour
{
    public Cotsume[] cotsume;

    public int hpForPVP;

    private void Start()
    {
        for (int i = 0; i < cotsume.Length; i++)
        {
            if (cotsume[i].checkUse)
            {
                hpForPVP = cotsume[i].HP;
            }
        }
    }
}
