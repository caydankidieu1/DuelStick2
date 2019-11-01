using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Power", menuName ="Power")]
public class Power : ScriptableObject
{
    public string id;
    public string namePower;
    public Sprite iconPower;
    public int amount;
    public int maxAmount;
    public string description;
    public int cost;
}
