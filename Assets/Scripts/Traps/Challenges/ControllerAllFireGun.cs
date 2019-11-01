using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAllFireGun : MonoBehaviour
{
    public Transform[] FirePoint;
    public GameObject bulletPrefab;
    public float timeToFire = 7f;
    public float forceBullet = 2;
    private float cloneTime;

    private void Start()
    {
        cloneTime = timeToFire;
    }

    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;
        if (timeToFire <= 0)
        {
            foreach (Transform item in FirePoint)
            {
                GameObject T = Instantiate(bulletPrefab, item.position, Quaternion.FromToRotation(Vector3.up, -transform.up));
                T.GetComponent<Rigidbody2D>().AddForce(transform.forward * 10 * forceBullet, ForceMode2D.Impulse);
                Destroy(T, 5f);
            }
            timeToFire = cloneTime;
        }
    }
}
