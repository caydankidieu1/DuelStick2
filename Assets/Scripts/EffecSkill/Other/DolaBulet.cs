using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolaBulet : MonoBehaviour
{
    public float force;
    public float timeToDestroy = 1f;
    public GameObject prefabFX;
    public Rigidbody2D rigi;

    private void Start()
    {
        if (gameObject.tag == "skillP")
        {
            gameObject.layer = 8;
        }
        else if (gameObject.tag == "skillE")
        {
            gameObject.layer = 10;
        }
        rigi.AddForce(transform.right * force * 10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        //rigi.velocity = (transform.right * force * Time.deltaTime * 10);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Boom();
        Destroy(gameObject);
        yield return 0;
    }
    void Boom()
    {
        Instantiate(prefabFX, transform.position, Quaternion.identity);
    }
    IEnumerator des()
    {
        yield return new WaitForSeconds(0.2f);
        Boom();
        Destroy(gameObject);
        yield return 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((gameObject.tag == "skillE" && col.collider.tag == "skillE") || (gameObject.tag == "skillP" && col.collider.tag == "skillP"))
        {
            Debug.Log("yes");
        }
        else if (!col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE"))
        {
            Boom();
            Destroy(gameObject);
        }
    }
}
