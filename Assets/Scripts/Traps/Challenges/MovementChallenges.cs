using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChallenges : MonoBehaviour
{
    public Vector2 localOrigin;
    void Start()
    {
        localOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = localOrigin;
    }
}
