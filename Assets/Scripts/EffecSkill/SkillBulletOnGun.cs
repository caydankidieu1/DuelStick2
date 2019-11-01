using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletOnGun : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float force = 200;
    public float timeToDestroy = 2f;
    public GameObject prefab;

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.AddForce(transform.right * force * 10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        //rigi.velocity = (transform.right * force * Time.deltaTime * 5);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    void Boom()
    {
        if (prefab)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if ((gameObject.tag == "skillE" && col.collider.tag == "skillE") || (gameObject.tag == "skillP" && col.collider.tag == "skillP")
            || (gameObject.tag == "skillE" && col.collider.tag == "enemy") || (gameObject.tag == "skillP" && col.collider.tag == "player")
            || (gameObject.tag == "skillE" && col.collider.tag == "weaponEnemy") || (gameObject.tag == "skillP" && col.collider.tag == "weapon"))
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
