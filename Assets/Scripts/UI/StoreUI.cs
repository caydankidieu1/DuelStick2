using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject weapon;
    public GameObject skins;
    public GameObject power;
    public GameObject iap;
    public GameObject package;

    [Header("Ins Video Store")]
    public int numberLimit = 5;
    public GameObject video;
    public GameObject video2;

    private void Update()
    {
        checkVideo();
    }
    public void Weapon()
    {
        weapon.SetActive(true);
        skins.SetActive(false);
        power.SetActive(false);
        iap.SetActive(false);
        package.SetActive(false);
    }
    public void Skins()
    {
        weapon.SetActive(false);
        skins.SetActive(true);
        power.SetActive(false);
        iap.SetActive(false);
        package.SetActive(false);
    }
    public void Power()
    {
        weapon.SetActive(false);
        skins.SetActive(false);
        power.SetActive(true);
        iap.SetActive(false);
        package.SetActive(false);
        power.GetComponent<PowerUI>().CheckViewPow();
    }
    public void IAP()
    {
        weapon.SetActive(false);
        skins.SetActive(false);
        power.SetActive(false);
        iap.SetActive(true);
        package.SetActive(false);
    }
    public void Package()
    {
        weapon.SetActive(false);
        skins.SetActive(false);
        power.SetActive(false);
        iap.SetActive(false);
        package.SetActive(true);
    }
    void checkVideo()
    {
        if (PlayerPrefs.HasKey("videoStore"))
        {
            int test = PlayerPrefs.GetInt("videoStore");
            if (test >= numberLimit)
            {
                //video.SetActive(false);
                video.GetComponent<Button>().enabled = false;
                video.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);

                video2.GetComponent<Button>().enabled = false;
                video2.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            }
            else
            {
                video.SetActive(true);
                video.GetComponent<Button>().enabled = true;
                video.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1f);

                video2.GetComponent<Button>().enabled = true;
                video2.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            }
        }
    }
}
