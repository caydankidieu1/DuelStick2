using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindController : MonoBehaviour
{
    public Pull pull;
    public bool P2;

    // Update is called once per frame
    void Update()
    {
        if (!P2)
        {
            pull.rigi = gameObject.GetComponent<Rigidbody2D>();
        }
        else if (P2)
        {
            pull.rigiP2 = gameObject.GetComponent<Rigidbody2D>();
        }
        
    }
}
