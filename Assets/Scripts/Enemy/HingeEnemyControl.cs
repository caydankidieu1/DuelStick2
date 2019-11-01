using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeEnemyControl : MonoBehaviour
{
    public HingeJoint2D hinge;

    [Header("get and set value Angle limit")]
    public float controllMin;
    public float controllMax;
    public float newhingeMin;
    public float newhingeMax;


    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useLimits = true;
    }

    // Update is called once per frame
    void Update()
    {
        controlH();
    }

    public void controlH() // focus hand and foot
    {
        hinge.useLimits = true;
        var l = hinge.limits;
        l.min = newhingeMin + controllMin;
        l.max = newhingeMax + controllMax;
        hinge.limits = l;
    }
}
