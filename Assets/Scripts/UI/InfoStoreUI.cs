using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoStoreUI : MonoBehaviour
{
    public Text nameItem;
    public Image imageItem;

    [Header("values Item")]
    public Text levelUnlock;
    public Slider damage;
    public Slider skill;
    public Text CheckPlayer;

    [Header("Button Buy")]
    public GameObject buttonBuy;

    [Header("All Button Upgrade")]
    public GameObject buttonUP;
    public Text buttonUPText;
    public Image imgCoins;
    public Image imgVideo;
    public GameObject btnUpgrade;
    public GameObject buttonUpgrade;
    public Text buttonUpgradeText;

    public bool check;


    [Header("value get from buttonStoreUI")]
    public StoreUI storeUI;
    public float cloneValueDamage;
    public float cloneValueMaxDamage;
    public float cloneValueSkill;
    public float cloneValueMaxSkill;

    private void Update()
    {
        damage.value = cloneValueDamage;
        damage.maxValue = cloneValueMaxDamage;
        skill.value = cloneValueSkill;
        skill.maxValue = cloneValueMaxSkill;

        if (skill.maxValue == 0)
        {
            skill.gameObject.SetActive(false);
        }
        else
        {
            skill.gameObject.SetActive(true);
        }

        if (check)
        {
            buttonUP.SetActive(false);
            btnUpgrade.SetActive(true);
        }
        else
        {
            buttonUP.SetActive(true);
            btnUpgrade.SetActive(false);
        }
    }
}
