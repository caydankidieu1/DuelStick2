using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("slider")]
    public bool P2;
    public Slider Slider;

    [Header("Values")]
    public float HP = 100f;
    public float maxHP;
    public float trueDamage = 10;
    public float trueDamageSkill = 10;
    public float trueReceiveDamage = 10; // damage base phai nhan
    public float trueReceiveDamageSkill = 10; // damage skill phai nhan
    public float trueDamageBase;
    public float timeTrueDeath = 4f;
    public bool trueDeath;

    public bool CheckAttackFromEnemy; // kiem tra enemy[i] voi damage khac nhau

    [Header("Compane")]
    private WeaponController weaponController;
    private Collider2D[] weaponBase;
    private AI ai;
    private HingeJoint2D[] hinge;
    private Animator[] anim;
    public SystemDamage systemDamage;
    [Space]
    public float forceKB;
    public float forceKBReceive;
    public float forceKBBase;
    
    [Header("value Pop-up")]
    private Local local;
    public float IsSumHitHead;
    public float IsSumHit;

    public Rigidbody2D rb2d;
    [Header("-------------------------- PVP ---------------------------")]
    [SerializeField] private LocalForPVP localPVP;
    [Header("-------------------------- Survival ----------------------")]
    [SerializeField] private LocalForSurvival localSur;
    [SerializeField] private CreateWeaponE WeaponE;
    [Space]
    private AnimationControllerEnemy[] body;

    [Header("Audio")]
    private Audio audioManager;
    public string nameSoundPunch;

    [Space]
    public float timeCDWeapon;

    [Header("Animation")]
    public Animator animMe;
    public Pull pull;
    public bool checkGround;
    public ControllerPvP PVP;
    public float smooth = 5f;
    public float forceJump = 3;
    private bool checkhighest;
    private bool test;

    public float damageBoxHp;

    [Header("Cinemachine")]
    public Cinemachine.AddOfGroupTarget camera;

    [Header("Heath Bar")]
    public GameObject heathBarUI;
    public Slider sliderHeath;

    void Start()
    {
        if (sliderHeath)
        {
            sliderHeath.value = CalulateHeath();
        }

        camera = GetComponent<Cinemachine.AddOfGroupTarget>();

        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.Log("No found any audio");
        }


        if (!systemDamage)
        {
            systemDamage = GameObject.FindObjectOfType<SystemDamage>();
        }
        rb2d = GetComponent<Rigidbody2D>();
        local = GetComponentInParent<Local>();
        hinge = GetComponentsInChildren<HingeJoint2D>();
        anim = GetComponentsInChildren<Animator>();
        localPVP = GetComponentInParent<LocalForPVP>();
        localSur = GetComponentInParent<LocalForSurvival>();
        ai = GetComponent<AI>();
        weaponBase = GetComponentsInChildren<Collider2D>();
        WeaponE = GetComponent<CreateWeaponE>();
        body = GetComponentsInChildren<AnimationControllerEnemy>();
        weaponController = GetComponentInChildren<WeaponController>();

        //ChangeLayer();

        damageBoxHp = (HP * 30) / 100; ;

        if (P2 && PVP)
        {
            PVP.P2Jump.onClick.RemoveAllListeners();
            PVP.P2Jump.onClick.AddListener(setJump);
        }
    }
    void Update()
    {
        #region Old
        if (Slider)
        {
            Slider.value = HP;
        }
        //death();
        if (systemDamage)
        {
            trueReceiveDamageSkill = systemDamage.trueDamageSkillPlayer; // get value receive damage skill Enemy
            trueReceiveDamage = systemDamage.trueDamagePlayer; // get value receive damage Enemy = damage player from SystemDamage
            trueDamageBase = systemDamage.damageBase;

            forceKBReceive = systemDamage.trueKnockBPlayer;
            forceKBBase = systemDamage.KnockBase;

            if (CheckAttackFromEnemy)
            {
                systemDamage.trueDamageEnemy = trueDamage;
                systemDamage.trueDamageSkillEnemy = trueDamageSkill;
                systemDamage.trueKnockBEnemy = forceKB;
            }
        }
      

        if (HP <= 0)
        {
            StartCoroutine(waitGetDeath());
            trueDamage = 0;
            trueDamageSkill = 0;
            trueReceiveDamage = 0;
            trueDamageBase = 0;
            trueReceiveDamageSkill = 0;
            if (ai != null)
            {
                ai.DropWeapon();
                ai.enabled = false;
                ai = null;
            }
            for (int i = 0; i < weaponBase.Length; i++)
            {
                if (weaponBase[i].tag == "weaponBaseE")
                {
                    weaponBase[i].gameObject.SetActive(false);
                }
            }

            if (WeaponE)
            {
                if (WeaponE.weaponManager.GetComponent<Collider2D>())
                {
                    WeaponE.weaponManager.GetComponent<Collider2D>().enabled = false;
                }
            }
        }

        KnockBackMe();

        if (timeCDWeapon != 0 && P2)
        {
            if (systemDamage)
            {
                systemDamage.timeP2 = timeCDWeapon;
            }
        }
        else if (timeCDWeapon == 0 && P2)
        {
            systemDamage.timeP2 = 0;
        }
        #endregion

        if (P2)
        {
            if (pull.InputXP2 > 0)
            {
                animMe.SetBool("runRight", true);
                animMe.SetBool("runLeft", false);

                // Quaternion target = Quaternion.Euler(0, 0, 0);
                // transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            else if (pull.InputXP2 < 0)
            {
                animMe.SetBool("runRight", false);
                animMe.SetBool("runLeft", true);

                //Quaternion target = Quaternion.Euler(0, 0, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            else if (pull.InputXP2 == 0 && pull.InputZP2 == 0)
            {
                animMe.SetBool("runRight", false);
                animMe.SetBool("runLeft", false);
            }
        }

        if (camera && HP <= 0)
        {
            camera.RemoveMe();
            camera = null;
        }

        if (sliderHeath)
        {
            sliderHeath.value = CalulateHeath();
        }
    
        if (HP < maxHP && HP > 0)
        {
            if (heathBarUI)
            {
                heathBarUI.SetActive(true);
            }
        }
        else if (HP <= 0)
        {
            if (heathBarUI)
            {
                heathBarUI.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!checkGround && !checkhighest)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0, -5) * 3f * Time.fixedDeltaTime);
        }

        if (checkGround)
        {
            checkhighest = true;
            test = true;
        }
        else if (test)
        {
            StartCoroutine(getHighest());
            test = false;
        }
    }
    void Damage(float damage)
    {
        //HP -= trueReceiveDamage * damage;
        if (systemDamage)
        {
            Vector2 pos;
            float force = 0;

            pos = transform.position - systemDamage.local;
            var distance = pos.magnitude;
            var direction = pos / distance;

            rb2d.AddForce(direction * 10 * forceKBReceive * 3, ForceMode2D.Impulse);
        }


        if (P2 && localPVP)
        {
            Slider.GetComponent<ControllerHPBar>().attack();
        }

        sendDeath();
    }
    void Damage2(float damage)
    {
        //HP -= trueReceiveDamageSkill * damage;

        if (P2 && localPVP)
        {
            Slider.GetComponent<ControllerHPBar>().attack();
        }

        sendDeath();
    }
    void DamageBase(float damage)
    {
        //HP -= trueDamageBase * damage;

        if (nameSoundPunch != null)
        {
            audioManager.PlaySound(nameSoundPunch);
        }

        if (P2 && localPVP)
        {
            Slider.GetComponent<ControllerHPBar>().attack();
        }

        sendDeath();
    }
    void DamageTrap(float damage)
    {
        //HP -= 10 * damage;

        if (P2 && localPVP)
        {
            Slider.GetComponent<ControllerHPBar>().attack();
        }

        sendDeath();
    }
    void sumHitHead()
    {
        if (local)
        {
            IsSumHitHead++;
            local.SendMessage("sumHitHead");
        }
        if (localPVP)
        {
            localPVP.SendMessage("sumHitHeadP2");
        }
        if (localSur)
        {
            IsSumHitHead++;
            localSur.SendMessage("sumHitHead");
        }
    }
    void sumHit()
    {
        if (local)
        {
            IsSumHit++;
            local.SendMessage("sumHit");
        }
        if (localPVP)
        {
            localPVP.SendMessage("sumHitP2");
        }
        if (localSur)
        {
            localSur.SendMessage("sumHit");
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeTrueDeath);
        Destroy(gameObject);
        yield return 0;
    }
    IEnumerator waitGetDeath()
    {
        yield return new WaitForSeconds(.001f);
        if (local)
        {
            local.SendMessage("death");
            local = null;
        }

        if (localPVP)
        {
            localPVP.SendMessage("deathP2");
            localPVP = null;
        }

        if (localSur)
        {
            localSur.SendMessage("death");

            if (gameObject.transform.localScale.x >= 1.1f)
            {
                localSur.SendMessage("deathBoss");
            }

            localSur = null;
        }

        systemDamage = null;
        yield return 0;
    }
    private void KnockBackMe()
    {
        if (systemDamage)
        {
            if (systemDamage.knockBack)
            {
                Vector2 pos;
                float force = 0;

                pos = transform.position - systemDamage.local;
                var distance = pos.magnitude;
                var direction = pos / distance;
                //Debug.Log(distance);
                if (distance > 10)
                {
                    force = 0;
                }
                else
                {
                    force = 2;
                }

                rb2d.velocity = direction * force * 25 * 50 * Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 pos;
        
        if (col.CompareTag(Tags.Wplayer))
        {
            HP -= trueReceiveDamage;

            pos = transform.position - col.gameObject.transform.position;
            var distance = pos.magnitude;
            var direction = pos / distance;

            //rb2d.velocity = new Vector2(-100, rb2d.velocity.y);
            //rb2d.velocity = direction * forceKBReceive * 25 * 100 * Time.deltaTime;

            rb2d.AddForce(direction * 20 * forceKBReceive * 3, ForceMode2D.Impulse);

            sendDeath();
        }

        if (col.CompareTag(Tags.SP))
        {
            HP -= trueReceiveDamageSkill;

            pos = transform.position - col.gameObject.transform.position;
            var distance = pos.magnitude;
            var direction = pos / distance;

            //rb2d.velocity = new Vector2(-100, rb2d.velocity.y);
            rb2d.velocity = direction * forceKBReceive * 25 * 100 * Time.deltaTime;

            sendDeath();
        }

        if (col.tag == "weaponBaseP")
        {
            HP -= trueDamageBase;

            pos = transform.position - col.gameObject.transform.position;
            var distance = pos.magnitude;
            var direction = pos / distance;

            //rb2d.velocity = new Vector2(-100, rb2d.velocity.y);
            rb2d.velocity = direction * forceKBBase * 27 * 100 * Time.deltaTime;

            sendDeath();
        }

        if (col.tag == "trap")
        {
            HP -= damageBoxHp;

            pos = transform.position - col.gameObject.transform.position;
            var distance = pos.magnitude;
            var direction = pos / distance;

            rb2d.velocity = direction * 4f * 25 * 100 * Time.deltaTime;

            sendDeath();
        }

        if (col.CompareTag("deathZone"))
        {
            HP -= 999999999;

            sendDeath();
        }

        if (col.CompareTag(Tags.deathAir))
        {
            HP -= 999999999;
            sendDeathVersion2();
        }

        if (col.CompareTag("ground"))
        {
            checkGround = true;
        }
    } // knock Back
    public void sendDeath()
    {
        if (HP <= 0)
        {
            trueDeath = true;
            StartCoroutine(waitGetDeath());

            foreach (HingeJoint2D item in hinge)
            {
                item.breakForce = 1;
                item.breakTorque = 1;
                item.enabled = false;
            }

            foreach (Animator item in anim)
            {
                item.enabled = false;
            }

            sendHideBody();
            StartCoroutine("wait");
        }
        else
        {
            trueDeath = false;
            if (systemDamage)
            {
                systemDamage.trueDeath = trueDeath;
            }
        }
    }
    public void sendDeathVersion2()
    {
        if (HP <= 0)
        {
            trueDeath = true;
            StartCoroutine(waitGetDeath());
            StartCoroutine("wait");
        }
        else
        {
            trueDeath = false;
            if (systemDamage)
            {
                systemDamage.trueDeath = trueDeath;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ground")
        {
            checkGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "ground")
        {
            checkGround = false;
        }
    }
    void sendHideBody()
    {
        if (WeaponE)
        {
            WeaponE.weaponController.enableWeapon = true;
        }
        
        for (int i = 0; i < body.Length; i++)
        {
            if (body[i] != null)
            {
                //body[i].destination = null;
                body[i].ExploreBody();
                body[i].gameObject.layer = 9;
                body[i].activelDis();
                body[i] = null;
            }
        }
    }
    public void ChangeLayer()
    {
        if (body != null)
        {
            for (int i = 0; i < body.Length; i++)
            {
                body[i].gameObject.layer = 9;
            }
            StartCoroutine(waitTochangeNormal());
        }
    }
    IEnumerator waitTochangeNormal()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < body.Length; i++)
        {
            body[i].gameObject.layer = 11;
        }
        yield return 0;
    }
    public void setJump()
    {
        if (checkGround)
        {
            rb2d.AddForce(Vector2.up * forceJump * 50, ForceMode2D.Impulse);
            StartCoroutine(getHighest());
        }
    }
    IEnumerator getHighest()
    {
        yield return new WaitForSeconds(0.5f);
        checkhighest = false;
        yield return 0;
    }
    public void DamageByBox()
    {
        if (P2)
        {
            //HP -= damageBoxHp;

            if (HP <= 0)
            {
                trueDeath = true;
                StartCoroutine(waitGetDeath());

                foreach (HingeJoint2D item in hinge)
                {
                    item.breakForce = 1;
                    item.breakTorque = 1;
                    item.enabled = false;
                }

                foreach (Animator item in anim)
                {
                    item.enabled = false;
                }

                sendHideBody();
                StartCoroutine("wait");
            }
            else
            {
                trueDeath = false;
                if (systemDamage)
                {
                    systemDamage.trueDeath = trueDeath;
                }
            }
        }
    }

    float CalulateHeath()
    {
        return HP / maxHP;
    }
}
