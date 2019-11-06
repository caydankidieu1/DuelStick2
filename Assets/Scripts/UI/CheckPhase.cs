using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPhase : MonoBehaviour
{
    [Header("Location Condition Star")]
    public Phase[] phase;
    public Button[] btnWaver;

    [Header("END LESS")]
    public Image btnEndLess;
    public bool endLess;
    public GameObject animBox;

    [Header("Color")]
    public Color defaultColor;
    public Color selectColor;

    [Header("Controller")] public ControllerAll1P controllerAll;


    void Start()
    {
        phase = GetComponentsInChildren<Phase>();
    }
    void Update()
    {
        checkPlayed();

        if (endLess)
        {
            animBox.SetActive(true);
            btnEndLess.color = selectColor;
        }
        else
        {
            animBox.SetActive(false);
            btnEndLess.color = defaultColor;
        }
    }

    public void checkPlayed()
    {
        for (int i = 0; i < phase.Length; i++)
        {
            for (int k = 1; k < phase.Length; k++)
            {
                if (phase[i].IsStar >= 1 && phase[k].IsStar == 0)
                {
                    if (!((i + 1) >= phase.Length))
                    {
                        phase[i + 1].checkNext = true;
                    }
                }
                else
                {
                    phase[0].checkNext = true;
                }
            }     
        }
    }
    public void resetColor()
    {
        for (int i = 0; i < btnWaver.Length; i++)
        {
            btnWaver[i].GetComponent<Phase>().activelNormal();
        }
    }

    public void activelEndLess()
    {
        endLess = !endLess;

        for (int i = 0; i < phase.Length; i++)
        {
            phase[i].endLess = endLess;
            phase[i].controllerAll = controllerAll;
        }
    }
}
