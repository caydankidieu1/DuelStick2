using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalForSurvival : MonoBehaviour
{
    [Header("-------")]
    public PlayerManager playerManager;
    public Transform player;
    public bool checkEnemy;
    public int sumEnemy;

    [Header("-----------------------------")]
    public SystemDamage system;
    public BattleUI battleUI;

    [Header("value Pop-Up")]
    public float sumKills;
    public float sumHits;
    public float sumHitHeads;
    public float sumCoin;
    public float combo; // so combo cao nhat ma trong wave dat duoc trong 3s
    [SerializeField]
    private float comboClone;
    [SerializeField]
    private float comboTestTime;
    private float test;
    [Header("-------------------------------------- Auto Create Enemy ----------------")]
    public WeaponManager weapons;
    public ControllerSystemSkins skins;
    public GameObject localtionEnemy;
    public GameObject prefabEnemy;
    public int amoutEnemysAccepted = 3;
    public float timeToCheckEnemys = 5f;
    private float cloneTimeCheck;
    [Space]
    public int HpStart = 150;
    public int DamageStart = 10;
    public int HPStartBoss = 500;
    public int DamageStartBoss = 25;
    public int limitOneWave = 5;
    private int sumBossCreate;

    [Header("Quest")]
    public Quest questKillEnemys;
    public Quest questKillBoss;
    public bool createBoss;

    [Header("Second Change")]
    public bool secondchange;

    private void Start()
    {
        sumBossCreate = 0;
        createBoss = false;
        secondchange = false;
        system.secondChange = false;
        system.loser = false;
        system.victory = false;

        playerManager = GetComponentInChildren<PlayerManager>();
        cloneTimeCheck = timeToCheckEnemys;
        test = system.timeCombo;
    }
    private void Update()
    {
        SumCombo();

        SumcoinsReward();
        battleUI.sumCoins = sumCoin;
        setValuePopUp();

        if (playerManager)
        {
            if (playerManager.HP > 0)
            {
                CheckAllEnemy();
            }
        }
       
        if (sumKills >= limitOneWave)
        {
            HpStart += (int)((HpStart * 10) / 100);
            DamageStart += (int)((DamageStart * 5) / 100);

            HPStartBoss += (int)((HPStartBoss * 15) / 100);
            DamageStartBoss += (int)((DamageStartBoss * 10) / 100);

            createBoss = true;
            limitOneWave += 5;
        }

        if (system)
        {
            system.secondChange = secondchange;
        }
    }

    public void SumcoinsReward()
    {
        sumCoin = (Mathf.Clamp((sumBossCreate - 1), 0, 100) * 100) + (sumKills * 50);
    }
    #region InfoCombo
    void setValuePopUp()
    {
        battleUI.VKill.text = sumKills.ToString();
        battleUI.VHeadshots.text = sumHitHeads.ToString();
        battleUI.VCombo.text = combo.ToString();
        battleUI.VCoins.text = sumCoin.ToString();

        battleUI.LKill.text = sumKills.ToString();
        battleUI.LHeadshots.text = sumHitHeads.ToString();
        battleUI.LCombo.text = combo.ToString();
        battleUI.LCoins.text = sumCoin.ToString();
    }
    void sumHit()
    {
        sumHits++;
        comboTestTime = test;

        if (comboTestTime >= 0)
        {
            comboClone++;
        }
    }
    void SumCombo()
    {
        comboTestTime -= Time.deltaTime;

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
    }
    void death()
    {
        sumKills++;
        questKillEnemys.number++;
    }
    void deathBoss()
    {
        questKillBoss.number++;
    }
    #endregion
    public void CheckAllEnemy()
    {
        timeToCheckEnemys -= Time.deltaTime;

        if (timeToCheckEnemys <= 0)
        {
            var test = GetComponentsInChildren<EnemyManager>().Length;
            //Debug.Log("Amount Enemys: " + test);
            if (test < amoutEnemysAccepted)
            {
                AutoCreateEnemy();
            }

            timeToCheckEnemys = cloneTimeCheck;
        }
    }
    public void AutoCreateEnemy()
    {
        GameObject T = Instantiate(prefabEnemy, localtionEnemy.transform.position, Quaternion.identity);

        T.gameObject.SetActive(true);
        T.GetComponent<AI>().force = Random.Range(40, (int)65);
        T.GetComponent<AI>().timeSkill = 5; ;
        T.transform.SetParent(gameObject.transform);

        var infoCotsume = T.GetComponent<CotsumeControllerEnemy>();
        //infoCotsume.id = RandomSkin();
        infoCotsume.id = 01;
        infoCotsume.HP = HpStart;
        infoCotsume.checkChangeColor = true;

        var infoWeapon = T.GetComponent<CreateWeaponE>();
        infoWeapon.idItem = "01";
        //infoWeapon.idItem = RandomWeapon(); // punch = 01
        infoWeapon.damageWeaponBase = DamageStart;
        infoWeapon.damageWeaponSkill = DamageStart;

        /*var test = Random.Range(0, 100);
        if (test >= 90)
        {
            T.gameObject.transform.localScale = new Vector3(2, 2, 2);
            T.GetComponent<CotsumeControllerEnemy>().HP = HpStart * 5;
            T.GetComponent<CreateWeaponE>().idItem = "01";
        }*/
        if (createBoss)
        {
            sumBossCreate++;
            T.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            infoCotsume.id = RandomSkin();
            infoCotsume.HP = HPStartBoss;
            infoCotsume.checkChangeColor = false;

            infoWeapon.idItem = RandomWeapon();
            //infoWeapon.idItem = "01";

            infoWeapon.damageWeaponBase = DamageStartBoss;
            infoWeapon.damageWeaponSkill = DamageStartBoss;
            createBoss = false;
        }

    }
    int RandomSkin()
    {
        List<Cotsume> testAllSkinBuy = new List<Cotsume>();
        for (int i = 0; i < skins.cotsume.Length; i++)
        {
            testAllSkinBuy.Add(skins.cotsume[i]);
        }

        Cotsume skinEnemys = testAllSkinBuy[Random.Range(0, (int)testAllSkinBuy.Count)];
        return skinEnemys.id;
    }
    string RandomWeapon()
    {
        List<Weapon> testAllWeaponBuy = new List<Weapon>();
        for (int i = 0; i < weapons.weaponItem.Length; i++)
        {
             testAllWeaponBuy.Add(weapons.weaponItem[i]);
        }

        Weapon weaponEnemys = testAllWeaponBuy[Random.Range(0, (int)testAllWeaponBuy.Count)];
        return weaponEnemys.id;
    }
    public void FreezeEnemy()
    {
        for (int i = 0; i < GetComponentsInChildren<EnemyManager>().Length; i++)
        {
            GetComponentsInChildren<EnemyManager>()[i].rb2d.bodyType = RigidbodyType2D.Static;
            GetComponentsInChildren<EnemyManager>()[i].GetComponent<AI>().enabled = false;
        }

        StartCoroutine(ActivelMove());
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
}
