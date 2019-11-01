using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Info and Value")]
    public string id;
    public string nameWeapon;
    public EquipmentSlot equipSlots;
    [Space]
    public float damage;
    public float maxdamage;
    [Space]
    public float skillDamage;
    public float maxskillDamage;
    public string description;
    [Space]
    public int timesToCD;
    public float countDownTime;
    [Space]
    public float forceKnockback;
    [Space]
    public Sprite Sprite;
    public Sprite SpriteStore;
    public int cost;
    public int levelUnlock;
    public bool checkVideoUnlock;
    public int videoUnlock;
    public float RateLucky;

    public bool checkBuy;
    public bool checkUse;
    public bool checkUseP1;
    public bool checkUseP2;

    [Header("Transform")]
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 Scale;

    [Header("Prefab")]
    public GameObject weaponPrefab;
    //public GameObject skillPrefab;

    [Header("--------------------------------------- 2 hands ----------------------")]
    public bool checkTwoHands;
    public Vector3 position2;
    public Vector3 rotation2;
    public Vector3 Scale2;
    [Space]
    public GameObject weaponPrefabClone;
}

public enum EquipmentSlot
{
    Head, Hands, Legs
}