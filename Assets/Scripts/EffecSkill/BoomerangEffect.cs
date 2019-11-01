using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangEffect : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float timeToDestroy = 10;
    public Transform target;
    public float force;
    public bool checkGoBack;
    private bool checkGobackTwo;
    private Vector2 pos;
    private bool checkEnemy;

    private void Start()
    { 
        if (gameObject.tag == "skillP")
        {
            gameObject.layer = 8;
            target = FindObjectOfType<PlayerManager>().transform;
            checkEnemy = false;
        }
        else if (gameObject.tag == "skillE")
        {
            gameObject.layer = 10;
            target = FindObjectOfType<EnemyManager>().transform;
            checkEnemy = true;
        }
    }

    void Update()
    {
        if (checkGoBack == false)
        {
            rigi.AddForce(transform.right * force * Time.deltaTime * 100);
        }
        else
        {
            if (target)
            {
                pos = transform.position - target.position;
                var distance = pos.magnitude;
                var direction = pos / distance;

                rigi.velocity = (-direction * force * Time.deltaTime * 30);
            }
        }
    
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Boom()
    {
        checkGoBack = true;
        StartCoroutine(wait());
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
        if ((col.CompareTag("player") && checkEnemy == false) || (col.CompareTag("enemy") && checkEnemy))
        {
            Debug.Log("here");
            Destroy(this.gameObject);
        }
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        GetComponent<Collider2D>().isTrigger = true;
        yield return 0;
    }
}
