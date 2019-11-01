using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenManager : MonoBehaviour
{
    public delegate void EndOfLife();
    public static event EndOfLife EndOfLifeMethods;

    public static void ResetLife()
    {
        EndOfLifeMethods();
    }
}
