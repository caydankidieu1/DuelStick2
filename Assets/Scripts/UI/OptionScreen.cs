using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    public int height;
    public int width;

    [Header("GameObject need option with multiScreen")]
    public GameObject leftHome;
    public GameObject level;
    public GameObject levelPVP;
    [Space]
    public GameObject panelReward;
    public GameObject titleDailyGift;
    public GameObject BtnClaim;
    public GameObject BtnHideClaim;
    [Space]
    public GameObject Store;

    void Start()
    {   
        height = Screen.height;
        width = Screen.width;

        //MoveLeftHome();
        MoveLevel();
        DailyGift();
        ScreenStore();

        Debug.Log(width / height);
    }

    void MoveLeftHome()
    {
        if (height == 1080 && width == 2160)
        {
            leftHome.GetComponent<RectTransform>().anchoredPosition = new Vector2(10,  -55);
        }

        if (height == 1440 && width == 2960)
        {
            leftHome.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -55);
        }
    }
    void MoveLevel()
    {
        var info1P = level.GetComponent<RectTransform>();
        var info2P = levelPVP.GetComponent<RectTransform>();

        if (height == 720 && width == 1600)
        {
            info1P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            info2P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
        if (height == 1080 && width == 2248)
        {
            info1P.localScale = new Vector3(0.97f, 0.97f, 0.97f);
            info2P.localScale = new Vector3(0.97f, 0.97f, 0.97f);
        }
        if (height == 720 && width == 1560)
        {
            info1P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            info2P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
        if (height == 1080 && width == 2340)
        {
            info1P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            info2P.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
    }
    void DailyGift()
    {
        var RectPanel = panelReward.GetComponent<RectTransform>();
        var RectTitleDaily = titleDailyGift.GetComponent<RectTransform>();
        var RectbtnClaim = BtnClaim.GetComponent<RectTransform>();
        var RectBtnHide = BtnHideClaim.GetComponent<RectTransform>();

        if (height == 1080 && width == 2160)
        {
            panelReward.GetComponent<GridLayoutGroup>().constraintCount = 3;
            RectPanel.anchoredPosition = new Vector2(0, 52f);

            RectTitleDaily.anchoredPosition = new Vector2(-100, 405);
            RectTitleDaily.sizeDelta = new Vector2(2086.8f, 237.4f);
           // RectTitleDaily.sizeDelta = new Vector2(2086.8f, 237.4f);
            RectTitleDaily.anchorMin = new Vector2(0.5f, 0.5f);
            RectTitleDaily.anchorMax = new Vector2(0.5f, 0.5f);

            RectbtnClaim.anchoredPosition = new Vector2(0, 120);
            RectBtnHide.anchoredPosition = new Vector2(0, 120);
        }
        if (height == 1440 && width == 2960)
        {
            panelReward.GetComponent<GridLayoutGroup>().constraintCount = 3;
            RectPanel.anchoredPosition = new Vector2(0, 52f);

            var info = RectTitleDaily;
            info.anchoredPosition = new Vector2(-100, 405);
            info.sizeDelta = new Vector2(2086.8f, 237.4f);
            //  RectTitleDaily.sizeDelta = new Vector2(2086.8f, 237.4f);
            info.anchorMin = new Vector2(0.5f, 0.5f);
            info.anchorMax = new Vector2(0.5f, 0.5f);

            RectbtnClaim.anchoredPosition = new Vector2(0, 163);
            RectBtnHide.anchoredPosition = new Vector2(0, 163);
        }
        if (height == 1080 && width == 2280)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -58);
            RectPanel.anchoredPosition = new Vector2(0, 60);
            RectbtnClaim.anchoredPosition = new Vector2(0, 158);
            RectBtnHide.anchoredPosition = new Vector2(0, 158);
        }
        if (height == 1080 && width == 2280)
        {
            RectbtnClaim.anchoredPosition = new Vector2(0, 150);
            RectBtnHide.anchoredPosition = new Vector2(0, 150);
        }
        if (height == 1440 && width == 3040)
        {
            RectPanel.anchoredPosition = new Vector2(0, 48);
            RectbtnClaim.anchoredPosition = new Vector2(0, 150);
            RectBtnHide.anchoredPosition = new Vector2(0, 150);
        }
        if (height == 1080 && width == 2340)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -56);
            RectPanel.anchoredPosition = new Vector2(0, 50);
            RectbtnClaim.anchoredPosition = new Vector2(0, 138);
            RectBtnHide.anchoredPosition = new Vector2(0, 138);
        }
        if (height == 720 && width == 1560)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -57);
            RectPanel.anchoredPosition = new Vector2(0, 50);
            RectbtnClaim.anchoredPosition = new Vector2(0, 138);
            RectBtnHide.anchoredPosition = new Vector2(0, 138);
        }
        if (height == 720 && width == 1600)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -57);
            RectPanel.anchoredPosition = new Vector2(0, 50);
            RectbtnClaim.anchoredPosition = new Vector2(0, 130);
            RectBtnHide.anchoredPosition = new Vector2(0, 130);
        }
        if (height == 1080 && width == 1920)
        {
            RectbtnClaim.anchoredPosition = new Vector2(0, 198);
            RectBtnHide.anchoredPosition = new Vector2(0, 198);
        }
        if (height == 1080 && width == 2160)
        {
            RectbtnClaim.anchoredPosition = new Vector2(0, 173);
            RectBtnHide.anchoredPosition = new Vector2(0, 173);
        }
        if (height == 1440 && width == 2560)
        {
            RectbtnClaim.anchoredPosition = new Vector2(0, 184);
            RectBtnHide.anchoredPosition = new Vector2(0, 184);
        }
        if (height == 1440 && width == 2560)
        {
            RectbtnClaim.anchoredPosition = new Vector2(0, 163);
            RectBtnHide.anchoredPosition = new Vector2(0, 163);
        }
        if (height == 1125 && width == 2436)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -41);
            RectPanel.anchoredPosition = new Vector2(0, 68);
            RectbtnClaim.anchoredPosition = new Vector2(0, 160);
            RectBtnHide.anchoredPosition = new Vector2(0, 160);
        }
        if (height == 1242 && width == 2688)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -41);
            RectPanel.anchoredPosition = new Vector2(0, 68);
            RectbtnClaim.anchoredPosition = new Vector2(0, 160);
            RectBtnHide.anchoredPosition = new Vector2(0, 160);
        }
        if (height == 828 && width == 1790)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -41);
            RectPanel.anchoredPosition = new Vector2(0, 68);
            RectbtnClaim.anchoredPosition = new Vector2(0, 160);
            RectBtnHide.anchoredPosition = new Vector2(0, 160);
        }
        if (height == 1080 && width == 2248)
        {
            RectTitleDaily.anchoredPosition = new Vector2(-95, -41);
            RectPanel.anchoredPosition = new Vector2(0, 68);
            RectbtnClaim.anchoredPosition = new Vector2(0, 160);
            RectBtnHide.anchoredPosition = new Vector2(0, 160);
        }
    }
    void ScreenStore()
    {

        var RectStore = Store.GetComponent<RectTransform>();

        if (height == 1080 && width == 2160)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1440 && width == 2960)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1080 && width == 2280)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1080 && width == 2280)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1440 && width == 3040)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1080 && width == 2340)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 720 && width == 1560)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 720 && width == 1600)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1080 && width == 1920)
        {
            RectStore.localScale = new Vector3(1, 1, 1);
        }
        if (height == 1080 && width == 2160)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1440 && width == 2560)
        {
            RectStore.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
        if (height == 1440 && width == 2560)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1125 && width == 2436)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1242 && width == 2688)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 828 && width == 1790)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        if (height == 1080 && width == 2248)
        {
            RectStore.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
}
