using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public GameObject[] bullets;

    private void Awake()
    {
        foreach (GameObject item in bullets)
        {
            item.tag = gameObject.tag;
            item.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        foreach (GameObject item in bullets)
        {
            item.tag = gameObject.tag;
            item.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }
}
