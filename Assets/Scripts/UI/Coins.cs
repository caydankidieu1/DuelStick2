using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text textCoins;
    public Text BattleCoins;
    public Text LuckySpinCoins;
    public WeaponStoreUI weaponUI;
    public int coins = 0;

    [Header("---------------------- Delay add Coins -----------------------")]
    public float duration = 0.5f;
    public int rateCoins = 1000;
    public int likeCoins = 1000;
    [SerializeField] private int numberView;

    // Update is called once per frame
    void Update()
    {
        textCoins.text = PlayerPrefs.GetInt("AllCoins").ToString();
        BattleCoins.text = PlayerPrefs.GetInt("AllCoins").ToString();
        LuckySpinCoins.text = PlayerPrefs.GetInt("AllCoins").ToString();
        coins = PlayerPrefs.GetInt("AllCoins");
    }
    public void RemoveADS()
    {
        ManagerAds.Ins.RemoveAds();
    }
    public void Rate()
    {
        ManagerAds.Ins.RateApp();
        StartCoroutine(test(rateCoins));
        PlayerPrefs.SetString("rate", "true");
    }
    public void LikeFanpage()
    {
        Application.OpenURL("https://www.facebook.com/LEGENDARY.Games.St/");
        StartCoroutine(test(likeCoins));
        PlayerPrefs.SetString("like", "true");
    }
    public void ResetLikeAndRate()
    {
        PlayerPrefs.DeleteKey("like");
        PlayerPrefs.DeleteKey("rate");
    }
    public void viewVideo()
    {
        if (!PlayerPrefs.HasKey("videoStore"))
        {
            PlayerPrefs.SetInt("videoStore", 1);
        }

        numberView = PlayerPrefs.GetInt("videoStore");
        ManagerAds.Ins.ShowRewardedVideo(isSuccess =>
        {
            if (isSuccess)
            {
                numberView++;
                StartCoroutine(test(300));
                weaponUI.CheckVideoRewardWeapon();
                PlayerPrefs.SetInt("videoStore", numberView);
            }
        });
    }

    public void AddMoney()//test
    {
        StartCoroutine(test(100000));
    }
    public void Buy(int value)
    {
        //int RateAdd = value;
        //coins -= RateAdd;
        //PlayerPrefs.SetInt("AllCoins", coins);

        StartCoroutine(test(-value));
    }
    public void Add(int value)
    {
        //int RateAdd = value;
        //coins += RateAdd;
        //PlayerPrefs.SetInt("AllCoins", coins);
        StartCoroutine(test(value));
    }
    public IEnumerator test(int value) // function tang hoac giam 1 gia tri trong duration(gia tri thoi gian)
    {
        //Debug.Log("active");
        int start = coins;
        int end = coins + value;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            coins = (int)Mathf.Lerp(start, end, progress);
            PlayerPrefs.SetInt("AllCoins", coins);
            yield return null;
        }

        coins = end;
        PlayerPrefs.SetInt("AllCoins", coins);
    }
}
