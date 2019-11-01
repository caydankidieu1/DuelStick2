using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public float InputX; //Left and Right Inputs
    public float InputZ; //Up and down Inputs
    public Camera cam; //Main camera (make sure tag is MainCamera)
    private Vector2 desiredMoveDirection;
    private Vector2 desiredMoveDirectionP2;

    public Joystick JS;
    public Joystick JS2;

    public float force = 70;
    public Rigidbody2D rigi;

    [Header("---------------------------- Controller PVP -------------------------------")]
    public Joystick P1;
    public float InputXP1; //Left and Right Inputs
    public float InputZP1; //Up and down Inputs
    [Space]
    public Joystick P2;
    public float InputXP2; //Left and Right Inputs
    public float InputZP2; //Up and down Inputs
    public Rigidbody2D rigiP2;

    [Header("-----------------------------------")]
    public GameObject cinemachine;

    private void Start()
    {
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        move2();
        move();

        moveP1();
        moveP2();
    }
    public void move()
    {
        // joystick Right and left 
        if (JS.Horizontal >= .2f)
        {
            InputX = force;
        }
        else if (JS.Horizontal <= -.2f)
        {
            InputX = -force;
        }
        else
        {
            InputX = 0;
        }

        //  joystick up and down
        if (JS.Vertical >= .2f)
        {
            InputZ = force / 9.5f;
        }
        else if (JS.Vertical <= -.2f)
        {
            InputZ = -force / 9.5f;
        }
        else
        {
            InputZ = 0;
        }

        //var camera = Camera.main;
        var forward = cam.transform.up;
        var right = cam.transform.right;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX; // sum vector2 Horizontal and vertical
        //Debug.Log(desiredMoveDirection);
     
        if (rigi)
        {
            if (InputZ != 0 || InputX != 0)
            {
                //rigi.velocity = (desiredMoveDirection * force * Time.deltaTime * 10);
                rigi.MovePosition(rigi.position + desiredMoveDirection * force * Time.fixedDeltaTime);
            }
        }

        if (desiredMoveDirection.x != 0 || desiredMoveDirection.y != 0)
        {
            StartCoroutine(waitToZoomCamera());
        }
        else
        {
           // cinemachine.SetActive(false);
        }
    }

    #region Skill
    public void BulletClone(Vector2 local)
    {
        desiredMoveDirection = local;

        if (rigi)
        {
            rigi.velocity = (desiredMoveDirection * force * Time.deltaTime * 80);
        }
    }
    public void BulletClone2(Vector2 local)
    {
        desiredMoveDirectionP2 = local;

        if (rigiP2)
        {
            rigiP2.velocity = (desiredMoveDirectionP2 * force * Time.deltaTime * 80);
        }
    }
    public void Bullet(Vector2 local)
    {
        desiredMoveDirection = local;

        if (rigi)
        {
            rigi.velocity = (desiredMoveDirection * force * Time.deltaTime * 60 * 2);
        }
    }
    public void Bullet2(Vector2 local)
    {
        desiredMoveDirectionP2 = local;

        if (rigiP2)
        {
            rigiP2.velocity = (desiredMoveDirectionP2 * force * Time.deltaTime * 60 * 2);
        }
    }
    #endregion
    public void move2()
    {
        // joystick Right and left 
        if (JS2.Horizontal >= .2f)
        {
            InputX = force;
        }
        else if (JS2.Horizontal <= -.2f)
        {
            InputX = -force;
        }
        else
        {
            InputX = 0;
        }

        //  joystick up and down
        if (JS2.Vertical >= .2f)
        {
            InputZ = force / 9.5f;
        }
        else if (JS2.Vertical <= -.2f)
        {
            InputZ = -force / 9.5f;
        }
        else
        {
            InputZ = 0;
        }

        var forward = cam.transform.up;
        var right = cam.transform.right;
        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX; 

        if (rigi)
        {
            if (InputZ != 0 || InputX != 0)
            {
                // rigi.velocity = (desiredMoveDirection * force * Time.deltaTime * 10);
                rigi.MovePosition(rigi.position + desiredMoveDirection * force * Time.fixedDeltaTime);
            }
        }


        if (desiredMoveDirection.x != 0 || desiredMoveDirection.y != 0)
        {
            StartCoroutine(waitToZoomCamera());
        }
        else
        {
           // cinemachine.SetActive(false);
        }
    }
    IEnumerator waitToZoomCamera()
    {
        yield return new WaitForSeconds(0.2f);
        cinemachine.SetActive(true);
    }

    public void moveP1()
    {
        // joystick Right and left 
        if (P1.Horizontal >= .2f)
        {
            InputXP1 = force;
        }
        else if (P1.Horizontal <= -.2f)
        {
            InputXP1 = -force;
        }
        else
        {
            InputXP1 = 0;
        }

        if (P1.Vertical >= .2f)
        {
            InputZP1 = force / 9.5f;
        }
        else if (P1.Vertical <= -.2f)
        {
            InputZP1 = -force / 9.5f;
        }
        else
        {
            InputZP1 = 0;
        }

        var forward = cam.transform.up;
        var right = cam.transform.right;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZP1 + right * InputXP1;
        //Debug.Log(desiredMoveDirection);

        if (rigi)
        {
            if (InputZP1 != 0 || InputXP1 != 0)
            {
                // rigi.velocity = (desiredMoveDirection * force * Time.deltaTime * 10);
                rigi.MovePosition(rigi.position + desiredMoveDirection * force * Time.fixedDeltaTime);
            }
        }

        if (desiredMoveDirection.x != 0 || desiredMoveDirection.y != 0)
        {
            StartCoroutine(waitToZoomCamera()); // zoom camera
        }
        else
        {
          //  cinemachine.SetActive(false);
        }
    }
    public void moveP2()
    {
        // joystick Right and left 
        if (P2.Horizontal >= .2f)
        {
            InputXP2 = force;
        }
        else if (P2.Horizontal <= -.2f)
        {
            InputXP2 = -force;
        }
        else
        {
            InputXP2 = 0;
        }

        if (P2.Vertical >= .2f)
        {
            InputZP2 = force / 9.5f;
        }
        else if (P2.Vertical <= -.2f)
        {
            InputZP2 = -force / 9.5f;
        }
        else
        {
            InputZP2 = 0;
        }

        var forward = cam.transform.up;
        var right = cam.transform.right;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirectionP2 = forward * InputZP2 + right * InputXP2;

        if (rigiP2)
        {
            if (InputZP2 != 0 || InputXP2 != 0)
            {
                //rigiP2.velocity = (desiredMoveDirectionP2 * force * Time.deltaTime * 10);
                rigiP2.MovePosition(rigiP2.position + desiredMoveDirectionP2 * force * Time.fixedDeltaTime);
            }
        }
    }

    
}
