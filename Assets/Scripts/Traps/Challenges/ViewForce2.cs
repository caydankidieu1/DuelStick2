using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewForce2 : MonoBehaviour
{
    public ControllerTrapCreates controller;
    public GameObject prefabBreakVersion;
    public int minForce = 2;
    public float timeToDestroy = 5f;
    public float timeLife = 5f;
    private bool checkPlayer;

    private void Update()
    {
        if (checkPlayer)
        {
            timeLife -= Time.deltaTime;

            if (timeLife <= 0)
            {
                activelBreakVersion();
                timeLife = 999;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "player" || col.collider.tag == "enemy")
        {
            checkPlayer = true;
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "player" || col.collider.tag == "enemy")
        {
            checkPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "player" || col.collider.tag == "enemy")
        {
            checkPlayer = false;
        }
    }

    public void activelBreakVersion()
    {
        GameObject T = Instantiate(prefabBreakVersion, transform.position, transform.rotation) as GameObject;

        if (controller)
        {
            controller.checkCreate = true;
            T.transform.SetParent(controller.gameObject.transform);
        }

        Destroy(T, timeToDestroy);
        Destroy(gameObject);
    }
}
