using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public int minForce;
    public GameObject brokenObject;
    public Transform parent;

    private void OnCollisionEnter2D(Collision2D col)
    {
        //print("The Magnitude Was: " + col.relativeVelocity.magnitude.ToString());
        if (col.relativeVelocity.magnitude >= minForce && (col.collider.tag == "skillE") || (col.collider.tag == "skillP"))
        {
            Shatter();
        }
    }

    void Shatter()
    {
        GameObject T =  Instantiate(brokenObject, transform.position, transform.rotation) as GameObject;
        T.transform.SetParent(parent);

        Destroy(T, 5f);
        //Destroy(this.gameObject);

        gameObject.SetActive(false);
    }
}
