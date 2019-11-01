using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotation : MonoBehaviour
{
    public Transform follow;
    public float speed;
    private void Update()
    {
        if (follow)
        {
            transform.position = follow.transform.position;
            transform.Rotate(Vector3.back, speed * Time.deltaTime * 100);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
