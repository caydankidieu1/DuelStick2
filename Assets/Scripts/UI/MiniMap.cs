using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public Camera camera;

    [Header("VALUE")]
    public float max = 50;
    public float min = 35;

    private void LateUpdate()
    {
        if (player)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }

    public void reductionMiniMap()
    {
        var test = camera.orthographicSize;
        test++;
        if (test > max)
        {
            test = max;
        }
        else if (test < min)
        {
            test = min;
        }
        camera.orthographicSize = test;
    }

    public void increaseMiniMap()
    {
        var test = camera.orthographicSize;
        test--;
        if (test > max)
        {
            test = max;
        }
        else if (test < min)
        {
            test = min;
        }
        camera.orthographicSize = test;
    }
}
