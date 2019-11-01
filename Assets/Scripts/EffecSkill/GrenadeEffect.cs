using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffect : MonoBehaviour
{
    public Rigidbody2D rigi;
    public GameObject prefab;
    public string nameTag;
    public BoxCollider2D box1;
    public CircleCollider2D box2;
    public float force;
    public float timeToDestroyAffterBoom = 0.5f;
    public float timeToBoom = 2f;
    private SpriteRenderer sprite;

    private Audio audioManager;
    public string nameSource;

    private void Start()
    {
        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.Log("No Find any Sound");
        }

        nameTag = gameObject.tag;
        if (gameObject.tag == "skillP")
        {
            gameObject.layer = 8;
        }
        if (gameObject.tag == "skillE")
        {
            gameObject.layer = 10;
        }
        gameObject.tag = "Untagged";

        box1.enabled = true;
        box2.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timeToBoom -= Time.deltaTime;

        if (force > 0)
        {
            rigi.AddForce(transform.right * force * Time.deltaTime * 50);
            force -= Time.deltaTime;
        }
        

        if (timeToBoom <= 0)
        {
            Boom();
        }
    }

    void Boom()
    {
        if (prefab)
        {
            audioManager.PlaySound(nameSource);

            force = 0;
            gameObject.tag = nameTag;
            sprite.enabled = false;
            box1.enabled = false;
            box2.enabled = true;

            Instantiate(prefab, transform.position, Quaternion.identity);
            timeToBoom = 999;
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroyAffterBoom);
        Destroy(gameObject);
        yield return 0;
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
            force = 0;
        }
    }
}
