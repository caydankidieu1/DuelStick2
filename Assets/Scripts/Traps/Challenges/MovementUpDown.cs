using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUpDown : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float speed;
    public float NumberUp;
    public float NumberDown;
    [SerializeField]private Vector2 local;
    [Range(1, 2)]
    public int type;
    private Vector2 test;

    private void Start()
    {
        local.x = transform.position.y + NumberUp;
        local.y = transform.position.y - NumberDown;

        switch (type)
        {
            case 1:
                test = Vector2.up;
                break;
            case 2:
                test = Vector2.down;
                break;
            default:
                test = Vector2.up;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ((test == Vector2.up && transform.position.y >= local.x) || (test == Vector2.down && transform.position.y <= local.y))
        {
            test *= -1;
        }

        rigi.velocity = test * speed * 10 * Time.deltaTime;
    }
}
