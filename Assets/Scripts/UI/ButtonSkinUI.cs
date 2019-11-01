using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinUI : MonoBehaviour
{
    public int idCotsume;
    public Text nameSkin;
    public Image imageSkin;
    
    public SkinUI skinUI;
    [Header("------------- Border -------------")]
    public Image SelectBorder;
    public Image SelectBorderP2;
    public Image ChoiceBorder;

    private Button meButton;
    private Image thisImage;

    private void Start()
    {
        thisImage = GetComponent<Image>(); 
        skinUI = GetComponentInParent<SkinUI>();
        imageSkin.SetNativeSize(); 
    }

    private void Update()
    {
        for (int i = 0; i < skinUI.cotsume.Length; i++)
        {
            if (skinUI.cotsume[i].id == idCotsume)
            {
                if (skinUI.cotsume[i].checkBuy)
                {
                    thisImage.sprite = skinUI.spriteBuyCotsume;
                }
                else
                {
                    thisImage.sprite = skinUI.spriteDefault;
                }
            }
        }
    }


    public void setIdtoSkinUI()
    {
        skinUI.idToCheck = idCotsume;
        skinUI.CheckViewCotsume();
    }
}
