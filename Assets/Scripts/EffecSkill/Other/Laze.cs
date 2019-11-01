using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laze : MonoBehaviour
{
    public Transform FirePoint;
    public float length = 50;

    public GameObject prefab;
    public LayerMask layer;
    private LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void activeLaze(GameObject test)
    {
        StartCoroutine(Shoot(test));
    }

    IEnumerator Shoot(GameObject test)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.right, 100, layer);
        
        if (hitInfo)
        {
            //Instantiate(test, hitInfo.transform.position, Quaternion.identity);
            /*if (hitInfo.collider != null)
            {
                GameObject T =  Instantiate(test, hitInfo.transform.position, Quaternion.identity);
            }*/

            GameObject T = Instantiate(test, hitInfo.transform.position, Quaternion.identity);
            Debug.Log(hitInfo.transform.gameObject.name);
            AnimationControllerEnemy enemy = hitInfo.transform.GetComponent<AnimationControllerEnemy>();
            if (enemy != null)
            {
                enemy.takeDamge();
            }

            Debug.DrawLine(FirePoint.position, hitInfo.point, Color.red);

            Vector3 endPosition = transform.position + (transform.right * length);
            lr.SetPositions(new Vector3[] { transform.position, endPosition });
        }
        else
        {
            Vector3 endPosition = transform.position + (transform.right * length * 10);
            lr.SetPositions(new Vector3[] { transform.position, endPosition });
        }

        lr.enabled = true;

        yield return 0;

        lr.enabled = false;
    }
}
