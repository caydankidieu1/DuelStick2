using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string id;
    public int number;
    public int numberMax;
    public int present;
    [Space]
    public int nowDay;
    [Space]
    public Sprite iconQuest;
    [Space]
    public bool checkClaim;
    public bool checkReceived;

    [TextArea(5,5)]
    public string Description;
}
