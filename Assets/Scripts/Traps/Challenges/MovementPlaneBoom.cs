using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlaneBoom : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BoomPrefab;
    public float speedPlane;
    public float timeTochangeDir = 5f;
    public bool checkRota;
    private float timeClone;
    private float time;
    private Vector2 test;

    // Update is called once per frame
    private void Start()
    {
        test = Vector2.right;
        timeClone = timeTochangeDir;
    }

    void Update()
    {
        transform.Translate(test * speedPlane);

        timeTochangeDir -= Time.deltaTime;
        time += Time.deltaTime;

        if (time >= 1.5f)
        {
            int test = Random.Range(0, 100);
            if (test >= 50)
            {
                Shoot();
                time = 0;
            }
        }

        if (transform.rotation.y == 0)
        {
            checkRota = true;
        }
        else
        {
            checkRota = false;
        }

        if (timeTochangeDir <= 0)
        {
            if (/*test == Vector2.right*/ checkRota)
            {
                Quaternion newVec = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.rotation = newVec;
                //test = Vector2.left;
            }
            else if(/*test == Vector2.left*/ !checkRota)
            {
                Quaternion newVec = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.rotation = newVec;
                //test = Vector2.right;
            }

            timeTochangeDir = timeClone;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BoomPrefab, FirePoint.position, Quaternion.identity);
        Destroy(bullet, 3f);
    }
}
