using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewForce : MonoBehaviour
{
    public ControllerTrapCreates controller;
    public GameObject prefabBreakVersion;
    public int minForce = 2;
    public float timeToDestroy = 15f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude >= minForce)
        {
            GameObject T =  Instantiate(prefabBreakVersion, transform.position, transform.rotation) as GameObject;

            if (controller)
            {
                controller.checkCreate = true;
                T.transform.SetParent(controller.gameObject.transform);
            }

            Destroy(T, timeToDestroy);
            Destroy(gameObject);
        }
    }
}
