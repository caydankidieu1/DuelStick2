using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rigi;
    public GameObject prefab;

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
            //Debug.Log("yes");
        }
        else if (!col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE") )
        {
            Boom();
            Destroy(gameObject);
        }
    }
}
