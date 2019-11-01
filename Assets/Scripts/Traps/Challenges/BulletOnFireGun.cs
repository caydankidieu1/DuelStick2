using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnFireGun : MonoBehaviour
{
    public GameObject VFX;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "player") || (col.tag == "enemy") || (col.tag == "ground"))
        {
            Boom();
        }

        if (col.tag == "sky")
        {
            transform.SetParent(col.transform);
        }
    }

    void Boom()
    {
        GameObject T = Instantiate(VFX, transform.position, Quaternion.identity);
        T.GetComponent<ParticleSystem>().Play();
        Destroy(T, 2f);
        Destroy(gameObject, 0.1f);
    }
}
