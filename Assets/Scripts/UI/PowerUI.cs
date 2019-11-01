using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PowerUI : MonoBehaviour
{
    [Header("Get Coins")]
    public Coins coins;

    [Header("Info Local Create Button")]
    public Transform local;
    public GameObject prefab;
    public string idCheck;

    [Header("Info Button")]
    public Text namePow;
    public Image imagePow;
    public Text numberPow;
    public Text description;

    [Space]
    public Button Buy;
    public Text costPow;
    private ButtonPowUI[] buttonPowUI;

    [Header("-------------------------- Data Power -----------------------")]
    public Power[] power;

    void Start()
    {
        LoadData();
        CreateNewSlotSkin();
        buttonPowUI = GetComponentsInChildren<ButtonPowUI>();
        defaultInfo();
    }
    void defaultInfo()
    {
        idCheck = "03";
        CheckViewPow();
    }
    public void CreateNewSlotSkin()
    {
        foreach (Power item in power)
        {
            if (item)
            {
                GameObject T = Instantiate(prefab) as GameObject;
                T.transform.SetParent(local);
                T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                T.GetComponent<ButtonPowUI>().idPow = item.id;
                T.GetComponent<ButtonPowUI>().namePowOnButton.text = item.namePower;
                T.GetComponent<ButtonPowUI>().image.sprite = item.iconPower;
            }
        }
    }
    public void CheckViewPow()
    {
        if (idCheck != null)
        {
            foreach (Power item in power)
            {
                if (item.id == idCheck)
                {
                    //Debug.Log(item.id);

                    foreach (ButtonPowUI button in buttonPowUI)
                    {
                        if (button.idPow == idCheck)
                        {
                            button.SelectBorder.enabled = true;
                        }
                        else
                        {
                            button.SelectBorder.enabled = false;
                        }
                    }
                    //Info Power
                    namePow.text = item.namePower;
                    imagePow.sprite = item.iconPower;
                    numberPow.text = item.amount.ToString();
                    description.text = item.description.ToString();
                    Buy.enabled = true;
                    costPow.text = item.cost.ToString();
                }
            }
        }
    }
    public void BuyPow()
    {
        if (idCheck != null)
        {
            foreach (Power item in power)
            {
                if (item.id == idCheck)
                {
                    //if (item.amount < item.maxAmount)
                    //{
                        Debug.Log(item.id);
                        Buy.enabled = true;
                        costPow.text = item.cost.ToString() ;

                        if (coins.coins >= item.cost)
                        {
                            //Debug.Log("you can buy");
                            coins.Buy(item.cost);
                            item.amount++;
                            numberPow.text = item.amount.ToString();

                            SaveData();
                        }
                        else
                        {
                            Debug.Log("You don't have enough money");
                        }

                       // if (item.amount > item.maxAmount)
                        //{
                        //    item.amount = item.maxAmount;
                       // }
                   // }
                }
            }
        }
    }
    public void SaveData()
    {
        SaveSystem.SavePower(this);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/Power.dat";
        if (File.Exists(path))
        {
            PowerData data = SaveSystem.LoadPower();

            for (int i = 0; i < power.Length; i++)
            {
                for (int k = 0; k < data.P.Count; k++)
                {
                    if (power[i].id == data.P[k].id)
                    {
                        power[i].amount = data.P[k].number;
                    }
                }
            }
        }
    }
}
