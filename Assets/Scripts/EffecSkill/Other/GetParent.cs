using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "skillP" && col.CompareTag("weapon"))
        {
            transform.SetParent(col.transform);
        }

        if (gameObject.tag == "skillE" && col.CompareTag("weaponEnemy"))
        {
            transform.SetParent(col.transform);
        }
    }
}
