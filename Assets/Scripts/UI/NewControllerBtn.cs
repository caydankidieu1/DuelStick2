using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewControllerBtn : MonoBehaviour
{
    public Cotsume Skins;
    public Weapon Weapons;
    public Image image;
    public Text text;

    public void activelScaleImage()
    {
        if (Skins)
        {
            image.sprite = Skins.Head;
            image.type = Image.Type.Simple;
            image.SetNativeSize();
            image.type = Image.Type.Sliced;
            text.gameObject.SetActive(false);
        }

        if (Weapons)
        {
            image.sprite = Weapons.Sprite;
            image.type = Image.Type.Simple;
            image.SetNativeSize();
            image.type = Image.Type.Sliced;
            text.gameObject.SetActive(true);
            text.text = Weapons.nameWeapon;
        }
    }
}
