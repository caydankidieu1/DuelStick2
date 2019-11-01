using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Package", menuName = "Package")]
public class Package : ScriptableObject
{
    [TextArea(10, 10)]
    public string namePackage;
    public Cotsume[] cotsume;
}
