using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTrapCreates : MonoBehaviour
{
    public GameObject prefab;
    public Transform FirePoint;
    public float timeToCreate = 6;
    public float timeClone;
    public bool checkCreate;

    private void Start()
    {
        timeClone = timeToCreate;
        timeToCreate = 0;
        checkCreate = true;
    }
    private void Update()
    {
        if (checkCreate)
        {
            timeToCreate -= Time.deltaTime;

            if (timeToCreate <= 0)
            {
                CreateNew();
            }
        }
    }

    public void CreateNew()
    {
        GameObject T = Instantiate(prefab, FirePoint.position, Quaternion.identity);

        T.gameObject.transform.SetParent(this.transform);

        if (T.GetComponent<ViewForce>() || T.GetComponent<ViewForce2>())
        {
            if (T.GetComponent<ViewForce>())
            {
                var child = T.GetComponent<ViewForce>();
                child.controller = this;
            }

            if (T.GetComponent<ViewForce2>())
            {
                var child = T.GetComponent<ViewForce2>();
                child.controller = this;
            }
        }
        timeToCreate = timeClone;
        checkCreate = false;
    }
}
