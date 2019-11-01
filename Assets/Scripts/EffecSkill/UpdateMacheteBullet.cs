using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMacheteBullet : MonoBehaviour
{
    public float force;
    public float timeToDestroy = 5f;
    public float timeToChange = 2f;
    [Range(0f, 3.5f)]
    public float forceDrag;
    public bool checkForward;

    public Rigidbody2D rigi;
    public Pull pull;
    private Vector3 test;
    public float timeToRotation;
    public AI ai;
    private bool testAI;

    private void Start()
    {
        testAI = false;

        pull = FindObjectOfType<Pull>();
        test = transform.right;
        StartCoroutine(wait());
    }
    private void Update()
    {
        rigi.velocity = (test * force * Time.deltaTime * 15);

       

        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0)
        {
            Boom();
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    void Boom()
    {
        force = 0;
        transform.Rotate(new Vector3(0, 0, 0));
        rigi.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;


        if (gameObject.tag == "skillP")
        {
            pull.Bullet(transform.right * forceDrag);
        }
        else if (gameObject.tag == "skillE")
        {
            if (pull.rigiP2 != null)
            {
                pull.Bullet2(transform.right * forceDrag);
            }
            else
            {
                ai.Bullet2(transform.right * forceDrag);
            }
        }
    }

    IEnumerator waitToDrag()
    {
        yield return new WaitForSeconds(0.1f);

        if (gameObject.tag == "skillP")
        {
            pull.Bullet(transform.right * forceDrag);
        }
        else if (gameObject.tag == "skillE")
        {
            if (pull.rigiP2)
            {
                pull.Bullet2(transform.right * forceDrag);
            }
        }

        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((gameObject.tag == "skillE" && col.collider.tag == "skillE") || (gameObject.tag == "skillP" && col.collider.tag == "skillP")
            || (gameObject.tag == "skillE" && col.collider.tag == "enemy") || (gameObject.tag == "skillP" && col.collider.tag == "player")
            || (gameObject.tag == "skillE" && col.collider.tag == "weaponEnemy") || (gameObject.tag == "skillP" && col.collider.tag == "weapon"))
        {
            Debug.Log("yes");
        }
        else if (!col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE"))
        {
            Boom();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "skillE" && col.CompareTag("weapon") && testAI)
        {
            ai = col.GetComponent<WeaponController>().ai;
            testAI = false;
        }
    }
}
