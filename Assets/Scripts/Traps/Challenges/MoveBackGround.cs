using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    public float speedVelocityX =  0.1f;
    public float localWantLoopX = 20f;
    public float timeDelay = 2f;
    private Vector3 localOrigin;
    private bool checkMove;

    void Start()
    {
        checkMove = true;
        localOrigin = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkMove)
        {
            transform.Translate(Vector2.left * speedVelocityX);
        }

        if (transform.position.x <= localWantLoopX)
        {
            checkMove = false;
            transform.position = localOrigin;
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeDelay);
        checkMove = true;
        yield return 0;
    }
}
