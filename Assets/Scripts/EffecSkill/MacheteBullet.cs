using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteBullet : MonoBehaviour
{
    public float force;
    public float timeToDestroy = 5f;
    public bool checkMulti;
    public bool checkForward;
    public bool checkRotat;
    [SerializeField]private float timeToChange = 0.75f;

    public Rigidbody2D rigi;
    public Pull pull;
    public GameObject prefab;
    private Vector3 test;
    private Vector3 test1;
    private Vector2 test3;
    public float timeToRotation;

    private Audio audioManager;
    public string nameSound;
    private float cloneTimeVirtual = 3;

    private void Start()
    {
        audioManager = Audio.instance;

        test = transform.right;
        test1 = transform.right;

        pull = FindObjectOfType<Pull>();

        if (gameObject.tag == "skillP")
        {
            pull.Bullet(transform.right * 3f);
        }
        else if (gameObject.tag == "skillE")
        {
            if (pull.rigiP2)
            {
                pull.Bullet2(transform.right * 3f);
            }
        }

        StartCoroutine(wait());
    }
    private void Update()
    {
        rigi.velocity = (test * force * Time.deltaTime * 15);

        if (checkForward)
        {
            if (gameObject.tag == "skillP")
            {
                pull.Bullet(test1 * 2f);
            }
            else if (gameObject.tag == "skillE")
            {
                pull.Bullet2(test1 * 2f);
            }

        }
        if (checkMulti)
        {
            timeToChange -= Time.deltaTime;
            if (timeToChange <= 0)
            {
                test *= -1;

                if (gameObject.tag == "skillP")
                {
                    if (test.x < 0)
                    {
                        pull.Bullet(transform.up * 1.7f);
                    }
                    else if (test.x > 0)
                    {
                        pull.Bullet(transform.right * 2.3f);
                    }
                   
                }
                else if (gameObject.tag == "skillE")
                {
                    if (pull.rigiP2)
                    {
                        if (test.x < 0)
                        {
                            pull.Bullet2(transform.up * 1.7f);
                        }
                        else if (test.x > 0)
                        {
                            pull.Bullet2(transform.right * 2.3f);
                        }
                    }
                }

                timeToChange = 0.75f;
            }
        }
        if (checkRotat)
        {
            test3 = new Vector2(0, 1);
            timeToRotation += Time.deltaTime;

            if (timeToRotation >= 0.2f && timeToRotation <= 0.4f)
            {
                test3 = new Vector2(1, 1);
            }
            if (timeToRotation > 0.4f && timeToRotation <= 0.6f)
            {
                test3 = new Vector2(1, 0);
            }
            if (timeToRotation > 0.6f && timeToRotation <= 0.8f)
            {
                test3 = new Vector2(1, -1);
            }
            if (timeToRotation > 0.8f && timeToRotation <= 1f)
            {
                test3 = new Vector2(0, -1);
            }
            if (timeToRotation > 1f && timeToRotation <= 1.2f)
            {
                test3 = new Vector2(-1, 1);
            }
            if (timeToRotation > 1.2f && timeToRotation <= 1.4f)
            {
                test3 = new Vector2(-1, 0);
            }

            if (gameObject.tag == "skillP")
            {
                pull.Bullet(test3 * 1f);
            }
            else if (gameObject.tag == "skillE")
            {
                pull.Bullet2(test3 * 1f);
            }
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
        if (prefab)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
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
            if (!checkMulti)
            {
                Boom();
                Destroy(gameObject);
            }
        }
    }

 
}
