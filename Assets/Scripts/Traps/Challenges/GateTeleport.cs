using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTeleport : MonoBehaviour
{
    public GameObject Gate1;
    public GameObject Gate2;
    public float timeToOpenGate = 5f;
    public float timeToCloseGate = 10f;
    public Collider2D coll;
    private float cloneTime;
    public bool Clone;
    public GateTeleport gate;

    [Header("Open and Close")]
    public GameObject GateOpen;
    public GameObject GateClose;


    void Update()
    {
        if (!Clone)
        {
            cloneTime += Time.deltaTime;

            if (cloneTime >= timeToOpenGate)
            {
                // open Gate
                GateOpen.SetActive(true);
                GateClose.SetActive(false);

                coll.enabled = true;

                if (cloneTime >= timeToCloseGate)
                {
                    cloneTime = 0;
                }

                unActivel();
            }
            else
            {
                // Close Gate
                GateOpen.SetActive(false);
                GateClose.SetActive(true);

                coll.enabled = false;

                activel();
            }
        }
    }

    public void activel()
    {
        if (gate)
        {
            // Open Gate
            gate.GateOpen.SetActive(true);
            gate.GateClose.SetActive(false);

            gate.coll.enabled = true;
        }
    }

    public void unActivel()
    {
        if (gate)
        {
            // Close gate
            gate.GateOpen.SetActive(false);
            gate.GateClose.SetActive(true);

            gate.coll.enabled = false;
        }
    }

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
