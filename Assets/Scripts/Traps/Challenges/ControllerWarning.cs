using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerWarning : MonoBehaviour
{
    public Image image;
    public float speedToChange = 1f;
    private float time;
    public Color color1;
    public Color color2;

    void Update()
    {
        time += Time.deltaTime * speedToChange;

        if ((int)time % 2 == 0)
        {
            image.color = color1;
        }
        else
        {
            image.color = color2;
        }
    }
}
