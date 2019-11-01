using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw2 : MonoBehaviour
{
    public float speed;

    [Header("Movement to Point")]
    public Transform[] Point;

    private Vector2 pos;
    private Transform test;

    private void Start()
    {
        test = Point[0].transform;
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime * 100);

        pos = transform.position - test.position;

        for (int i = 0; i < Point.Length; i++)
        {
            if ((int)transform.position.x == (int)Point[i].position.x && (int)transform.position.y == (int)Point[i].position.y)
            {
                if ((i+1) == Point.Length)
                {
                    test = Point[0].transform;
                }
                else
                {
                    test = Point[i + 1].transform;
                }
            }
        }

        var distance = pos.magnitude;
        var direction = pos / distance;

        transform.position += new Vector3(-direction.x, -direction.y, 0) * Time.deltaTime * 5;
    }
}
