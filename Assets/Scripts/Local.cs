using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local : MonoBehaviour
{
    public Phase Phase;
    [Header("-------")]
    public Transform played;
    public bool checkEnemy;
    public int sumEnemy;
    public SystemDamage system;
    [SerializeField]
    private EnemyManager[] enemyManager;
    public PlayerManager playerManager;
    public BattleUI battleUI;

    [Header("value Pop-Up")]
    public float sumKills;
    public float sumHits;
    public float sumHitHeads;
    public float sumCoin;
    public int coinReward;
    public float combo; // so combo cao nhat ma trong wave dat duoc trong 3s
    private float comboClone;
    private float comboTestTime;

    [Header("value Star")]
    public int sumStar;
    public int Star1 = 0;
    public int Star2 = 0;
    public int Star3 = 0;

    [Header("Quest")]
    public Quest quest;
    public Quest questHead;

    [Header("---------------------------------------------------------------")]
    public int numberEnemysMapHave = 5;
    public WeaponManager weaponManager;
    public ControllerSystemSkins controllerSkin;
    public Package[] packages;
    [SerializeField] private Package randomInPackages;
    [Space]
    public Transform[] LocalEnemys;
    public GameObject prefabEnemys;
    [Space]
    public int AmoutEnemysAccepted = 2;
    public float TimeToCheckEnemys = 5f;
    private float cloneTimeCheck;
    [Header("--------------------------- ID Skins Weapon --------------------")]
    public int HP;
    public int damagebase;
    public int damageskill;
    public int[] skinsE;
    public string[] weaponsE;

    [Header("second Change")]
    public bool secondchange;

    [Header("END LESS")]
    public bool ENDLESS;

    private int testvalue;
    private void Start()
    {
        battleUI.hidePopUp();
        secondchange = false;
        system.secondChange = false;
        system.trueDeath = false;
        system.loser = false;
        system.victory = false;

        enemyManager = GetComponentsInChildren<EnemyManager>();
        playerManager = GetComponentInChildren<PlayerManager>();
        comboTestTime = system.timeCombo;
        cloneTimeCheck = TimeToCheckEnemys;

        if (packages.Length != 0)
        {
            randomInPackages = packages[Random.Range(0, (int)packages.Length)];
        }

        AmoutEnemysAccepted = 1;
    }
    private void Update()
    {
        if (playerManager)
        {
            if (playerManager.HP > 0)
            {
                CheckAllEnemy();
                if (ENDLESS)
                {
                    numberEnemysMapHave = 999;
                    Mathf.Clamp(sumKills, 0, 998);
                }
                else
                {
                    numberEnemysMapHave = 1;
                }
            }
            else
            {
                numberEnemysMapHave = 999;
            }
        }
        else
        {
            numberEnemysMapHave = 999;
        }

        checEnemys();
        SumCombo();
        if (checkEnemy)
        {
            testvalue = 1;
        }
        else
        {
            testvalue = 0;
        }

        sumCoin = ((sumKills * 2) + (combo) + (sumHitHeads * 5) + (coinReward)) * testvalue;
        battleUI.sumCoins = sumCoin;


        setValuePopUp();

        //FreezeEnemy();
    }
    void conditionStar()
    {
        if (checkEnemy)
        {
            Star1 = 1;
            /* 
            if (combo >= 30)
            {
                Star2 = 1;
            }

            if (sumHitHeads >= 5)
            {
                Star3 = 1;
            }*/
           // CheckHpPlayer();
        }
    }
    void CheckHpPlayer()
    {
        if (sumKills >= numberEnemysMapHave)
        {
            float HP50 = (playerManager.maxHP * 50f) / 100;
            float HP70 = (playerManager.maxHP * 70f) / 100;

            if (playerManager.HP >= HP50)
            {
                Star2 = 1;
            }

            if (playerManager.HP >= HP70)
            {
                Star3 = 1;
            }
        }
    }
    void setValuePopUp()
    {
        battleUI.VKill.text = sumKills.ToString();
        battleUI.VHeadshots.text = sumHitHeads.ToString();
        battleUI.VCombo.text = combo.ToString();
        battleUI.VCoins.text = sumCoin.ToString();
        if (sumStar == 1)
        {
            battleUI.starV1.SetActive(true);
            battleUI.starV2.SetActive(false);
            battleUI.starV3.SetActive(false);
        }
        else if (sumStar == 2)
        {
            battleUI.starV1.SetActive(false);
            battleUI.starV2.SetActive(true);
            battleUI.starV3.SetActive(false);
        }
        else if (sumStar == 3)
        {
            battleUI.starV1.SetActive(false);
            battleUI.starV2.SetActive(false);
            battleUI.starV3.SetActive(true);
        }
        else
        {
            battleUI.starV1.SetActive(false);
            battleUI.starV2.SetActive(false);
            battleUI.starV3.SetActive(false);
        }

        battleUI.LKill.text = sumKills.ToString();
        battleUI.LHeadshots.text = sumHitHeads.ToString();
        battleUI.LCombo.text = combo.ToString();
        if (checkEnemy)
        {
            battleUI.LCoins.text = sumCoin.ToString();
        }
        else
        {
            battleUI.LCoins.text = "0";
        }
       
    }
    void checEnemys()
    {
        if (sumKills >= numberEnemysMapHave) // fix this
        {
            //checkEnemy = true;
            if (system && playerManager != null)
            {
                StartCoroutine(waitToSend());
            }
        }
        else
        {
            if (system)
            {
                StartCoroutine(waitToSend2());
            }
        }

        if (system)
        {
            system.trueDeathAllEnemy = checkEnemy;
            system.secondChange = secondchange;
        }

        if (system && checkEnemy)
        {
            system = null;
        }
    }
    IEnumerator waitToSend()
    {
        yield return new WaitForSeconds(system.timeDelayWin);
        checkEnemy = true; // xac nhan toan bo enemy da chet
        yield return 0;
    }
    IEnumerator waitToSend2()
    {
        yield return new WaitForSeconds(system.timeDelayWin);
        checkEnemy = false; // xac nhan toan bo enemy da chet
        yield return 0;
    }
    void sumHit()
    {
        sumHits++;
        if (system)
        {
            comboTestTime = system.timeCombo;
        }
        

        if (comboTestTime >= 0)
        {
            comboClone++;
        }
    }
    void SumCombo()
    {
        comboTestTime -= Time.deltaTime;
        if (sumKills >= enemyManager.Length)
        {
            comboTestTime = -1;
        }

        if (comboTestTime <= 0f)
        {
            if (comboClone > combo)
            {
                combo = comboClone;
                comboClone = 0;
            }
            else
            {
                comboClone = 0;
            }
        }
    }
    void sumHitHead()
    {
        sumHitHeads++;
        questHead.number++;
    }
    void death()
    {
        sumKills++;
        quest.number++;
    }
    public void FreezeEnemy()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
       // {
            for (int i = 0; i < GetComponentsInChildren<EnemyManager>().Length; i++)
            {
                GetComponentsInChildren<EnemyManager>()[i].rb2d.bodyType = RigidbodyType2D.Static;
                GetComponentsInChildren<EnemyManager>()[i].GetComponent<AI>().enabled = false;
            }

            StartCoroutine(ActivelMove());
       // }
    }
    IEnumerator ActivelMove()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < GetComponentsInChildren<EnemyManager>().Length; i++)
        {
            GetComponentsInChildren<EnemyManager>()[i].rb2d.bodyType = RigidbodyType2D.Dynamic;
            GetComponentsInChildren<EnemyManager>()[i].GetComponent<AI>().enabled = true;
        }
        yield return 0;
    }
    #region Create Enemys
    public void CheckAllEnemy()
    {
        TimeToCheckEnemys -= Time.deltaTime;

        if (TimeToCheckEnemys <= 0)
        {
            var test = GetComponentsInChildren<EnemyManager>().Length;
            //Debug.Log("Amount Enemys: " + test);
            if (sumKills + test < numberEnemysMapHave)
            {
                if (test < AmoutEnemysAccepted)
                {
                    AutoCreateEnemy();
                }
            }
        
            TimeToCheckEnemys = cloneTimeCheck;
        }
    }
    public void AutoCreateEnemy()
    {
        GameObject T = Instantiate(prefabEnemys, LocalEnemys[Random.Range(0, (int)LocalEnemys.Length)].transform.position, Quaternion.identity);
        T.gameObject.SetActive(true);
        T.GetComponent<AI>().force = Random.Range(40, (int)65);
        T.GetComponent<AI>().timeSkill = 5;
        T.transform.SetParent(gameObject.transform);
        var infoCotsume = T.GetComponent<CotsumeControllerEnemy>();
        //.id = RandomSkin();
        infoCotsume.id = RandomSkin();
        infoCotsume.HP = (RandomHP() * 70 / 100);
        T.GetComponent<CreateWeaponE>().idItem = RandomWeapon();

        if (damagebase != 0)
        {
            T.GetComponent<CreateWeaponE>().damageWeaponBase = damagebase;
        }
        if (damageskill != 0)
        {
            T.GetComponent<CreateWeaponE>().damageWeaponSkill = damageskill;
        }
    }
    int RandomSkin()
    {
        List<Cotsume> testAllSkinBuy = new List<Cotsume>();

        if (packages.Length != 0)
        {
            for (int i = 0; i < randomInPackages.cotsume.Length; i++)
            {
                if (randomInPackages.cotsume[i] != null)
                {
                    testAllSkinBuy.Add(randomInPackages.cotsume[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < controllerSkin.cotsume.Length; i++)
            {
                for (int j = 0; j < skinsE.Length; j++)
                {
                    if (controllerSkin.cotsume[i].id == skinsE[j])
                    {
                        testAllSkinBuy.Add(controllerSkin.cotsume[i]);
                    }
                }
            }
        }

        Cotsume skinEnemys = testAllSkinBuy[Random.Range(0, (int)testAllSkinBuy.Count)];
        return skinEnemys.id;
    }
    int RandomHP()
    {
        List<Cotsume> testAllSkinBuy = new List<Cotsume>();

        if (packages.Length != 0)
        {
            for (int i = 0; i < randomInPackages.cotsume.Length; i++)
            {
                if (randomInPackages.cotsume[i] != null)
                {
                    testAllSkinBuy.Add(randomInPackages.cotsume[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < controllerSkin.cotsume.Length; i++)
            {
                for (int j = 0; j < skinsE.Length; j++)
                {
                    if (controllerSkin.cotsume[i].id == skinsE[j])
                    {
                        testAllSkinBuy.Add(controllerSkin.cotsume[i]);
                    }
                }
            }
        }

        Cotsume skinEnemys = testAllSkinBuy[Random.Range(0, (int)testAllSkinBuy.Count)];
        return skinEnemys.HP;
    }
    string RandomWeapon()
    {
        List<Weapon> testAllWeaponBuy = new List<Weapon>();
        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            for (int j = 0; j < weaponsE.Length; j++)
            {
                if (weaponManager.weaponItem[i].id == weaponsE[j])
                {
                    testAllWeaponBuy.Add(weaponManager.weaponItem[i]);
                }
            }
        }

        Weapon weaponEnemys = testAllWeaponBuy[Random.Range(0, (int)testAllWeaponBuy.Count)];
       
        for (int i = 0; i < testAllWeaponBuy.Count; i++)
        {
            //Debug.Log(testAllWeaponBuy[i]);
        }
        //Debug.Log(weaponEnemys.id);
        return weaponEnemys.id;
    }
    #endregion
}
