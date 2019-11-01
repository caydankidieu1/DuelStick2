using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkinScytheEffect : MonoBehaviour
{
    public float scaleWeapon = 2;
    public float scaleWant = 2;
    public float forceRotation = 2;
    private float AxisZ;
    private Vector3 test;
    public float timeLife = 1;

    [Header("Clone")]
    public Collider2D mainColl;
    public GameObject weaponClone;

    private Audio audioManager;
    public string nameSound;
    private float cloneTimeVirtual = 3;

    private void Start()
    {
        audioManager = Audio.instance;

        weaponClone.tag = gameObject.tag;
        StartCoroutine(wait());
    }

    void Update()
    {
        if (transform.localScale.x < scaleWant)
        {
            transform.localScale += transform.localScale * scaleWeapon * Time.deltaTime;
        }

        if (transform.localScale.x >= scaleWant)
        {
            if (cloneTimeVirtual > 0)
            {
                StartCoroutine(delay());
                cloneTimeVirtual = -10;
            }
            AxisZ += forceRotation * Time.deltaTime;
            test = new Vector3(0, 0, AxisZ);
            transform.Rotate(test);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        audioManager.PlaySound(nameSound);
        yield return 0;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeLife);
        Destroy(gameObject);
        yield return 0;
    }
}
