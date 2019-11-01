using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerEnemy : MonoBehaviour
{
    [Header("Value")]
    public bool checkLocaltionWantHinge;
    public float receiveDamage = 1;
    public float receiveDamageSkill = 1;
    public bool hurt;
    public bool Player2;
    [Space]
    public bool pointDontBase;

    [Header("-------------- time hinge comback-----------")]
    public float timeHingeBack = 1f;

    [Header("Component")]
    public Animator anim;
    public HingeJoint2D hinge;
    public EnemyManager enemyManager;
    public SpriteRenderer body;
    public Rigidbody2D rgb2;

    [Header("--------------- Local Explore ---------------")]
    public Transform destination;
    public Transform cloneDestination;

    public int LimitAttack;

    [Header("ForceHurt")]
    public int minForce = 2;

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        anim = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
        body = GetComponent<SpriteRenderer>();
        hurt = false;
        rgb2 = GetComponent<Rigidbody2D>();
        cloneDestination = destination;
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

        if (destination)
        {
            cloneDestination = destination;
        }
    }
    IEnumerator waitHurt() // xu ly truong hop bi dinh skill 
    {
        yield return new WaitForSeconds(timeHingeBack);
        hurt = false;
        if (hinge && !checkLocaltionWantHinge)
        {
            hinge.useLimits = true;
        }
        yield return 0;
    }
    public void takeDamge()
    {
        hurt = true;
        if (LimitAttack <= 1)
        {
            if (gameObject.name == "Head")
            {
                receiveDamageSkill = receiveDamageSkill * 2;
                enemyManager.SendMessage("sumHitHead");
            }

            enemyManager.SendMessage("Damage2", receiveDamageSkill);
            enemyManager.SendMessage("sumHit");

            LimitAttack++;
        }
        else
        {
            StartCoroutine(limitAttack());
        }
    }
    private void OnCollisionEnter2D(Collision2D col) // vu khi tuong tac vao thi nhap nhay do va gui damage receive 
    {
        if (col.relativeVelocity.magnitude >= minForce && col.collider.tag == "ground")
        {
            if (gameObject.name == "Head")
            {
                Debug.Log("hurt");
                enemyManager.DamageByBox();
            }
        }

        if (LimitAttack <= 1)
        {
            if (col.collider.CompareTag("weapon"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                    enemyManager.SendMessage("sumHitHead");
                }

                enemyManager.SendMessage("Damage", receiveDamage);
                enemyManager.SendMessage("sumHit");
            

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }

            if (pointDontBase == false)
            {
                if (col.collider.CompareTag("weaponBaseP"))
                {
                    hurt = true;

                    if (gameObject.name == "Head")
                    {
                        receiveDamage = receiveDamage * 2;
                        enemyManager.SendMessage("sumHitHead");
                    }

                    enemyManager.SendMessage("DamageBase", receiveDamage);
                    enemyManager.SendMessage("sumHit");


                    if (hinge)
                    {
                        //hinge.useLimits = false;
                    }

                    LimitAttack++;
                }
            }

            if (col.collider.CompareTag("skillP"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamageSkill = receiveDamageSkill * 2;
                    enemyManager.SendMessage("sumHitHead");
                }

                enemyManager.SendMessage("Damage2", receiveDamageSkill);
                enemyManager.SendMessage("sumHit");


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
    private void OnCollisionExit2D(Collision2D col) // vu khi ngung tuong tac thi tat nhap nhay do 
    {
        if (col.collider.CompareTag("weapon"))
        {
            hurt = false;

            if (hinge && !checkLocaltionWantHinge)
            {
                hinge.useLimits = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LimitAttack <= 1)
        {
            if (col.CompareTag("trap") && Player2)
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamage = receiveDamage * 2;
                }

                enemyManager.SendMessage("DamageTrap", receiveDamage);

                if (hinge)
                {
                    //hinge.useLimits = false;
                }

                LimitAttack++;
            }

            if (col.CompareTag("skillP"))
            {
                hurt = true;

                if (gameObject.name == "Head")
                {
                    receiveDamageSkill = receiveDamageSkill * 2;
                    enemyManager.SendMessage("sumHitHead");
                }

                enemyManager.SendMessage("Damage2", receiveDamageSkill);
                enemyManager.SendMessage("sumHit");


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
            body.color = new Color(body.color.r, body.color.g, body.color.b, (float)Mathf.Lerp(1, 0, progress));
            yield return null;
        }

        body.color = new Color(body.color.r, body.color.g, body.color.b, 0);
    }
    public void ExploreBody()
    {
        if (destination)
        {
            gameObject.tag = "Untagged";
            Vector3 test;
            test = transform.position - cloneDestination.position;
            var distance = test.magnitude;
            var direction = test / distance;

            //Debug.Log(direction);
            rgb2.AddForce(-direction * 50 * 100 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    IEnumerator limitAttack()
    {
        yield return new WaitForSeconds(1f);
        LimitAttack = 0;
        yield return 0;
    }
}
