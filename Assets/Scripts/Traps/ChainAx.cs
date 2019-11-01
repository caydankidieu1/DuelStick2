using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAx : MonoBehaviour
{
    /*public float force;
    public float timeToChange;
    [SerializeField] private float timeClone;
    [SerializeField] private float timeClone2;
    [SerializeField] private Rigidbody2D rigi;
    [SerializeField] private Vector3 test;
    [SerializeField] private Vector3 test2;

    private void Start()
    {
        test = Vector3.right;
        test2 = new Vector3(0, -1, 0);
    }


    void Update()
    {
        timeClone += Time.deltaTime;
        rigi.AddForce(test * Time.deltaTime * force * 10);
        rigi.AddForce(test2 * Time.deltaTime * force * 10);
        if (timeClone >= timeToChange)
        {
            test *= -1;
            timeClone = 0;
        }
    }*/

    public float force;
    public float timeToChange;
    [SerializeField] private float timeClone;
    [SerializeField] private Rigidbody2D rigi;
    [SerializeField] private Vector2 test;

    private void Start()
    {
        test = new Vector2(10, 0);
        rigi.AddForce(test * Time.deltaTime * force * 5000);
    }

    void Update()
    {
        timeClone += Time.deltaTime;
       
        if (timeClone >= timeToChange)
        {
            if (transform.position.x > 0)
            {
                test = new Vector2(10, 0);
                rigi.AddForce(test * Time.deltaTime * force * 50);
                timeClone = 0;
            }
        }
    }
}
