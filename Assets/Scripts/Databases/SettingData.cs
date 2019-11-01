using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData 
{
    public bool sounds;
    public bool Vibration;
    public bool leftC;

    public SettingData (SettingControllerUI setting)
    {
        sounds = setting.Sounds;
        Vibration = setting.Vibration;
        leftC = setting.LeftC;
    }
}
