using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Setting", menuName = "setting")]
public class Setting : ScriptableObject
{
    public bool sounds;
    public bool vibration;
    public bool leftController;
}
