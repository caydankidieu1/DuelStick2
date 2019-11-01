using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBtnDebug : MonoBehaviour
{

    public GameObject AllBtn;
    public bool activel;

    public void acti()
    {
        AllBtn.SetActive(!activel);
        activel = !activel;
    }
}
