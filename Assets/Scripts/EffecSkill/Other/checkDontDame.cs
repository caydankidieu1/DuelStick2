using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDontDame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((gameObject.tag == "skillE" && col.tag == "skillE") || (gameObject.tag == "skillP" && col.tag == "skillP")
            || (gameObject.tag == "skillE" && col.tag == "enemy") || (gameObject.tag == "skillP" && col.tag == "player")
            || (gameObject.tag == "skillE" && col.tag == "weaponEnemy") || (gameObject.tag == "skillP" && col.tag == "weapon"))
        {
            Debug.Log("yes");
        }
        else if (!col.CompareTag("enemy") || !col.CompareTag("player"))
        {
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = false;
        yield return 0;
    }
}
