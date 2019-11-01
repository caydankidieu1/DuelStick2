using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeControl : MonoBehaviour
{
    public HingeJoint2D hinge;
    public Pull pull;

    [Header("get and set value Angle limit")]
    public float controllMin;
    public float controllMax;
    public float newhingeMin;
    public float newhingeMax;

    [Header("Controller new")]
    public bool activel;
    private float CloneMin;
    private float CloneMax;
    [Header("Test Controller")]
    public bool activel2;

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useLimits = true;

        CloneMin = newhingeMin;
        CloneMax = newhingeMax;
    }

    // Update is called once per frame
    void Update()
    {
        controlH2();
        controlH();
    }

    public void controlH() // focus hand and foot
    {
        if (activel2 == false)
        {
            if (pull.InputX != 0 || pull.InputZ != 0)
            {
                hinge.useLimits = true;
                var l = hinge.limits;
                l.min = newhingeMin + controllMin;
                l.max = newhingeMax + controllMax;
                hinge.limits = l;
            }
            else if (pull.InputX == 0 || pull.InputZ == 0)
            {
                hinge.useLimits = true;
                var l = hinge.limits;
                l.min = newhingeMin + controllMin;
                l.max = newhingeMax + controllMax;
                hinge.limits = l;
            }
        }
    }

    public void controlH2() // focus hand and foot
    {
        if (activel2)
        {
            if (pull.InputXP1 != 0 || pull.InputZP1 != 0)
            {
                if (CloneMin < 0 && CloneMax < 0)
                {
                    if (newhingeMin > CloneMax)
                    {
                        newhingeMin -= Time.deltaTime * 100 * 2;
                    }
                    else
                    {
                        newhingeMin = CloneMin;
                    }

                    var l = hinge.limits;
                    l.min = newhingeMin;
                    l.max = newhingeMin;
                    hinge.limits = l;
                }

                if (CloneMin > 0 && CloneMax > 0)
                {
                    if (newhingeMin < CloneMax)
                    {
                        newhingeMin += Time.deltaTime * 100  * 2;
                    }
                    else
                    {
                        newhingeMin = CloneMin;
                    }

                    var l = hinge.limits;
                    l.min = newhingeMin;
                    l.max = newhingeMin;
                    hinge.limits = l;
                }
            }
            else if (pull.InputXP1 == 0 && pull.InputZP1 == 0)
            {
                hinge.useLimits = true;
                var l = hinge.limits;
                l.min = CloneMin + controllMin;
                l.max = CloneMax + controllMax;
                hinge.limits = l;
            }
        }     
    }
}
