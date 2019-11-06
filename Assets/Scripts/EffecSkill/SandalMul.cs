using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandalMul : MonoBehaviour
{
    public float timeToDestroy = .5f;
    public int numberFire;
    public float timeDelay;
    private float timeDelayClone;
    private float sum;
    public bool checkM24;

    public GameObject prefab;
    public GameObject VFX;
    public string nametag;

    private void Start()
    {
        nametag = gameObject.tag;
        StartCoroutine(waitchange());
        timeDelayClone = timeDelay;

        if (checkM24)
        {
            timeDelay = 0.1f;
        }
    }

    IEnumerator waitchange()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.tag = Tags.unTag;
        yield return 0;
    }

    private void Update()
    {
        timeDelay -= Time.deltaTime;
        if (timeDelay <= 0)
        {
            if (VFX)
            {
                VFX.SetActive(true);
            }

            GameObject T = Instantiate(prefab, transform.position, transform.rotation);

            StartCoroutine(waiToHide());

            T.tag = nametag;
            timeDelay = timeDelayClone;
            sum++;

            if (sum >= numberFire)
            {
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator waiToHide()
    {
        yield return new WaitForSeconds(0.3f);
        if (VFX)
        {
            VFX.SetActive(false);
        }
        yield return 0;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }
}
