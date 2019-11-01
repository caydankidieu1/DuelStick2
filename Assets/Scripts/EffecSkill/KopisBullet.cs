using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopisBullet : MonoBehaviour
{
    public float speed;
    public float force;
    public float timeToDestroy = 2f;

    public Rigidbody2D rigi;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime * 10);

        rigi.velocity = (transform.right * force * Time.deltaTime * 100);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "skillP" && col.collider.CompareTag("player"))
        {
            gameObject.layer = 8;
        }

        if (gameObject.tag == "skillE" && col.collider.CompareTag("enemy"))
        {
            gameObject.layer = 10;
        }

        if (col.collider.CompareTag("ground"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
