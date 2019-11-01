using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMeteos : MonoBehaviour
{
    public GameObject warning;
    public float timeToWarning = 5;
    public float timeExist = 1f;
    private float timeClone;

    [Range(1, 5)]
    public int numberMeteos;
    private int numberClone;
    public GameObject MeteosPrefab;
    public Transform FirePoint;

    void Start()
    {
        warning.SetActive(false);
        timeClone = timeToWarning;
    }

    // Update is called once per frame
    void Update()
    {
        timeToWarning -= Time.deltaTime;

        if (timeToWarning <= 0)
        {
            warning.SetActive(true);
            timeToWarning = timeClone;
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeExist);
        warning.SetActive(false);
        MeteoIsfalls();
        yield return 0;
    }

    public void MeteoIsfalls()
    {
        numberClone = Random.Range(1, numberMeteos);
        for (int i = 0; i < numberClone; i++)
        {
            GameObject T = Instantiate(MeteosPrefab, FirePoint.position, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        warning.SetActive(false);
    }

    private void OnDisable()
    {
        warning.SetActive(false);
    }
}
