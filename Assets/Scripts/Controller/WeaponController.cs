using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Values")]
    public float forceKB;
    public float DamageWeapon; // damage base
    public float DamageSkill; // damage skill
    public bool checkPlayer;
    public bool checkUse;
    public string id;
    private int numberFireAccept;

    [Header("Instantiate")]
    public Weapon weapon; // test
    public GameObject skill;
    public Transform FirePoint;
    public bool skillOther;
    //public GameObject VFX_DropWeapon;

    [Header("test")]
    public GameObject otherEffect;
    public float timeActivelEffect = 0.5f;
    public GameObject impactEffect;
    public Laze laze;
    [Space]
    private Vector3 scaleNormal;
    public bool activelScale;
    public float scaleNumber = 3;
    public float timeScale = 1f;
    [Space]
    public bool checkMagnum;

    [Header("Info weapon 2")]
    public CloneWeaponController rightHands;
    public GameObject FlameWeapon;
    public float timeToHide;

    public GameObject weapon2;

    [Header("Component")]
    public Collider2D[] allCol;
    private Transform[] trans;
    private AnimationController animP; // dieu khien cac gia tri cua khop cam vu khi cua player
    private AnimationControllerEnemy animE; // dieu khien cac gia tri cua khop cam vu khi cua enemy
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private Rigidbody2D rigi;
    //public TimeController timeC;
    [SerializeField]private int test = 0;
    private float timeToFire = 0;
    private bool acceptToFire;
    [Header("------------------------- enable weapon ---------------------")]
    private float TIME_GROUND = 1;
    public bool checkGround;
    public bool checkInTarget;
    public bool enableWeapon;
    [Space]
    private Audio audioManager;
    public string audioName;
    public string audioColl;


    [Header("CounDown Weapon send to System")]
    public bool checkP1;
    public bool checkP2;

    [Header("New")]
    public AI ai;

    private void Start()
    {
        if (FlameWeapon)
        {
            FlameWeapon.SetActive(false);
        }
        
        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.LogError("No found Audio Manager");
        }

        ai = GetComponentInParent<AI>();

        allCol = GetComponents<Collider2D>();
        rigi = GetComponent<Rigidbody2D>();
        trans = GetComponentsInParent<Transform>();
        animP = GetComponentInParent<AnimationController>();
        animE = GetComponentInParent<AnimationControllerEnemy>();
        playerManager = GetComponentInParent<PlayerManager>();
        enemyManager = GetComponentInParent<EnemyManager>();
        id = weapon.id; // test
        DamageWeapon = weapon.damage;
        DamageSkill = weapon.skillDamage;
        forceKB = weapon.forceKnockback;
        enableWeapon = false;
        scaleNormal = gameObject.transform.localScale;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        numberFireAccept = weapon.timesToCD;

        if (playerManager != null)
        {
            checkP1 = true;
            playerManager.timeCDWeapon = weapon.countDownTime;
        }
        else
        {
            checkP1 = false;
        }

        if (enemyManager != null && enemyManager.P2 == true)
        {
            checkP2 = true;
            enemyManager.timeCDWeapon = weapon.countDownTime;
        }
        else
        {
            checkP2 = false;
        }

       /* if (gameObject.tag == "unweapon" && VFXOnGround.activeSelf)
        {
            for (int i = 0; i < allCol.Length; i++)
            {
                allCol[i].isTrigger = true;
            }
        }*/
    }
    private void Update()
    {
        checkWeaponTarget();
        sendtrueDamage();
        if (checkUse)
        {
            Destroy(rigi);
        }

        timeToFire -= Time.deltaTime;
        if (timeToFire <= 0)
        {
            acceptToFire = true;
        }

        if (playerManager == null && enemyManager == null)
        {
            if (otherEffect)
            {
                otherEffect.SetActive(false);
            }

            if (impactEffect)
            {
                impactEffect.SetActive(false);
            }
        }

        if (checkUse && checkPlayer != true)
        {
            StartCoroutine(waitTochangeCollider());
        }
    }
    IEnumerator waitTochangeCollider()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < allCol.Length; i++)
        {
            allCol[i].isTrigger = false;
        }
        yield return 0;
    }
    void sendtrueDamage()
    {
        /* set value true damage weapon = all damage player and enemy
         * 
         * 
         */
        if (playerManager)
        {
            playerManager.trueDamage = DamageWeapon;
            playerManager.trueDamageSkill = DamageSkill;
            playerManager.forceKB = forceKB;
        }

        if (enemyManager)
        {
            enemyManager.trueDamage = DamageWeapon;
            enemyManager.trueDamageSkill = DamageSkill;
            enemyManager.forceKB = forceKB;
        }
    }
    void checkWeaponTarget()
    {
        foreach (Transform item in trans)
        {
            if (item.CompareTag("player"))
            {
                if (rigi)
                {
                    Destroy(rigi);
                    //rigi.bodyType = RigidbodyType2D.Kinematic;
                }
                checkPlayer = true;
                gameObject.tag = "weapon";
                gameObject.layer = 8;
                checkUse = true;

                if (weapon2)
                {
                    weapon2.tag = "weapon";
                    weapon2.layer = 8;
                }

                if (enableWeapon)
                {
                    gameObject.tag = "ground";
                    gameObject.layer = 0;
                }

            }
            else if (item.CompareTag("enemy"))
            {
                if (rigi)
                {
                    Destroy(rigi);
                    //rigi.bodyType = RigidbodyType2D.Kinematic;
                }
                checkPlayer = false;
                gameObject.tag = "weaponEnemy";
                gameObject.layer = 10;
                checkUse = true;

                if (weapon2)
                {
                    weapon2.tag = "weaponEnemy";
                }

                if (enableWeapon)
                {
                    gameObject.tag = "ground";
                    gameObject.layer = 0;
                }
            }
        }
        //Debug.Log(checkPlayer);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("player"))
        {
            if (enemyManager)
            {
                enemyManager.CheckAttackFromEnemy = true; // kiem tra den luot enemy nao tan cong
            }
        }

        if (col.collider.CompareTag("weaponEnemy"))
        {
            if (playerManager)
            {
                animP.receiveDamage = 0;
                animP.hurt = false;
                animP.anim = null;
                //timeC.DoSlowmotion();
            }  
        }

        if (col.collider.CompareTag("weapon"))
        {
            if (enemyManager)
            {
                animE.receiveDamage = 0;
                animE.hurt = false;
                animE.anim = null;
            }
        }

        if (col.collider.CompareTag("ground") || col.collider.CompareTag("sky"))
        {
            checkGround = true;
        }

        if ((gameObject.tag == "weaponEnemy" && col.collider.tag == "player") || (gameObject.tag == "weapon" && col.collider.tag == "enemy"))
        {
            checkInTarget = true;

            if (audioColl != null)
            {
                audioManager.PlaySound(audioColl);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (gameObject.CompareTag("unweapon") && (col.collider.CompareTag("ground") || col.collider.CompareTag("sky")))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            gameObject.layer = 12;

            for (int i = 0; i < allCol.Length; i++)
            {
                allCol[i].isTrigger = true;
            }

            Destroy(rigi);
        }

        if (col.collider.CompareTag("ground") || col.collider.CompareTag("sky") && checkUse)
        {
            checkGround = true;

            TIME_GROUND -= Time.deltaTime;
            if (TIME_GROUND <= 0)
            {
                for (int i = 0; i < allCol.Length; i++)
                {
                    allCol[i].isTrigger = true;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (enemyManager)
        {
            enemyManager.CheckAttackFromEnemy = false;
        }

        if (gameObject.CompareTag("unweapon") && col.collider.CompareTag("ground"))
        {
            StopCoroutine(setKinematic());
        }

        if (col.collider.CompareTag("ground") || col.collider.CompareTag("sky"))
        {
            checkGround = false;
        }

        if ((gameObject.tag == "weaponEnemy" && col.collider.tag == "player") || (gameObject.tag == "weapon" && col.collider.tag == "enemy"))
        {
            checkInTarget = false;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("ground") || col.CompareTag("sky") && checkUse)
        {
            checkGround = false;

            TIME_GROUND = 1;
            for (int i = 0; i < allCol.Length; i++)
            {
                allCol[i].isTrigger = false;
            }
        }
    }

    public void Skill() // ki nang dac biet cua weapon
    {
        if (FirePoint)
        {
             if (skill)
             {
                if (acceptToFire)
                {
                    if (checkPlayer)
                    {
                        //skill.tag = "weapon";
                        skill.tag = "skillP";
                    }
                    else if (checkPlayer == false)
                    {
                        //skill.tag = "weaponEnemy";
                        skill.tag = "skillE";
                        enemyManager.CheckAttackFromEnemy = true;
                        StartCoroutine(waitcheck());
                    }

                    GameObject bulet;
                    bulet = Instantiate(skill, FirePoint.position, FirePoint.rotation) as GameObject;
                    test++;
                    if (test >= numberFireAccept)
                    {
                        timeToFire = weapon.countDownTime;
                        acceptToFire = false;
                        test = 0;
                    }

                    if (otherEffect)
                    {
                        otherEffect.SetActive(true);
                        StartCoroutine(waitToHideVFX());
                    }

                    if (activelScale)
                    {
                        ScaleWeapon();
                    }

                    if (checkMagnum)
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        var test = GetComponentsInChildren<SpriteRenderer>();
                        if (test != null)
                        {
                            for (int i = 0; i < test.Length; i++)
                            {
                                test[i].enabled = false;
                            }
                        }

                        for (int i = 0; i < allCol.Length; i++)
                        {
                            allCol[i].isTrigger = true;
                        }
                        //GetComponent<Collider2D>().enabled = false;
                        StartCoroutine(backMagnum());
                    }

                    if (audioName != null)
                    {
                        audioManager.PlaySound(audioName);
                    }
                }
             }

             if (FlameWeapon)
             {
                if (acceptToFire)
                {
                    if (checkPlayer)
                    {
                        FlameWeapon.tag = "skillP";
                    }
                    else if (checkPlayer == false)
                    {
                        FlameWeapon.tag = "skillE";
                        enemyManager.CheckAttackFromEnemy = true;
                        StartCoroutine(waitcheck());
                    }

                    test++;
                    if (test >= numberFireAccept)
                    {
                        timeToFire = weapon.countDownTime;
                        acceptToFire = false;
                        test = 0;
                    }

                    FlameWeapon.SetActive(true);
                    StartCoroutine(hideCollider());

                    if (audioName != null)
                    {
                        audioManager.PlaySound(audioName);
                    }
                }
             }

            if (skillOther)
            {
                if (acceptToFire)
                {
                    if (checkPlayer)
                    {
                        //skill.tag = "weapon";
                        impactEffect.tag = "skillP";
                    }
                    else if (checkPlayer == false)
                    {
                        //skill.tag = "weaponEnemy";
                        impactEffect.tag = "skillE";
                        enemyManager.CheckAttackFromEnemy = true;
                        StartCoroutine(waitcheck());
                    }

                    if (laze)
                    {
                        //laze.activeLaze();
                        laze.activeLaze(impactEffect);
                        //Instantiate(impactEffect, FirePoint.position, FirePoint.rotation);
                    }

                    test++;
                    if (test >= weapon.timesToCD)
                    {
                        timeToFire = weapon.countDownTime;
                        acceptToFire = false;
                        test = 0;
                    }

                    if (activelScale)
                    {
                        ScaleWeapon();
                    }

                    if (otherEffect)
                    {
                        otherEffect.SetActive(true);
                        StartCoroutine(waitToHideVFX());
                    }

                    if (audioName != null)
                    {
                        audioManager.PlaySound(audioName);
                    }
                }
            }
        }
    } 
    IEnumerator hideCollider()
    {
        yield return new WaitForSeconds(timeToHide);
        FlameWeapon.SetActive(false);
        yield return 0;
    }
    public void Throw() // vut weapon
    {
        if (!checkGround && !checkInTarget)
        {
            if (playerManager)
            {
                playerManager.timeCDWeapon = 0;
            }

            if (enemyManager)
            {
                enemyManager.timeCDWeapon = 0;
            }

            GameObject weapon;
            weapon = Instantiate(gameObject, FirePoint.position, FirePoint.rotation) as GameObject;
            if (GetComponentInParent<Local>())
            {
                weapon.transform.SetParent(GetComponentInParent<Local>().transform);
            }
            else if (GetComponentInParent<LocalForPVP>())
            {
                weapon.transform.SetParent(GetComponentInParent<LocalForPVP>().transform);
            }
            else if (GetComponentInParent<LocalForSurvival>())
            {
                weapon.transform.SetParent(GetComponentInParent<LocalForSurvival>().transform);
            }

            weapon.tag = "unweapon";
            weapon.layer = 15;
            weapon.GetComponent<WeaponController>().checkUse = false;
            weapon.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if (rightHands)
            {
                Destroy(rightHands.gameObject);
            }

            Destroy(gameObject);
        }else
        {
            Debug.Log("Yot can't because ground");
        }
    }
    IEnumerator setKinematic() // set vi tri tinh cho weapon
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        yield return 0;
    }
    IEnumerator waitcheck()
    {
        yield return new WaitForSeconds(0.1f);
        enemyManager.CheckAttackFromEnemy = false;
        yield return 0;
    }
    IEnumerator waitToHideVFX()
    {
        yield return new WaitForSeconds(timeActivelEffect);
        otherEffect.SetActive(false);
        yield return 0;
    }
    void ScaleWeapon()
    {
        gameObject.transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
        StartCoroutine(backToNormal());
    }
    IEnumerator backToNormal()
    {
        yield return new WaitForSeconds(timeScale);
        gameObject.transform.localScale = scaleNormal;
        yield return 0;
    }
    IEnumerator backMagnum()
    {
        yield return new WaitForSeconds(timeScale);
        GetComponent<SpriteRenderer>().enabled = true;

        var test = GetComponentsInChildren<SpriteRenderer>();
        if (test != null)
        {
            for (int i = 0; i < test.Length; i++)
            {
                test[i].enabled = true;
            }
        }

        for (int i = 0; i < allCol.Length; i++)
        {
            allCol[i].isTrigger = false;
        }
        //GetComponent<Collider2D>().enabled = true;
        yield return 0;
    }
}
