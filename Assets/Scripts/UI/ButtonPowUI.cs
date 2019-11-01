using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPowUI : MonoBehaviour
{
    public string idPow;
    public Text namePowOnButton;
    public Image image;
    public Image border;

    public PowerUI powerUI;

    [Header("--------")]
    public Image SelectBorder;

    private void Start()
    {
        powerUI = GetComponentInParent<PowerUI>();
    }

    public void setIdtoSkinUI()
    {
        powerUI.idCheck = idPow;
        powerUI.CheckViewPow();
    }
}
