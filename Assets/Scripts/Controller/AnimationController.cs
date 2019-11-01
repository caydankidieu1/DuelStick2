using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Value")]
    public bool checkLocaltionWantHinge;
    public float receiveDamage = 1;
    public bool hurt;
    [Space]
    public bool testJet;
    public float timeJet;
    [Space]
    public bool pointDontBase;

    [Header("----------------------- time hinge back------------------")]
    public float TimeHingeBack = 1f;

    [Header("Component")]
    public Rigidbody2D rgb2;
    public Animator anim;
    public HingeJoint2D hinge;
    public PlayerManager playerManager;
    public SpriteRenderer body;

    [Header("---------------------- Local Explore ---------------------")]
    public Transform destination;
    public Transform cloneDestination;

    [Header("-------------------- Test Limit Attack ------------------")]
    public int LimitAttack;

    [Header("Sound")]
    private Audio audioManager;
    public string nameSound;

    [Header("ForceHurt")]
    public int minForce = 2;


    public void Start()
    {
        audioManager = Audio.instance;

        LimitAttack = 0;
        rgb2 = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        anim = GetComponent<Animator>();
        playerManager = GetComponentInParent<PlayerManager>();
        body = GetComponent<SpriteRenderer>();
        hurt = false;
    }
    private void Update()
    {
        if (anim)
        {
            anim.SetBool("hurt", hurt);
        }

        if (hurt)
        {
            StartCoroutine(waitHurt());
        }

        if (testJet)
        {
            timeJet += Time.deltaTime;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            timeJet = 0;
        }

        if (destination)
        {
            cloneDestination = destination;
        }
    }
    IEnumerator waitHurt() // xu ly truong hop bi dinh skill 
    {
        yield return new WaitForSeconds(TimeHingeBack);
        hurt = false;
        if (hinge && !checkLocaltionWantHinge)
        {
            hinge.useLimits = true;

        }
        yield return 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude >= minForce && col.collider.tag == "ground")
        {
            if (gameObject.name == "Head")
            {
                Debug.Log("hurt");
                playerManager.DamageByBox();
            }
        }

        if (pointDontBase == false)
        {
            if (col.collider.CompareTag("weaponBaseE"))
            {
                if (nameSound != null)
                {
                    audioManager.PlaySound(nameSound);
                }
            }
        }

        if (LimitAttack <= 1)
        {
            if (col.collider.CompareTag("weaponEnemy"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                    playerManager.SendMessage("sumHitHead");
                }

                playerManager.SendMessage("Damage", receiveDamage);
                playerManager.SendMessage("sumHit");

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }

            if (pointDontBase == false)
            {
                if (col.collider.CompareTag("weaponBaseE"))
                {
                    if (nameSound != null)
                    {
                        audioManager.PlaySound(nameSound);
                    }

                    hurt = true;

                    if (gameObject.name == "Head")
                    {
                        receiveDamage = receiveDamage * 2;
                        playerManager.SendMessage("sumHitHead");
                    }

                    playerManager.SendMessage("DamageBase", receiveDamage);
                    playerManager.SendMessage("sumHit");

                    if (hinge)
                    {
                        //hinge.useLimits = false;
                    }

                    LimitAttack++;
                }
            }

            if (col.collider.CompareTag("skillE"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                    playerManager.SendMessage("sumHitHead");
                }

                playerManager.SendMessage("DamageBySkill", receiveDamage);
                playerManager.SendMessage("sumHit");

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }
        }
        else
        {
            StartCoroutine(limitAttack());
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {

        if (timeJet >= 2.5f && timeJet < 3.5f)
        {
            if (col.collider.tag == "weapon" || col.collider.tag == "sky" || col.collider.tag == "ground" || col.collider.tag == "enemy" ||
                col.collider.tag == "weaponEnemy")
            {
                testJet = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("weaponEnemy"))
        {
            hurt = false;

            if (hinge && !checkLocaltionWantHinge)
            {
                hinge.useLimits = true;
            }

            playerManager.SendMessage("offAudio");
        }

        testJet = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (pointDontBase == false)
        {
            if (col.CompareTag("weaponBaseE"))
            {
                if (nameSound != null)
                {
                    audioManager.PlaySound(nameSound);
                }
            }
        }

        if (LimitAttack <= 1)
        {
            if (col.CompareTag("trap"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                }

                playerManager.SendMessage("DamageTrap", receiveDamage);

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }

            if (col.CompareTag("skillE"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                    playerManager.SendMessage("sumHitHead");
                }

                playerManager.SendMessage("DamageBySkill", receiveDamage);
                playerManager.SendMessage("sumHit");

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }
        }
        else
        {
            StartCoroutine(limitAttack());
        }
    }

    public void activelDis()
    {
        StartCoroutine(actionDisappear());
    }
    public IEnumerator actionDisappear()
    {
        yield return new WaitForSeconds(1f);
        float duration = 3;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            body.color = new Color(255, 255, 255, (float)Mathf.Lerp(1, 0, progress));
            yield return null;
        }

        body.color = new Color(255, 255, 255, 0);
    }
    public void ExploreBody()
    {
        if (destination)
        {
            Vector3 test;
            test = transform.position - cloneDestination.position;
            var distance = test.magnitude;
            var direction = test / distance;

            //Debug.Log(direction);
            rgb2.AddForce(-direction * 10 * 100 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    IEnumerator limitAttack()
    {
        yield return new WaitForSeconds(0.5f);
        LimitAttack = 0;
        yield return 0;
    }
}
