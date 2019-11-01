using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBulletMul : MonoBehaviour
{
    public float force;
    public float timeToDestroy = 2f;

    public bullets[] B;

    private void Awake()
    {
        foreach (bullets item in B)
        {
            item.bullet.tag = gameObject.tag;
        }
    }


    private void Update()
    {
        foreach (bullets item in B)
        {
            item.timeToFire -= Time.deltaTime;
        
            if (item.timeToFire <= 0)
            {
                if (item.bullet)
                {
                    item.bullet.SetActive(true);
                    item.bullet.GetComponent<Rigidbody2D>().velocity = (transform.right * force * Time.deltaTime * 10);
                } 
            }
        }

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }
}

[System.Serializable]
public class bullets
{
    public GameObject bullet;
    public float timeToFire;
}
