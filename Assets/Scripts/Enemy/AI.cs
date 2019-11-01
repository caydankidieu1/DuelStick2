using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Header("values")]
    public EnemyManager enemyManager;
    public Transform target;
    public float force = 10f;
    public float DistanceActiveAi = 5f;
    public float delay = 0f;
    public float timeSkill = 2f;
    private float timeCD;

    [SerializeField] private float cloneTime;
    public Rigidbody2D rigi;
    private Vector2 pos;
    public WeaponController weaponController;
    public LayerMask whatToHit;
    private float timeToMoveTarget = 1f;
    private float timeToJump = 0.5f;
    private float timeclone;

    [Header("New Thing")]
    public Animator anim;

    private void Start()
    {
        timeCD = timeSkill;
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>() ? GameObject.FindGameObjectWithTag("player").GetComponent<Transform>() : gameObject.transform; 
        }
        
        weaponController = GetComponentInChildren<WeaponController>();
        timeclone = timeToJump;
    }

    void FixedUpdate()
    {
        timeToMoveTarget -= Time.deltaTime;
        timeToJump -= Time.deltaTime;
        if (target)
        {
            if (timeToMoveTarget > 0)
            {
                //moveAIAway();
            }
            else 
            {
                //moveAI();
                moveAI2();
            }
        }
  
        AttackSkill();
    }
    public void setWeaponController()
    {
        weaponController = GetComponentInChildren<WeaponController>();
    }
    void moveAI()
    {
        pos = transform.position - target.position;
        var distance = pos.magnitude;
        var direction = pos / distance;
 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position, distance, whatToHit);
        //Debug.DrawLine(transform.position, target.position, Color.red, 100);

        if (hit.collider != null)
        {
            cloneTime += Time.deltaTime;
            if (cloneTime > 0 && cloneTime <= 1.8f)
            {
                rigi.velocity = (Vector2.up * force * Time.deltaTime * 10 * 1.5f);
            }
            else if (cloneTime > 1.8f && cloneTime <= 4f)
            {
                rigi.velocity = (-direction * force * Time.deltaTime * 10 * 1.5f);
            }
            else if (cloneTime > 4 && cloneTime <= 6f)
            {
                rigi.velocity = (direction * force * Time.deltaTime * 10 * 1.5f);
            }
            else
            {
                cloneTime = 0;
            }
        }
        else
        {
            cloneTime = 0;

            if (Vector2.Distance(transform.position, target.position) > DistanceActiveAi)
            {
                rigi.velocity = (-direction * force * Time.deltaTime * 10);
            }
            else if (Vector2.Distance(transform.position, target.position) < DistanceActiveAi)
            {
                rigi.velocity = (-direction * force * Time.deltaTime * 5);
            }
        }
    }
    void moveAI2()
    {
        pos = transform.position - target.position;
        var distance = pos.magnitude;
        var direction = pos / distance;

        direction = new Vector2(direction.x, Mathf.Lerp(direction.y, 0, 0.87f));
        //Debug.Log(direction.x);

        if (direction.x < 0)
        {
            anim.SetBool("runRight", true);
            anim.SetBool("runLeft", false);
        }
        else if (direction.x > 0)
        {
            anim.SetBool("runRight", false);
            anim.SetBool("runLeft", true);
        }
        else if (direction.x == 0 && direction.y == 0)
        {
            anim.SetBool("runRight", false);
            anim.SetBool("runLeft", false);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position, distance + 1, whatToHit);
        Debug.DrawLine(transform.position, target.position, Color.red, 0.01f);
        //Debug.Log(direction);

        if (hit.collider != null)
        {
            rigi.velocity = (-direction * force * Time.deltaTime * 10 * 1.5f);
      
            //Debug.Log(hit.collider.name + " + " + hit.collider.transform.position);
        }
        else
        {
            rigi.velocity = -direction * force * Time.deltaTime * 10 * 1.5f;

           /* if (direction.x > 0)
            {
                rigi.velocity = (new Vector2(-1,0) * force * Time.deltaTime * 10 * 1.5f);
            }

            if (direction.x < 0)
            {
                rigi.velocity = (new Vector2(1, 0) * force * Time.deltaTime * 10 * 1.5f);
            }*/

        }

        if (timeToJump <= 0)
        {
            var test = Random.Range(0, 100);
            if (test >= 25)
            {
                enemyManager.setJump();
            }
            timeToJump = timeclone;
        }

    }
    void moveAIAway()
    {
        pos = transform.position - target.position;
        var distance = pos.magnitude;
        var direction = pos / distance;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position, distance, whatToHit);

        if (hit.collider != null)
        {
            cloneTime += Time.deltaTime;
            if (cloneTime > 0 && cloneTime <= 1.8f)
            {
                rigi.velocity = (Vector2.up * force * Time.deltaTime * 10 * 1.5f);
            }
            else if (cloneTime > 1.8f && cloneTime <= 4f)
            {
                rigi.velocity = (direction * force * Time.deltaTime * 10 * 1.5f);
            }
            else if (cloneTime > 4 && cloneTime <= 6f)
            {
                rigi.velocity = (-direction * force * Time.deltaTime * 10 * 1.5f);
            }
            else
            {
                cloneTime = 0;
            }
        }
        else
        {
            cloneTime = 0;

            if (Vector2.Distance(transform.position, target.position) > DistanceActiveAi)
            {
                rigi.velocity = (direction * force * Time.deltaTime * 10);
            }
            else if (Vector2.Distance(transform.position, target.position) < DistanceActiveAi)
            {
                rigi.velocity = (direction * force * Time.deltaTime * 5);
            }
        }
    }
    void AttackSkill()
    {
        if (weaponController)
        {
            timeSkill -= Time.deltaTime;
            if (timeSkill <= 0)
            {
                weaponController.Skill();
                timeSkill = timeCD;
            }
        }
    }
    public void DropWeapon()
    {
        /*var test = Random.Range(0, 100);
        if (test >= 70)
        {
            if (weaponController)
            {
                weaponController.Throw();
            }
            else Debug.Log("don't have weapon");
        }*/
    }
    public void Bullet2(Vector2 local)
    {
        Vector2 desiredMoveDirectionP2 = local;

        if (rigi)
        {
            rigi.velocity = (desiredMoveDirectionP2 * force * Time.deltaTime * 60 * 2);
        }
    }

}
