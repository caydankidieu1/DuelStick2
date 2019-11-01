using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    [Header("----------------------------------------")]
    private StarController StarC;

    [Header("Value")]
    public int IsStar = 0;
    public int IsStarClone = 0;
    public bool checkClear;

    [Header("Sprite for Star")]
    public Sprite starGold;
    public Sprite starSilve;

    [Header("Sprite for Phase")]
    public Sprite Phase1;
    public Sprite Phase2;
    public Sprite Phase3;
    public Sprite Phase4;

    [Header("location Star")]
    public Image star1;
    public Image star2;
    public Image star3;

    [Header("Text And Wave")]
    public GameObject text;

    [SerializeField]
    private Image Me;
    [SerializeField]
    public bool checkNext = false;
    [SerializeField]
    private CheckPhase checkPhase;
    [SerializeField]
    private bool checkUnlock;
    

    [Header("Want to Create Player and Enemy")]
    public GameObject wave;
    public GameObject Played;
    private StarController starController;

    [Header("New Game")]
    public Sprite imageBackGround;
    public Image LocalImage;
    public Button BattleInfo;

    [Header("END LESS")]
    public bool endLess;

    private void Start()
    {
       /* star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
        star1.GetComponent<Image>().sprite = starSilve;
        star2.GetComponent<Image>().sprite = starSilve;
        star3.GetComponent<Image>().sprite = starSilve;*/
        text.SetActive(true);

        Me = GetComponent<Image>();
        //Me.sprite = Phase3;

       // checkPhase = GetComponentInParent<CheckPhase>();

        //starController = GetComponentInParent<StarController>();
    }
/*    void FixedUpdate()
    {
        checkUnlock = checkPhase.activeUnlock;
        if (IsStar >= 1)
        {
            checkNext = false;
        }
        //show();

        if (IsStar >= 1)
        {
            checkClear = true;
        }
        else
        {
            checkClear = false;
        }
    }*/

    void show()
    {
        if (checkUnlock)
        {
            gameObject.GetComponent<Button>().enabled = true;
            text.SetActive(true);
            if (IsStar == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
                if (checkNext)
                {
                    Me.sprite = Phase2;
                }
                else
                {
                    Me.sprite = Phase3;
                }
            }
            else if (IsStar <= 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
                star1.GetComponent<Image>().sprite = starGold;
                star2.GetComponent<Image>().sprite = starSilve;
                star3.GetComponent<Image>().sprite = starSilve;

                Me.sprite = Phase1;
            }
            else if (IsStar == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
                star1.GetComponent<Image>().sprite = starGold;
                star2.GetComponent<Image>().sprite = starGold;
                star3.GetComponent<Image>().sprite = starSilve;
                Me.sprite = Phase1;
            }
            else if (IsStar >= 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
                star1.GetComponent<Image>().sprite = starGold;
                star2.GetComponent<Image>().sprite = starGold;
                star3.GetComponent<Image>().sprite = starGold;
                Me.sprite = Phase1;
            }
        }
        else if (checkUnlock == false)
        {
            text.SetActive(false);
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
            Me.sprite = Phase4;
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
    public void ActionWave()
    {
        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);
        
        GameObject P;
        P = Instantiate(Played, map.GetComponent<Local>().played.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<Local>().played.transform.position;

        var value = map.GetComponent<Local>();
        value.ENDLESS = endLess;
    }
    public void ReloadWave()//test
    {
        Destroy(FindObjectOfType<Local>().gameObject);

        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(Played, map.GetComponent<Local>().played.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<Local>().played.transform.position;

        var value = map.GetComponent<Local>();
        value.ENDLESS = endLess;
    }
    public void RivivalPlayer()//test
    {
        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = FindObjectOfType<Local>().gameObject;
        map.SetActive(true);
        map.GetComponent<Local>().secondchange = true;

        GameObject P;
        P = Instantiate(Played, map.GetComponent<Local>().played.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<Local>().played.transform.position;
        P.GetComponent<PlayerManager>().Block();

        map.GetComponent<Local>().playerManager = P.GetComponent<PlayerManager>();
        var test = map.GetComponentsInChildren<AI>();
        if (test.Length > 0)
        {
            for (int i = 0; i < test.Length; i++)
            {
                test[i].target = P.transform;
            }
        }

        var value = map.GetComponent<Local>();
        value.ENDLESS = endLess;
    }
    public void RivivalPlayerClone()//test
    {
        GameObject map;
        map = FindObjectOfType<Local>().gameObject;
        map.SetActive(true);
        map.GetComponent<Local>().secondchange = true;
    }
    public void Save()
    {
        for (int i = 0; i < StarC.Phase.Length; i++)
        {
            if (StarC.Phase[i].phase == this)
            {
                StarC.Phase[i].Star = IsStar;
            }
        }

        starController.SaveStar();
    }
    public void Save2()
    {
        if (IsStarClone > IsStar)
        {
            IsStar = IsStarClone;
            starController.SaveStar();
        }
    }


    public void activelNormal()
    {
        //Me.sprite = Phase3;
        Me.sprite = imageBackGround;
    }
    public void activelChoice()
    {
        //Me.sprite = Phase2;
        Me.sprite = imageBackGround;
    }
    public void showBackGround()
    {
        //LocalImage.sprite = imageBackGround;
        checkPhase.resetColor();
        BattleInfo.onClick.RemoveAllListeners();
        BattleInfo.onClick.AddListener(ActionWave);

        var info = BattleInfo.GetComponent<AudioUI>();
        BattleInfo.onClick.AddListener(info.onClick);
        activelChoice();
    }
}
