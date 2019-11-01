using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PackageStore", menuName = "New Packages Store")]
public class PackageStore : ScriptableObject
{
    public string id;
    public string name;
    [TextArea(5,5)]
    public string description;
    public float cost;
    public string namePackage;
    public Sprite icon;
    public Weapon[] weapons;
    public Cotsume[] skins;
    [Space]
    public bool checkBuy;
}
