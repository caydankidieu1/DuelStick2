using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoxDeath : MonoBehaviour
{
    public float speed = 5;
    public bool checkColl;
    public Rigidbody2D rigi;
    private Vector2[] allLocal = { Vector2.right, Vector2.left, Vector2.up, Vector2.down , new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    [SerializeField] private Vector2 Dir;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        Dir = allLocal[Random.Range(0, allLocal.Length)];
    }

    void Update()
    {
        rigi.velocity = Dir * speed * 10 * Time.deltaTime;

        if (checkColl)
        {
            Dir = allLocal[Random.Range(0, allLocal.Length)];
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "ground" || col.collider.tag == "sky" || col.collider.tag == "deathZone" || col.collider.tag == "trap")
        {
            checkColl = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "ground" || col.collider.tag == "sky" || col.collider.tag == "deathZone" || col.collider.tag == "trap")
        {
            checkColl = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "ground" || col.collider.tag == "sky" || col.collider.tag == "deathZone" || col.collider.tag == "trap")
        {
            checkColl = false;
        }
    }
}
