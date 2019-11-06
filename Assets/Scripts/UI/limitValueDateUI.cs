using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class limitValueDateUI : MonoBehaviour
{
    public InputField input;
    private int testValue;
    public int min;
    public int max;
    public bool activelCheckYear;

    public void changeValue()
    {
        if (input.text != "")
        {
            if (activelCheckYear)
            {
                max = DateTime.Now.Year;
                min = max - 100;
            }
            testValue = int.Parse(input.text);
            testValue = Mathf.Clamp(testValue, min, max);

            input.text = testValue.ToString();

            Debug.Log("In here");
        }
    
    }
}
