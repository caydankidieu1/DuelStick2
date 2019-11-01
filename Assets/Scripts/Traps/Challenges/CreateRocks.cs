using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRocks : MonoBehaviour
{
    [Header("new")]
    public GameObject warning;
    public float timeExist = 1f;

    [Header("old")]
    public Transform FirePoint;
    public GameObject Rock;
    public float timeToCreate = 10f;
    public float timeToDestroy = 20f;
    private float TimeClone;

    private void Start()
    {
        TimeClone = timeToCreate;

        warning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeToCreate -= Time.deltaTime;

        if (timeToCreate <= 0)
        {
            CreateBegin();
            timeToCreate = TimeClone;
        }
    }

    void CreateBegin()
    {
        warning.SetActive(true);
        StartCoroutine(wait());

        GameObject T = Instantiate(Rock, FirePoint.transform.position, Quaternion.identity);
        T.transform.SetParent(gameObject.transform);
        Destroy(T, timeToDestroy);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeExist);
        warning.SetActive(false);
        yield return 0;
    }

    private void OnEnable()
    {
        if (warning)
        {
            warning.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (warning)
        {
            warning.SetActive(false);
        }
    }
}
