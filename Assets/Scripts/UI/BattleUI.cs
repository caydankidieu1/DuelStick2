using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject popUp;
    public GameObject popUpRight;
    public GameObject popUpLeft;
    [Header("Info victory")]
    public GameObject win;
    public Text VKill;
    public Text VHeadshots;
    public Text VCombo;
    public Text VCoins;
    public Button VBtnHome;
    public Button VBtnReload;
    public Button VBtnNext;
    public GameObject starV1;
    public GameObject starV2;
    public GameObject starV3;

    [Header("Info gameover")]
    public GameObject Rivival;
    public GameObject lose;
    public Text LKill;
    public Text LHeadshots;
    public Text LCombo;
    public Text LCoins;
    public Button LBtnHome;
    public Button LBtnReload;
    public Button BtnRivival;
    public Text timeCanRivival;
    public float time = 5;

    [Header("Info Button rate and more")]
    public Button rate;
    public Button store;

    [Header("Info PopUp")]
    public Local local;
    public float sumCoins;
    public Coins coins;

    [Header("All thing btn Skill")]
    public SystemDamage system;

    public Image btnFireCloneRight;
    public Image btnFireCloneLeft;
    public float timeCDP1;
    [SerializeField] private bool activel; // activel effect skill

    public bool activelSecondChange;
    [SerializeField] private float number;
    public bool activelAnim;

    private void Start()
    {
        popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 40);
        popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 40);


        popUp.SetActive(false);
    }
    private void OnEnable()
    {
        activelAnim = false;
        activelSecondChange = false;
        activel = true;
        btnFireCloneRight.fillAmount = 1;
        btnFireCloneLeft.fillAmount = 1;
        ManagerAds.Ins.HideBanner();
    }
    private void OnDisable()
    {
        ManagerAds.Ins.ShowBanner();
    }

    private void Update()
    {
        if (activel == true)
        {
            number += Time.deltaTime * 3f;
            if ((int)number % 2 == 0)
            {
                btnFireCloneRight.color = new Color(255, 0, 0, 1);
                btnFireCloneLeft.color = new Color(255, 0, 0, 1);
            }
            else
            {
                btnFireCloneRight.color = new Color(255, 0, 0, 0);
                btnFireCloneLeft.color = new Color(255, 0, 0, 0);
            }
        }

        if (system && system.timeP1 != 0)
        {
            timeCDP1 = system.timeP1;
        }
        else if (system && system.timeP1 == 0)
        {
            timeCDP1 = 0;
        }

        if (activelSecondChange)
        {
            time -= Time.deltaTime;
            timeCanRivival.text = (int)time + "";

            if (activelSecondChange && time <= 0)
            {
                FindLocalGetSeconChange();

                if (activelSecondChange == false)
                {
                    GameOver2();
                }
            }
        }
        else
        {
            time = 5.5f;
        }

        if (time <= 5 && time > 4)
        {
            timeCanRivival.color = new Color(0, 255, 0, 1);
        }
        else if (time <= 4 && time > 3)
        {
            timeCanRivival.color = new Color(255, 255, 0, 1);
        }
        else if (time <= 3 && time > 2)
        {
            timeCanRivival.color = new Color(255, 93, 0, 1);
        }
        else if (time <= 2 && time > 1)
        {
            timeCanRivival.color = new Color(255, 0, 0, 1);
        }
    }
    public void GameWin()
    {
        popUp.SetActive(true);
        win.SetActive(true);
        lose.SetActive(false);
        Rivival.SetActive(false);
        popUpRight.SetActive(true);
        popUpLeft.SetActive(true);

        if (activelAnim != true)
        {
            popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 40);
            popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 40);
            StartCoroutine(testAnimRight(0.5f));
            //Debug.Log("Yes");
        }

        activelAnim = true;
    }
    public void GameOver()
    {
        if (activelSecondChange == false)
        {
            time = 5.5f;
            activelSecondChange = true;
        }

        popUp.SetActive(true);
        win.SetActive(false);
        lose.SetActive(false);
        Rivival.SetActive(true);
        popUpRight.SetActive(false);
        popUpLeft.SetActive(false);

        activelAnim = false;
    }
    public void GameOver2()
    {
        popUp.SetActive(true);
        win.SetActive(false);
        lose.SetActive(true);
        Rivival.SetActive(false);
        popUpRight.SetActive(true);
        popUpLeft.SetActive(true);

        if (activelAnim != true)
        {
            popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 40);
            popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 40);
            StartCoroutine(testAnimRight(0.5f));
        }

        activelAnim = true;
    }
    public void GameOver3()
    {
        popUp.SetActive(true);
        win.SetActive(false);
        lose.SetActive(true);
        Rivival.SetActive(false);
        popUpRight.SetActive(true);
        popUpLeft.SetActive(true);

        popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(144, 40);
        popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(-144, 40);
    }
    public void hidePopUp()
    {
        popUp.SetActive(false);
    }
    public void addCoins()
    {
        coins.Add((int)sumCoins);
        Debug.Log("sum coins Local: " + sumCoins);
    }

    public void FindLocalGetSeconChange()
    {
        if (FindObjectOfType<Local>())
        {
            FindObjectOfType<Local>().secondchange = true;
        }
        if (FindObjectOfType<LocalForSurvival>())
        {
            FindObjectOfType<LocalForSurvival>().secondchange = true;
        }
        activelSecondChange = false;
    }
    public void activelBtnClone()
    {
        if (timeCDP1 != 0 && activel)
        {
            activel = false;
            var timer = timeCDP1;
            btnFireCloneRight.fillAmount = 0;
            btnFireCloneLeft.fillAmount = 0;

            btnFireCloneRight.color = new Color(255, 0, 0, 1);
            btnFireCloneLeft.color = new Color(255, 0, 0, 1);
            StartCoroutine(test(0, 1, timer));
        }
    }
    public IEnumerator test(float start, float end, float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            btnFireCloneRight.fillAmount = (float)Mathf.Lerp(start, end, progress);
            btnFireCloneLeft.fillAmount = (float)Mathf.Lerp(start, end, progress);
            yield return null;
        }

        btnFireCloneRight.fillAmount = end;
        btnFireCloneLeft.fillAmount = end;
        activel = true;
    }

    public IEnumerator testAnimRight(float duration)
    {
        popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 40);
        popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 40);

        var test1 = popUpRight.GetComponent<RectTransform>();
        var test2 = popUpLeft.GetComponent<RectTransform>();
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            test1.anchoredPosition = new Vector2((float)Mathf.Lerp(200, -144, progress) , 40);
            test2.anchoredPosition = new Vector2((float)Mathf.Lerp(-200, 144, progress), 40);
            yield return null;
        }

        popUpLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(144, 40);
        popUpRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(-144, 40);
    }

    public void ressetAnim()
    {
        activelSecondChange = false;
        BtnRivival.enabled = true;
        activelAnim = false;
        time = 5;
    }
    public void UnEnableBtn()
    {
        BtnRivival.enabled = false;
    }
    public void activelChangeTest()
    {
        activelSecondChange = false;
    }
}
