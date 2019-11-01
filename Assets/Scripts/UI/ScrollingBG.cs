using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    public float scrollSpeed = -5f;
    public float lengToRepeat = 20;
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, lengToRepeat);
        transform.position = startPos + Vector2.right * newPos;
    }
}
