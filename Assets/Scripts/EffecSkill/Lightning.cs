using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float timeToDestoy;

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
    }

    void Update()
    {
       
        timeToDestoy -= Time.deltaTime;
        if (timeToDestoy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "skillP" && col.collider.CompareTag("weapon"))
        {
            gameObject.layer = 8;
        }

        if (gameObject.tag == "skillE" && col.collider.CompareTag("weaponEnemy"))
        {
            gameObject.layer = 10;
        }

        if (gameObject.tag == "skillP" && col.collider.CompareTag("player"))
        {
            gameObject.layer = 8;
        }

        if (gameObject.tag == "skillE" && col.collider.CompareTag("enemy"))
        {
            gameObject.layer = 10;
        }
    }


}
