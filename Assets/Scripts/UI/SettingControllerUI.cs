using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingControllerUI : MonoBehaviour
{
    [SerializeField]private Audio audio;
    [SerializeField]private Coins coins;

    [Header("Setting control Left")]
    public GameObject RightController;
    public GameObject LeftController;

    [Header("Button")]
    public GameObject offSound;
    public GameObject offVibration;
    public GameObject offLeftC;
    [Space]
    public GameObject onSound;
    public GameObject onVibration;
    public GameObject onLeftC;

    [Header("Button on Pop-up")]
    public GameObject offSound1;
    public GameObject offVibration1;
    public GameObject offLeftC1;
    [Space]
    public GameObject onSound1;
    public GameObject onVibration1;
    public GameObject onLeftC1;

    [Header("Info Setting")]
    public bool Sounds;
    public bool Vibration;
    public bool LeftC;

    [Header("Rate , Like, Remove")]
    public bool ResetALL;
    public Button Rate;
    public GameObject numberCoinsRate;
    [Space]
    public Button Like;
    public GameObject numberCoinsLike;

    private void Start()
    {
        LoadSetting();
    }
    private void Update()
    {
        if (LeftC)
        {
            TurnOnLeftController();
        }
        else
        {
            TurnOffLeftController();
        }

        if (Sounds)
        {
            OffMute();
        }
        else
        {
            Mute();
        }

        if (Vibration)
        {
            TurnOnVibration();
        }
        else
        {
            TurnOffVibration();
        }

        if (ResetALL)
        {
            PlayerPrefs.SetString("rate", "false");
            PlayerPrefs.SetString("like", "false");
        }

        checkRate();
        CheckLike();
    }
    public void TurnOnVibration()
    {
        onVibration.SetActive(true);
        offVibration.SetActive(false);

        onVibration1.SetActive(true);
        offVibration1.SetActive(false);

        Vibration = true;
        SaveSetting();
    }
    public void TurnOffVibration()
    {
        onVibration.SetActive(false);
        offVibration.SetActive(true);

        onVibration1.SetActive(false);
        offVibration1.SetActive(true);

        Vibration = false;
        SaveSetting();
    }
    public void TurnOnLeftController()
    {
        onLeftC.SetActive(true);
        offLeftC.SetActive(false);

        offLeftC1.SetActive(false);
        onLeftC1.SetActive(true);

        RightController.SetActive(false);
        LeftController.SetActive(true);
        LeftC = true;
        SaveSetting();
    }
    public void TurnOffLeftController()
    {
        onLeftC.SetActive(false);
        offLeftC.SetActive(true);

        offLeftC1.SetActive(true);
        onLeftC1.SetActive(false);

        RightController.SetActive(true);
        LeftController.SetActive(false);
        LeftC = false;
        SaveSetting();
    }
    public void Mute()
    {
        offSound.SetActive(true);
        onSound.SetActive(false);

        offSound1.SetActive(true);
        onSound1.SetActive(false);

        audio.TurnOffMusic();
        Sounds = false;
        SaveSetting();
    }
    public void OffMute()
    {
        offSound.SetActive(false);
        onSound.SetActive(true);

        offSound1.SetActive(false);
        onSound1.SetActive(true);

        audio.TurnOnMusic();
        Sounds = true;
        SaveSetting();
    }
    public void LoadSetting()
    {
        string path = Application.persistentDataPath + "/Setting.dat";
        if (File.Exists(path))
        {
            SettingData data = SaveSystem.LoadSetting();
            Sounds = data.sounds;
            Vibration = data.Vibration;
            LeftC = data.leftC;
        }
    }
    public void SaveSetting()
    {
        SaveSystem.SaveSetting(this);
    }
    public void checkRate()
    {
        if (PlayerPrefs.HasKey("rate"))
        {
            var test = PlayerPrefs.GetString("rate");
            if (test == "true")
            {
                coins.rateCoins = 0;
                numberCoinsRate.SetActive(false);
            }
            else if (test == "false")
            {
                coins.rateCoins = 1000;
                numberCoinsRate.SetActive(true);
            }
        }
    }
    public void CheckLike()
    {
        if (PlayerPrefs.HasKey("like"))
        {
            var test = PlayerPrefs.GetString("like");
            if (test == "true")
            {
                coins.likeCoins = 0;
                numberCoinsLike.SetActive(false);
            }
            else if (test == "false")
            {
                coins.likeCoins = 1000;
                numberCoinsLike.SetActive(true);
            }
        }
    }
}
