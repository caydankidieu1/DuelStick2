using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nails : MonoBehaviour
{
    public float timeToActive;

    [Header("1: down, 2: right, 3: left, 4: up")]
    [Range(1,4)]
    public int typeNail;

    public Vector3 testUp;
    public Vector3 testRight;
    public GameObject StartPoint;
    public GameObject EndPoint;
    public Vector3 pos;

    private void Start()
    {
        testUp = new Vector3(0, -1, 0);
        testRight = new Vector3(-1, 0, 0);
        pos = transform.position;
    }

    private void Update()
    {
        active(typeNail); 
    }

    void active(int type)
    {
        switch (type)
        {
            case 1:
                activeNailDown();
                break;
            case 2:
                activeNailRight();
                break;
            case 3:
                activeNailLeft();
                break;
            case 4:
                activeNailTop();
                break;
            default:
                break;
        }
    }

    void activeNailDown()
    {
        transform.Translate(testUp * Time.deltaTime);
        if (transform.position.y >= pos.y + 2f)
        {
            testUp = Vector3.down;
        }

        if (transform.position.y <= pos.y - 0.1f)
        {
            testUp = Vector3.up;
        }
    }

    void activeNailRight()
    {
        transform.Translate(testRight * Time.deltaTime);
        if (transform.position.x <= pos.x - 0.1f)
        {
            if (testRight != Vector3.right)
            {
                testRight = Vector3.right;
            }
        }

        if (transform.position.x >= pos.x + 2f)
        {
            if (testRight != Vector3.left)
            {
                testRight = Vector3.left;
            }
        }
    }

    void activeNailLeft()
    {
        transform.Translate(testRight * Time.deltaTime);
        if (transform.position.x <= pos.x - 2f)
        {
            if (testRight != Vector3.right)
            {
                testRight = Vector3.right;
            }
        }

        if (transform.position.x >= pos.x + 0.1f)
        {
            if (testRight != Vector3.left)
            {
                testRight = Vector3.left;
            }
        }
    }

    void activeNailTop()
    {
        transform.Translate(testUp * Time.deltaTime);
        if (transform.position.y >= pos.y + 0.1f)
        {
            testUp = Vector3.down;
        }

        if (transform.position.y <= pos.y - 2f)
        {
            testUp = Vector3.up;
        }
    }
}
