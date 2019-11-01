using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LuckySpinController : MonoBehaviour
{
    public WeaponManager weaponManager;
    public ControllerSystemSkins skinManager;
    public PowerUI power;
    public Coins coins;
    public LoadData loadData;
    public QuestControllerUI questUI;
    public Quest quest;
    public WeaponStoreUI weaponStore;

    [Header("----------------------------------------------------------")]
    public bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public Button TurnButtonVideo;
    public GameObject Circle; 			// Rotatable Object with rewards
    public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    public Text CurrentCoinsText; 		// Pop-up text with wasted or rewarded coins amount
    public int TurnCost = 300;			// How much coins user waste when turn whe wheel
    public int CurrentCoinsAmount = 1000;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    public int PreviousCoinsAmount;		// For wasted coins animation

    public Image Activel;
    public Image imageInActivel;
    public Text textInActivel;

    private int TestAngle;

    [Header("-------------------------------------")]
    public GameObject backAdventure;
    public GameObject PannelHide;
    public GameObject ButtonHide;
    public GameObject ACReward;
    public Text nameReward;
    public Image imgRewards;
    public Image imgRewards2;
    public Image imgRewards3;
    public GameObject btnVideo;
    public GameObject btnClose;
    private Vector3 localBtnVideo;
    private Vector3 localBtnClose;

    public Text TurnVideo;
    public int countViewVideo;
    public int countTurnVideo;
    public int videoWeapon;
    
    [Header("------------------------ Clone Coins And Power ----------------------")]
    public int CoinsClone;
    public List<Power> powerClone = new List<Power>();

    [Header("------------------------ Controll VFX -------------------------------")]
    public ParticleSystem VFX_BtnTurn;

    [Header("audio")]
    private Audio audioManager;
    public string nameGachapon;
    public string nameRewarl;

    private void Awake()
    {
        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();
        Activel.gameObject.SetActive(false);
        ACReward.SetActive(false);
        imgRewards.SetNativeSize();
        imgRewards2.gameObject.SetActive(false);
        imgRewards3.gameObject.SetActive(false);

        if (PlayerPrefs.HasKey("numberVideo"))
        {
            countViewVideo = PlayerPrefs.GetInt("numberVideo");
        }
        else
        {
            PlayerPrefs.SetInt("numberVideo", 0);
        }

        if (PlayerPrefs.HasKey("numberVideoTurn"))
        {
            countTurnVideo = PlayerPrefs.GetInt("numberVideoTurn");
        }
        else
        {
            PlayerPrefs.SetInt("numberVideoTurn", 0);
        }

        if (PlayerPrefs.HasKey("CountVideo"))
        {
            videoWeapon = PlayerPrefs.GetInt("CountVideo");
        }
        else
        {
            PlayerPrefs.SetInt("CountVideo", 0);
        }
        PannelHide.SetActive(false);
    }
    private void Start()
    {
        localBtnVideo = btnVideo.GetComponent<RectTransform>().localPosition;
        localBtnClose = btnClose.GetComponent<RectTransform>().localPosition;

        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.Log("No found any Audio");
        }
    }

    public void TurnWheelByTime()
    {
        TurnButton.gameObject.SetActive(false);
        ButtonHide.SetActive(true);
        //VFX_BtnTurn.gameObject.SetActive(false);
    }
    public void TurnWheelByTime2()
    {
        TurnButton.gameObject.SetActive(true);
        ButtonHide.SetActive(false);
        //VFX_BtnTurn.gameObject.SetActive(true);
    }
    public void TurnWheelByVideo()
    {
        ManagerAds.Ins.ShowRewardedVideo(isSuccess =>
        {
            if (isSuccess)
            {
                TurnWheel();
                countTurnVideo++;
                videoWeapon++; //test
                weaponStore.CheckVideoRewardWeapon();
                PlayerPrefs.SetInt("numberVideoTurn", countTurnVideo);
                PlayerPrefs.SetInt("CountVideo", videoWeapon);
            }
        });
    }
    public void TurnWheel()
    {
        // Player has enough money to turn the wheel
        if (CurrentCoinsAmount >= TurnCost)
        {
            if (quest.number < 5)
            {
                quest.number++;
                questUI.SaveQuest();
            }
          
            _currentLerpRotationTime = 0f;

            // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
            _sectorsAngles = new float[] { 45, 90, 135, 180, 225, 270, 315, 360};

            if (nameGachapon != null)
            {
                audioManager.PlaySound(nameGachapon);
            }
            CoinsClone = 0;
            powerClone.Clear();
            imgRewards2.gameObject.SetActive(false);
            imgRewards3.gameObject.SetActive(false);
            ACReward.SetActive(false);
            calulateLoot();
            Debug.Log(TestAngle);

            int fullCircles = 5;
            //float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
            //Debug.Log(randomFinalAngle);
            float randomFinalAngle = TestAngle;
            Debug.Log(randomFinalAngle);

            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;

            PreviousCoinsAmount = CurrentCoinsAmount;

            // Decrease money for the turn
            CurrentCoinsAmount -= TurnCost;

            // Show wasted coins
            if (CoinsDeltaText)
            {
                CoinsDeltaText.text = "-" + TurnCost;
                CoinsDeltaText.gameObject.SetActive(true);
            }
          
            // Animate coins
            StartCoroutine(HideCoinsDelta());
            StartCoroutine(UpdateCoinsAmount());

            Activel.gameObject.SetActive(false);
        }
    }
    private void GiveAwardByAngle()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle)
        {
            case 0:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;
                Debug.Log("here");
                for (int i = 0; i < LootTable.Count; i++)
                {
                    if (LootTable[i].angle == 360)
                    {
                        RewardCoins(LootTable[i].coins);
                    }
                }
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -315:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;
                for (int i = 0; i < LootTable.Count; i++)
                {
                    if (LootTable[i].angle == -_startAngle)
                    {
                        RewardCoins(LootTable[i].coins);
                    }
                }
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -270: // skins

                btnVideo.GetComponent<RectTransform>().localPosition = new Vector3(0, -191, 0);
                btnClose.GetComponent<RectTransform>().localPosition = new Vector3(0, -191, 0);

                calulateLootSkins();
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -225:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;

                for (int i = 0; i < LootTable.Count; i++)
                {
                    if (LootTable[i].angle == -_startAngle)
                    {
                        RewardCoins(LootTable[i].coins);
                    }
                }
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -180: // weapon
                btnVideo.GetComponent<RectTransform>().localPosition = new Vector3(0, -191, 0);
                btnClose.GetComponent<RectTransform>().localPosition = new Vector3(0, -191, 0);

                calulateLootWeapon();
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -135:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;

                for (int i = 0; i < LootTable.Count; i++)
                {
                    if (LootTable[i].angle == -_startAngle)
                    {
                        RewardCoins(LootTable[i].coins);
                    }
                }
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -90:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;

                calulateLootPower(3);
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;

            case -45:
                btnVideo.GetComponent<RectTransform>().localPosition = localBtnVideo;
                btnClose.GetComponent<RectTransform>().localPosition = localBtnClose;

                calulateLootPower(1);
                Activel.gameObject.SetActive(true);
                StartCoroutine(RewardAll());
                break;
        }
    }
    void Update()
    {
        countViewVideo = PlayerPrefs.GetInt("numberVideo");
        countTurnVideo = PlayerPrefs.GetInt("numberVideoTurn");
        TurnVideo.text = countTurnVideo + " / 3";

        if (countViewVideo < 5)
        {
            btnVideo.SetActive(true);
        }
        if (countTurnVideo >= 3)
        {
            TurnButtonVideo.interactable = false;
            TurnButtonVideo.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }

        // Make turn button non interactable if user has not enough money for the turn
        if (_isStarted || CurrentCoinsAmount < TurnCost)
        {
            PannelHide.SetActive(true);
            TurnButton.interactable = false;
            TurnButtonVideo.interactable = false;
            //TurnButton.GetComponent<Image>().color = new Color(28, 185, 46, 1f);
            TurnButtonVideo.interactable = false;
            TurnButtonVideo.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            if (countTurnVideo < 3)
            {
                TurnButtonVideo.interactable = true;
            }
            // TurnButton.GetComponent<Image>().color = new Color(28, 185, 46, 1f);
            if (countTurnVideo < 3)
            {
                TurnButtonVideo.interactable = true;
                TurnButtonVideo.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            }
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 4f;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;
            Debug.Log(_startAngle);

            GiveAwardByAngle();
            StartCoroutine(HideCoinsDelta());
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void HideReward()
    {
        if (ACReward)
        {
            ACReward.gameObject.SetActive(false);
        }
    }
    private void RewardCoins(int awardCoins)
    {
        CurrentCoinsAmount += awardCoins;
        if (CoinsDeltaText)
        {
            CoinsDeltaText.text = "+" + awardCoins;
            CoinsDeltaText.gameObject.SetActive(true);
        }
       
        if (awardCoins > 0)
        {
            nameReward.text = "+ " + awardCoins;
            coins.Add(awardCoins);
        }
        
        imgRewards.sprite = LootTable[0].sprite;
        imgRewards.type = Image.Type.Simple;
        imgRewards.SetNativeSize();
        imgRewards.type = Image.Type.Sliced;

        StartCoroutine(UpdateCoinsAmount());
    }
    IEnumerator RewardAll()
    {
        if (nameGachapon != null)
        {
            audioManager.StopSound(nameGachapon);
        }
        yield return new WaitForSeconds(1f);

        if (nameRewarl != null)
        {
            audioManager.PlaySound(nameRewarl);
        }

        ACReward.SetActive(true);
        PannelHide.SetActive(false);
        yield return 0;
        //StartCoroutine(HideCoinsDelta());
    }
    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds(1f);
        if (CoinsDeltaText)
        {
            //CoinsDeltaText.gameObject.SetActive(false);
        }
        if (ACReward)
        {
           // ACReward.gameObject.SetActive(false);
        }
    }
    private IEnumerator UpdateCoinsAmount()
    {
        // Animation for increasing and decreasing of coins amount
        const float seconds = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp(PreviousCoinsAmount, CurrentCoinsAmount, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();
    }
    public void ClickVideoReward()
    {
        ManagerAds.Ins.ShowRewardedVideo(isSuccess =>
        {
            if (isSuccess)
            {
                coins.Add(CoinsClone);
                for (int i = 0; i < powerClone.Count; i++)
                {
                    powerClone[i].amount++;
                    if (powerClone[i].amount >= powerClone[i].maxAmount)
                    {
                        powerClone[i].amount = powerClone[i].maxAmount;
                    }
                }
                countViewVideo++;
                if (countViewVideo >= 5)
                {
                    btnVideo.SetActive(false);
                }
                PlayerPrefs.SetInt("numberVideo", countViewVideo);
                HideReward();
            }
        });
    }
    [System.Serializable]
    public class DropCurrency
    {
        public int angle;
        public string name;
        public int coins;
        public Sprite sprite;
        public int DropRarity;
    }

    public List<DropCurrency> LootTable = new List<DropCurrency>();
    public int dropChange = 1;

    void calulateLoot()
    {
        int _calc_dropChange = UnityEngine.Random.Range(0, 101);

        if (_calc_dropChange > dropChange)
        {
            Debug.Log("No loot For you");
            return;
        }

        if (_calc_dropChange <= dropChange)
        {
            int itemWeight = 0;

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].DropRarity;
            }

            int Randomvalue = UnityEngine.Random.Range(0, itemWeight);

            for (int j = 0; j < LootTable.Count; j++)
            {
                if (Randomvalue <= LootTable[j].DropRarity)
                {
                    Debug.Log(LootTable[j].name);
                    TestAngle = LootTable[j].angle;
                    imageInActivel.sprite = LootTable[j].sprite;
                    imageInActivel.SetNativeSize();
                    if (LootTable[j].coins > 0)
                    {
                        textInActivel.gameObject.SetActive(true);
                        textInActivel.text = LootTable[j].coins.ToString();
                        CoinsClone = LootTable[j].coins;
                    }
                    else
                    {
                        textInActivel.gameObject.SetActive(false);
                    }
                    return;
                }

                Randomvalue -= LootTable[j].DropRarity;
            }
        }
    }
    void calulateLootSkins()
    {
        int _calc_dropChange = UnityEngine.Random.Range(0, 101);

        if (_calc_dropChange > dropChange)
        {
            Debug.Log("No loot For you");
            return;
        }

        if (_calc_dropChange <= dropChange)
        {
            float itemWeight = 0;

            for (int i = 0; i < skinManager.cotsume.Length; i++)
            {
                itemWeight += skinManager.cotsume[i].rateLuckySpin;
            }

            float Randomvalue = UnityEngine.Random.Range(0, itemWeight);

            for (int j = 0; j < skinManager.cotsume.Length; j++)
            {
                if (Randomvalue <= skinManager.cotsume[j].rateLuckySpin)
                {
                    Debug.Log(skinManager.cotsume[j].name);

                    nameReward.text = skinManager.cotsume[j].name;
                    imgRewards.sprite = skinManager.cotsume[j].icon;
                    imgRewards.type = Image.Type.Simple;
                    imgRewards.SetNativeSize();
                    imgRewards.type = Image.Type.Sliced;

                    skinManager.cotsume[j].checkBuy = true;
                    loadData.SaveAndLoadSkins();
                    return;
                }

                Randomvalue -= skinManager.cotsume[j].rateLuckySpin;
            }
        }
    }
    void calulateLootWeapon()
    {
        int _calc_dropChange = UnityEngine.Random.Range(0, 101);

        if (_calc_dropChange > dropChange)
        {
            Debug.Log("No loot For you");
            return;
        }

        if (_calc_dropChange <= dropChange)
        {
            float itemWeight = 0;

            for (int i = 0; i < weaponManager.weaponItem.Length; i++)
            {
                itemWeight += weaponManager.weaponItem[i].RateLucky;
            }

            float Randomvalue = UnityEngine.Random.Range(0, itemWeight);

            for (int j = 0; j < weaponManager.weaponItem.Length; j++)
            {
                if (Randomvalue <= weaponManager.weaponItem[j].RateLucky)
                {
                    Debug.Log(weaponManager.weaponItem[j].name);

                    nameReward.text =  weaponManager.weaponItem[j].name;
                    imgRewards.sprite = weaponManager.weaponItem[j].SpriteStore;
                    imgRewards.type = Image.Type.Simple;
                    imgRewards.SetNativeSize();
                    imgRewards.type = Image.Type.Sliced;

                    weaponManager.weaponItem[j].checkBuy = true;
                    loadData.SaveAndLoadWeapon();
                    return;
                }

                Randomvalue -= weaponManager.weaponItem[j].RateLucky;
            }
        }
    }
    void calulateLootPower(int number)
    {
        List<Sprite> test = new List<Sprite>();

        for (int i = 0; i < number; i++)
        {
            int local = UnityEngine.Random.Range(0, (int)power.power.Length);
            power.power[local].amount++;
            /*if (power.power[local].amount >= power.power[local].maxAmount)
            {
                power.power[local].amount = power.power[local].maxAmount;
            }*/
            loadData.SaveAndLoadPower();
            if (number > 2)
            {
                nameReward.text = "You got them reward !";
            }
            else if (number == 1)
            {
                nameReward.text = power.power[local].name;
            }

            test.Add(power.power[local].iconPower);
            powerClone.Add(power.power[local]);
        }

        if (test.Count >= 2)
        {
            imgRewards2.gameObject.SetActive(true);
            imgRewards3.gameObject.SetActive(true);
            imgRewards.sprite = test[0];
            imgRewards2.sprite = test[1];
            imgRewards3.sprite = test[2];

            imgRewards.type = Image.Type.Simple;
            imgRewards.SetNativeSize();
            imgRewards.type = Image.Type.Sliced;

            imgRewards2.type = Image.Type.Simple;
            imgRewards2.SetNativeSize();
            imgRewards2.type = Image.Type.Sliced;

            imgRewards3.type = Image.Type.Simple;
            imgRewards3.SetNativeSize();
            imgRewards3.type = Image.Type.Sliced;
        }
        else if (test.Count == 1)
        {
            imgRewards.sprite = test[0];
            imgRewards.type = Image.Type.Simple;
            imgRewards.SetNativeSize();
            imgRewards.type = Image.Type.Sliced;
        }
       
    }

    public void HideBackAdventure()
    {
        backAdventure.SetActive(false);
    }
    public void ActivelBackAdventure()
    {
        backAdventure.SetActive(true);
    }
}
