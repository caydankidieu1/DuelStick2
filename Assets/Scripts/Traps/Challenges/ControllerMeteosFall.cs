using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMeteosFall : MonoBehaviour
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
    private Vector2 direction;

    private void Awake()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
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

        pos = transform.position - EndPoint.transform.position;
        var distance = pos.magnitude;
        direction = pos / distance;
    }

    private void FixedUpdate()
    {
      
        rigi.velocity = (-direction * force * Time.deltaTime * 100);
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

        if (col.collider.CompareTag("sky") || col.collider.CompareTag("skillP") || col.collider.CompareTag("skillE") || col.collider.CompareTag("trap") ||
            col.collider.CompareTag("weapon") || col.collider.CompareTag("weaponEnemy"))
        {

        }
        else
        {
            Boom();
            Destroy(gameObject);
        }
    }
}
