using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLaser : MonoBehaviour
{
    public GameObject Laser;
    public Transform firePoint;
    public ParticleSystem VFX;
    public float timeCD = 5f;
    public float timeExist = 1f;
    public float timeClone;

    void Start()
    {
        timeClone = timeCD;
        Laser.SetActive(false);
        VFX.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeCD -= Time.deltaTime;

        if (timeCD <= 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        timeCD = timeClone;

        if (VFX.gameObject.activeSelf == false)
        {
            VFX.gameObject.SetActive(true);
        }

        VFX.Play();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeExist);
        Laser.SetActive(true);
        yield return new WaitForSeconds(timeExist);
        Laser.SetActive(false);
        yield return 0;
    }
}
