using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowArrow : MonoBehaviour
{
    public float timeToFire;
    public float force;
    public float timeToDestroy = 2f;

    public GameObject arrow1;
    public float timeToFire1;
    [Space]
    public GameObject arrow2;
    public float timeToFire2;
    [Space]
    public GameObject arrow3;
    public float timeToFire3;

    private void Awake()
    {
        arrow1.tag = gameObject.tag;
        arrow2.tag = gameObject.tag;
        arrow3.tag = gameObject.tag;
    }

    private void Update()
    {
        timeToFire1 -= Time.deltaTime;
        timeToFire2 -= Time.deltaTime;
        timeToFire3 -= Time.deltaTime;

        if (timeToFire1 <= 0)
        {
            if (arrow1)
            {
                arrow1.SetActive(true);
                //arrow1.GetComponent<Rigidbody2D>().AddForce(transform.right * force * Time.deltaTime * 100);
                //arrow1.GetComponent<Rigidbody2D>().velocity = (transform.right * force * Time.deltaTime * 7.5f);
            }
        }

        if (timeToFire2 <= 0)
        {
            if (arrow2)
            {
                arrow2.SetActive(true);
                // arrow2.GetComponent<Rigidbody2D>().AddForce(transform.right * force * Time.deltaTime * 100);
                // arrow2.GetComponent<Rigidbody2D>().velocity = (transform.right * force * Time.deltaTime * 7.5f);
            }
        }

        if (timeToFire3 <= 0)
        {
            if (arrow3)
            {
                arrow3.SetActive(true);
                // arrow3.GetComponent<Rigidbody2D>().AddForce(transform.right * force * Time.deltaTime * 100);
                //arrow3.GetComponent<Rigidbody2D>().velocity = (transform.right * force * Time.deltaTime * 7.5f);
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
