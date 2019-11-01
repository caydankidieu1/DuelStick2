using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGun : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject prefab;
    public float timeToFire;
    private float cloneTime;
    void Start()
    {
        cloneTime = timeToFire;
    }

    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;

        if (timeToFire <= 0)
        {
            GameObject T = Instantiate(prefab, FirePoint.position, FirePoint.rotation) as GameObject;
            T.gameObject.transform.SetParent(this.gameObject.transform);
            timeToFire = cloneTime;
        }
    }
}
