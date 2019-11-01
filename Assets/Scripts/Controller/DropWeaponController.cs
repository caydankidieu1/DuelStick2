using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeaponController : MonoBehaviour
{
    public GameObject parent;
    public GameObject[] weapon;
    public Transform trans;
    public float timeDrop = 10f;


    private void Update()
    {
        timeDrop -= Time.deltaTime;
        if (timeDrop <= 0)
        {
            CreateWeapon();
            timeDrop = 10f;
        }
    }

    public void CreateWeapon()
    {
        var x = Random.Range(0, weapon.Length);
        GameObject W;
        W = Instantiate(weapon[x], trans.position, trans.rotation);
        W.transform.SetParent(parent.transform);
        W.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        W.tag = "unweapon";
        W.layer = 12;
    }
}
