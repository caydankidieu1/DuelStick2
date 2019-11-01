using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cotsume", menuName = "Cotsume")]
public class Cotsume : ScriptableObject
{
    [Header("value")]
    public int id;
    public int HP;
    public int maxHPCotsume;
    public int cost;
    public int levelUnlock;
    public float rateLuckySpin;
    public string nameCotsume;
    public bool checkBuy;
    public bool checkUse;
    public bool checkUseP1;
    public bool checkUseP2;

    [Header("Sprite icon")]
    public Sprite icon;
    public Sprite picture;

    [Header("Prefab")]
    public Sprite Head;
    public Sprite Chest;
    public Sprite Hip;

    [Header("Prefab Arm")]
    public Sprite UpperArm_R;
    public Sprite LowerArm_R;
    public Sprite UpperArm_L;
    public Sprite LowerArm_L;

    [Header("Prefab Leg")]
    public Sprite UpperLeg_R;
    public Sprite LowerLeg_R;
    public Sprite UpperLeg_L;
    public Sprite LowerLeg_L;
}
