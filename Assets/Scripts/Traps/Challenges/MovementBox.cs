using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public float timeToDestroy = 5;
    private bool test;

    private void Start()
    {
        test = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    /*    void Update()
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0 && test)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                Destroy(gameObject, 5f);
                test = false;
            }
        }*/

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player" || col.tag == "enemy")
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0 && test)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                Destroy(gameObject, 5f);
                test = false;
            }
        }
    }
}
