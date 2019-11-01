using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    public GameObject Fire;
    public ParticleSystem FireFront;
    public ParticleSystem FireBack;

    [Range(1, 10)]
    public float TimeToActive;
    public float TimeToHide;
    private float CloneTime;
    // Start is called before the first frame update
    void Start()
    {
        Fire.SetActive(true);
        CloneTime = TimeToActive;
    }

    // Update is called once per frame
    void Update()
    {
        TimeToActive -= Time.deltaTime;

        if (TimeToActive <= 0)
        {
            Fire.SetActive(false);
            StartCoroutine(wait(TimeToHide));
          
        }
    }

    IEnumerator wait(float TimeToHide)
    {
        yield return new WaitForSeconds(TimeToHide);
        TimeToActive = CloneTime;
        Fire.SetActive(true);
        yield return 0;
    }
}
