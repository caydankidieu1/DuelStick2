using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerData
{
    public List<power> P = new List<power>();

    public PowerData(PowerUI powerUI)
    {
        P.Clear();
        for (int i = 0; i < powerUI.power.Length; i++)
        {
            power test = new power();
            test.id = powerUI.power[i].id;
            test.number = powerUI.power[i].amount;

            P.Add(test);
        }
    }
}

[System.Serializable]
public class power
{
    public string id;
    public int number;
}
