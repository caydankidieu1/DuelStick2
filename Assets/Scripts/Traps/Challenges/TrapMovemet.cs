using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovemet : MonoBehaviour
{

    public GameObject VFX;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "player") || (col.tag == "enemy"))
        {
            Boom();
        }
    }

    void Boom()
    {
        GameObject T = Instantiate(VFX, transform.position, Quaternion.identity);
        Destroy(T, 2f);
        Destroy(gameObject, 0.1f);
    }
}
