using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideClone : MonoBehaviour
{
    public Rigidbody2D rigi;
    public GameObject prefab;
    public GameObject Clone;
    public float force;
    public float forceRotate;
    public float forceRotateMin;
    public float forceRotateMax;
    public float timeToDestroyAffterBoom = 0.5f;
    [Header("something more")]
    public bool exploreNow;
    public float activelExploreNow = 2f;
    public GameObject parent;

    private float timeLimitDestroy = 10f;

    private void Start()
    {
        if (gameObject.tag == "skillP")
        {
            gameObject.layer = 8;
        }
        else if (gameObject.tag == "skillE")
        {
            gameObject.layer = 10;
        }

        forceRotate = Random.Range(forceRotateMin, forceRotateMax);

        if (parent)
        {
            gameObject.tag = parent.tag;
        }

        rigi.AddForce(transform.right * force * 10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (forceRotateMin != 0 || forceRotateMax != 0)
        {
            transform.Rotate(Vector3.back, forceRotate * Time.deltaTime * 10);
        }

        //rigi.velocity = (transform.right * force * Time.deltaTime * 30);

        activelExploreNow -= Time.deltaTime;
        if (exploreNow && activelExploreNow <= 0)
        {
            Boom();
            activelExploreNow = 999;
        }

        timeLimitDestroy -= Time.deltaTime;
        if (timeLimitDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Boom()
    {
        if (prefab)
        {
            if (Clone)
            {
                Clone.SetActive(false);
            }
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (GetComponent<Collider2D>())
            {
                GetComponent<Collider2D>().enabled = false;
            }
            rigi.bodyType = RigidbodyType2D.Kinematic;
            force = 0;
            rigi.velocity = Vector2.zero;
            Instantiate(prefab, transform.position, Quaternion.identity);
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroyAffterBoom);
        Destroy(gameObject);
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if ((gameObject.tag == "skillE" && col.collider.tag == "skillE") || (gameObject.tag == "skillP" && col.collider.tag == "skillP")
            || (gameObject.tag == "skillE" && col.collider.tag == "enemy") || (gameObject.tag == "skillP" && col.collider.tag == "player")
            || (gameObject.tag == "skillE" && col.collider.tag == "weaponEnemy") || (gameObject.tag == "skillP" && col.collider.tag == "weapon"))
        {
            //Debug.Log("yes");
        }
        else if (!col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE"))
        {
            Boom();
        }
    }
}
