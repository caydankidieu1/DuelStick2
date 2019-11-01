using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new IAP", menuName = "IAP")]
public class IAP : ScriptableObject
{
    public string id;
    public string name;
    public Sprite icon;
    public int coins;
    public float cost;
    public string packageName;
}
