using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLocot : MonoBehaviour
{
    public Transform Gear;
    public Rigidbody2D rigi;

    public bool checkMove;
    public float speedBar = 0.1f;
    public float speedGear = 0.1f;
    public float timeToDelay = 5f;

    [Range(1,2)]
    public int type;
    [SerializeField] private Vector2 test;

    void Start()
    {
        checkMove = true;
        switch (type)
        {
            case 1:
                test = Vector2.right;
                break;
            case 2:
                test = Vector2.left;
                break;
            default:
                break;
        }

        rigi.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkMove)
        {
            rigi.velocity = test * speedBar * 10 * Time.deltaTime;
            Gear.Rotate(Vector3.back * speedGear * 100);
        }
    }

    IEnumerator waitMove()
    {
        yield return new WaitForSeconds(timeToDelay);
        test *= -1;
        checkMove = true;
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);

        if (col.collider.tag == "ground")
        {
            Debug.Log("here");
            checkMove = false;
            StartCoroutine(waitMove());
        }
    }
}
