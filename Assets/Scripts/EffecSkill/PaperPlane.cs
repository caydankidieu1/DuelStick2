using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : MonoBehaviour
{
    public float newForce = 10;
    public float NewspeedRotation;
    public float NewTimeToChange = 0.5f;
    public float NewTimeToStopRotate = 1f;
    private Vector3 NewVector;
    [SerializeField]private int choice;

    [Space]
    public float timeToDestroy = 2f;
    public float forceToMove;
    float timeCouter = 0;

    public float speedRotation;
    public float speed;
    public float width;
    public float height;

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    [SerializeField] private Rigidbody2D rigi;
    [SerializeField] private float timeToChange;
    [SerializeField] private bool activelMove;

    public GameObject prefab;

    private void Start()
    {
        NewTimeToStopRotate = Random.Range(1f, NewTimeToStopRotate);
        choice = Random.Range(0, 2);
        Debug.Log(choice);
    }

    void Update()
    {
        activelPaperPlane();

        StartCoroutine(wait());
    }

    void activelPaperPlane()
    {
        NewTimeToChange -= Time.deltaTime;
        if (NewTimeToChange <= 0)
        {
            if (choice == 0)
            {
                transform.Rotate(-Vector3.back, NewspeedRotation * Time.deltaTime * 50);
            }
            else if (choice == 1)
            {
                transform.Rotate(Vector3.back, NewspeedRotation * Time.deltaTime * 50);
            }
            
            NewTimeToStopRotate -= Time.deltaTime;
            if (NewTimeToStopRotate <= 0)
            {
                NewspeedRotation = 0;
            }
        }
        
        rigi.velocity = (transform.right * Time.deltaTime * newForce * 50);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    IEnumerator waitToChange()
    {
        yield return new WaitForSeconds(2f);
        timeToChange = 0;
        rigi.bodyType = RigidbodyType2D.Dynamic;
        activelMove = true;
        yield return 0;
    }

    IEnumerator destroyG()
    {
        yield return new WaitForSeconds(0.3f);
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
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
