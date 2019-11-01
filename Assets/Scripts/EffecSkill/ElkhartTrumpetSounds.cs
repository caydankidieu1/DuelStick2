using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkhartTrumpetSounds : MonoBehaviour
{
    public float x, y, z;

    public Rigidbody2D rigi;
    public float force = 200;
    public float timeToDestroy = 3.5f;
    [Header("clone")]
    public float timeToHide;
    public float duration = 0.5f;
    private float timeToattack = 0.2f;
    public SpriteRenderer clone;
    public SpriteRenderer clone1;
    public SpriteRenderer clone2;

    private void Start()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;

        clone = GetComponent<SpriteRenderer>();
        StartCoroutine(wait());
        timeToHide = (timeToDestroy * 2) / 3;
    }


    // Update is called once per frame
    void Update()
    {
        //rigi.AddForce(transform.right * force * Time.deltaTime * 100);
        rigi.velocity = (transform.right * force * Time.deltaTime * 10);

        x += Time.deltaTime * 2;
        y += Time.deltaTime * 2;
        z += Time.deltaTime * 2;

        transform.localScale = new Vector3(x, y, z);

        timeToHide -= Time.deltaTime;

        if (timeToHide <= 0)
        {
            StartCoroutine(test());
        }
    }

    public IEnumerator test()
    {
        float start = 1;
        float end = 0;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;

            clone.color = new Color(255, 255, 255, (float)Mathf.Lerp(start, end, progress));
            clone1.color = new Color(255, 255, 255, (float)Mathf.Lerp(start, end, progress));
            clone2.color = new Color(255, 255, 255, (float)Mathf.Lerp(start, end, progress));
            yield return null;
        }


        clone.color = new Color(255, 255, 255, 0f);
        clone1.color = new Color(255, 255, 255, 0f);
        clone2.color = new Color(255, 255, 255, 0f);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "skillP" && col.collider.CompareTag("weapon"))
        {
            gameObject.layer = 8;
        }

        if (gameObject.tag == "skillE" && col.collider.CompareTag("weaponEnemy"))
        {
            gameObject.layer = 10;
        }

        if (gameObject.tag == "skillP" && col.collider.CompareTag("player"))
        {
            gameObject.layer = 8;
        }

        if (gameObject.tag == "skillE" && col.collider.CompareTag("enemy"))
        {
            gameObject.layer = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        timeToattack -= Time.deltaTime;
        if (timeToattack <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
