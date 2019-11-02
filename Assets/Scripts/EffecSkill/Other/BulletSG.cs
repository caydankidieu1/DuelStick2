using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSG : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;
    public Rigidbody2D rigi;
    public float force = 200;
    public float timeToDestroy = 1f;
    private Vector2 pos;
    private Vector2 direction;
    public GameObject prefab;

    private void Awake()
    {
        pos = transform.position - parent.transform.position;
        var distance = pos.magnitude;
        direction = pos / distance;

        gameObject.tag = parent.tag;

        if (gameObject.tag == "skillP")
        {
            gameObject.layer = 8;
        }
        else if (gameObject.tag == "skillE")
        {
            gameObject.layer = 10;
        }
    }

    void Start()
    {
        //rigi.velocity = (direction * force * Time.deltaTime * 100);
        rigi.AddForce(direction * force * 10, ForceMode2D.Impulse);
        StartCoroutine(wait());
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
            force = 0;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            if (child)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            GetComponent<Collider2D>().enabled = false;
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
        if (!col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE"))
        {
            Boom();
        }
    }
}
