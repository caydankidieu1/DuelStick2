using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public _Muscle[] Muscles;

    private void FixedUpdate()
    {
        foreach (_Muscle muscle in Muscles)
        {
            muscle.activeMuscle();
        }
    }

    [System.Serializable]
    public class _Muscle
    {
        public Rigidbody2D rigi;
        public float restRotate;
        public float force;

        public void activeMuscle ()
        {
            rigi.MoveRotation(Mathf.LerpAngle(rigi.rotation, restRotate, force * Time.fixedDeltaTime));
        }
    }
}
