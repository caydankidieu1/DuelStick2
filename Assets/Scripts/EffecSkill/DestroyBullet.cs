using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float force = 200;
    public float timeToDestroy = 2f;

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        checklayer();
    }

    void checklayer()
    {
        if (gameObject.CompareTag("weapon"))
        {
            gameObject.layer = 8;
        }
        else if (gameObject.CompareTag("weaponEnemy"))
        {
            gameObject.layer = 10;
        }
    }

    private void Update()
    {
        rigi.AddForce(-transform.right * force * Time.deltaTime * 100);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.CompareTag("weaponEnemy"))
        {
            if (!col.collider.CompareTag("weaponEnemy"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
