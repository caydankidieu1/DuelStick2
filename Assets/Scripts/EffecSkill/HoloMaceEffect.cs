using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMaceEffect : MonoBehaviour
{
    public GameObject mainWeapon;
    public float force;
    private bool checkToBack;
    public float timeToback = 5f;
    public float timeToDestroy = 1f;
    private Rigidbody2D rigi;
    private Collider2D coll;
    private DistanceJoint2D distanceJ;

    void Start()
    {
        mainWeapon.tag = gameObject.tag;
        rigi = mainWeapon.GetComponent<Rigidbody2D>();
        coll = mainWeapon.GetComponent<Collider2D>();
        distanceJ = mainWeapon.GetComponent<DistanceJoint2D>();
        distanceJ.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeToback -= Time.deltaTime;
        if (checkToBack == false)
        {
            rigi.velocity = (transform.right * force * Time.deltaTime * 50);
        }

        if (timeToback <= 0)
        {
            Boom();
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
            Boom();
        }
    }

    private void Boom()
    {
        checkToBack = true;
        coll.isTrigger = true;
        distanceJ.enabled = true;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }
}
