using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;

    [Header("Movement to Point")]
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;

    private Vector2 pos;
    private Transform test;

    private void Start()
    {
        test = Point1.transform;
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime * 100);

        pos = transform.position - test.position;
        if ((int)transform.position.x == (int)Point1.transform.position.x && (int)transform.position.y == (int)Point1.transform.position.y)
        {
            test = Point2.transform;
        }
        else if ((int)transform.position.x == (int)Point2.transform.position.x && (int)transform.position.y == (int)Point2.transform.position.y)
        {
            test = Point3.transform;
        }
        else if ((int)transform.position.x == (int)Point3.transform.position.x && (int)transform.position.y == (int)Point3.transform.position.y)
        {
            test = Point4.transform;
        }
        else if ((int)transform.position.x == (int)Point4.transform.position.x && (int)transform.position.y == (int)Point4.transform.position.y)
        {
            test = Point1.transform;
        }

        var distance = pos.magnitude;
        var direction = pos / distance;

        transform.position += new Vector3(-direction.x, -direction.y, 0) * Time.deltaTime * 5;
    }
}

