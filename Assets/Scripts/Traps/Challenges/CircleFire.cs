using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    public bool activelScale;
    public float speedScale = .1f;
    public bool activelRoation;
    public float speedRoation = 1f;
    private float timeToStop = 3.5f;


    private void Start()
    {
        StartCoroutine(wait());
    }
    // Update is called once per frame
    void Update()
    {
        if (activelScale)
        {
            var x = transform.localScale.x + speedScale * Time.deltaTime;
            var y = transform.localScale.y + speedScale * Time.deltaTime;
            var z = transform.localScale.z + speedScale * Time.deltaTime;

            transform.localScale = new Vector3(x,y,z); 
        }

        if (activelRoation)
        {
            transform.Rotate(Vector3.back * speedRoation * 100);
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToStop);
        activelScale = false;
        Destroy(gameObject);
        yield return 0;
    }
}
