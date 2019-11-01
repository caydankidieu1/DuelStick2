using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarnumRotation : MonoBehaviour
{
    public float speed;
    public float timeToDestroy = 2f;

    public Rigidbody2D rigi;

    private void Start()
    {
        rigi.bodyType = RigidbodyType2D.Kinematic;
    }
    private void Update()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
        yield return 0;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("weapon"))
        {
            gameObject.transform.SetParent(col.gameObject.transform);
        }
    }
}
