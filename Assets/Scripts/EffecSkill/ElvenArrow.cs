using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvenArrow : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float force;

    private void Update()
    {
        rigi.velocity = transform.right * force * Time.deltaTime * 10;
        //rigi.AddForce(transform.right * force * Time.deltaTime * 100);
    }
}
