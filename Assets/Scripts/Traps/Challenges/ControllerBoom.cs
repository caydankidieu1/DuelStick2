using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBoom : MonoBehaviour
{
    public float timeToExplore = 30f;
    public GameObject[] Clock;
    public GameObject VFXExplore;
    public Rigidbody2D rigi;
    public SpriteRenderer sprite;
    public GameObject deathZone;
    private Collider2D col2d;
    private float timeClone;
    private float timeCLone2;
    private float timeClone3;
    private int countNumber;
    private bool checkExplore;

    
    void Start()
    {
        countNumber = 0;
        for (int i = 0; i < Clock.Length; i++)
        {
            Clock[i].SetActive(false);
        }
        timeClone = timeToExplore / Clock.Length;
        timeCLone2 = timeToExplore - timeClone;
        timeClone3 = timeToExplore;
        col2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timeToExplore -= Time.deltaTime;
        timeCLone2 = timeClone3 - (countNumber * timeClone);

        if (timeToExplore <= timeCLone2 && !checkExplore)
        {
            if (countNumber < Clock.Length)
            {
                //timeCLone2 = timeClone3 - (countNumber * timeClone);
                Clock[countNumber].SetActive(true);
                countNumber++;
            }
        }

        if (countNumber >= Clock.Length && !checkExplore)
        {
            Boom();
        }
    }

    void Boom()
    {
        col2d.isTrigger = true;
        rigi.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.tag = "deathZone";
        if (VFXExplore && !checkExplore)
        {
            for (int i = 0; i < Clock.Length; i++)
            {
                Clock[i].SetActive(false);
            }

            sprite.enabled = false;
            Instantiate(VFXExplore, transform.position, transform.rotation);
            GameObject T = Instantiate(deathZone, transform.position, transform.rotation);
            T.transform.SetParent(this.transform);
            
            checkExplore = true;

        }
        //Destroy(gameObject, 5f);
    }
}
