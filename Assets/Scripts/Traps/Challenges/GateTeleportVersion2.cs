using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTeleportVersion2 : MonoBehaviour
{
    public Transform Gate2;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player" || col.tag == "enemy")
        {
            if (col.tag == "player" && col.gameObject.GetComponent<PlayerManager>() != null)
            {
                col.transform.position = Gate2.transform.position;
            }

            if (col.tag == "enemy" && col.gameObject.GetComponent<EnemyManager>() != null)
            {
                col.transform.position = Gate2.transform.position;
            }
        }
    }
}
