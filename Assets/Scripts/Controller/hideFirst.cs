using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideFirst : MonoBehaviour
{
    public GameObject[] mode;

    void Start()
    {
        for (int i = 0; i < mode.Length; i++)
        {
            if (mode.Length > 0)
            {
                mode[0].SetActive(true);
            }
            else
            {
                mode[i].SetActive(false);
            }
        }
    }
}
