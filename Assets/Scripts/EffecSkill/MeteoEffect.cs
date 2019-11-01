using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoEffect : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public GameObject parent;
    public GameObject prefab;

    public float timeToDestoy;
    public float force;
    public Rigidbody2D rigi;

    private Vector2 pos;
    private Vector3 test;

    public float minRandomX;
    public float maxRandomX;

    private void Awake()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
        StartPoint.transform.position = new Vector3(0, 40, 0);

        transform.position = StartPoint.transform.position;
        gameObject.tag = parent.tag;

        EndPoint.transform.position = new Vector3(EndPoint.transform.position.x + (Random.Range(minRandomX, maxRandomX)),
            EndPoint.transform.position.y,
            EndPoint.transform.position.z); 
    }
    private void Start()
    {
    
        if (gameObject.tag == "skillP" || gameObject.tag == "skillE")
        {
            gameObject.layer = 14;
        }
    }

    private void FixedUpdate()
    {
        
        pos = transform.position - EndPoint.transform.position;
        var distance = pos.magnitude;
        var direction = pos / distance;
        //transform.Translate(-direction * Time.deltaTime * force);
        rigi.AddForce(-direction * force * Time.deltaTime * 100);
    }

    private void Update()
    {
        timeToDestoy -= Time.deltaTime;
        if (timeToDestoy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Boom()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        /* if (!col.collider.CompareTag("sky") || !col.collider.CompareTag("skillP") || !col.collider.CompareTag("skillE"))
         {
             Boom();
             Destroy(gameObject);
         }*/

        if (col.collider.CompareTag("sky") || col.collider.CompareTag("skillP") || col.collider.CompareTag("skillE"))
        {

        }
        else
        {
            Boom();
            Destroy(gameObject);
        }
    }
}
