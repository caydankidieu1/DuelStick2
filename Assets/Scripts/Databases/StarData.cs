using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarData
{
    public List<int> Star = new List<int>();

    public StarData (StarController starController)
    {
        Star.Clear();
        for (int i = 0; i < starController.Phase.Length; i++)
        {
            Star.Add(starController.Phase[i].Star);
        }
    }
}
