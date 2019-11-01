using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeTodestroy = 2f;
    public Rigidbody2D rigi;
    public float force;
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        timeTodestroy -= Time.deltaTime;
        if (timeTodestroy <= 0)
        {
            Destroy(gameObject);
        }

        //rigi.AddForce(transform.up * force * Time.deltaTime * 50);
        if (rigi)
        {
            rigi.velocity = (transform.up * force * Time.deltaTime);
        }
    }

    void Boom()
    {
        if (prefab)
        {
            force = 0;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            StartCoroutine(waitToChange());
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator waitToChange()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = false;
        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("enemy") || !col.CompareTag("weaponEnemy"))
        {
            Boom();
        }
    }
}
