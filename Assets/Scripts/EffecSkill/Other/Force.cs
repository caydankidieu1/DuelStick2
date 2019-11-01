using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float timeToDestroy;
    public float force;
    public GameObject prefab;
    public GameObject CLone;

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
    }
    // Update is called once per frame
    void Update()
    {
        rigi.AddForce(transform.right * force * Time.deltaTime * 100);
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
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
            Boom();
            if (GetComponent<SpriteRenderer>())
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (CLone)
            {
                prefab = null;
                CLone.SetActive(false);
                force = 0;
                transform.position = transform.position;
                rigi.velocity = Vector2.zero;
            }
        }
    }

}
