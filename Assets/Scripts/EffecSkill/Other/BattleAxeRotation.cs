using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAxeRotation : MonoBehaviour
{
    public float duration = 0.5f;
    public float timeToStop = 1f;

    private void Update()
    {
        timeToStop -= Time.deltaTime;
        if (timeToStop >= 0)
        {
            transform.Rotate(Vector3.forward, duration * Time.deltaTime * 10);
        }
    }

}
