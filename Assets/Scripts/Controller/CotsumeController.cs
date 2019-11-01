using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotsumeController : MonoBehaviour
{
    public Cotsume cotsume;

    [Header("Only Enemy to change HP")]
    public float HP;
    public string id;

    [Header("Local Cotsume")]
    public int HPCotsume;
    public GameObject Head;
    public GameObject Chest;
    public GameObject Hip;

    [Header("Arms")]
    public GameObject UpperArm_R;
    public GameObject LowerArm_R;
    public GameObject UpperArm_L;
    public GameObject LowerArm_L;

    [Header("legs")]
    public GameObject UpperLeg_R;
    public GameObject lowerLeg_R;
    public GameObject UpperLeg_L;
    public GameObject lowerLeg_L;


    private void Start()
    {
        actionCotsume();
    }

    void actionCotsume()
    {
        if (cotsume)
        {
            HPCotsume = cotsume.HP;

            var player = gameObject.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.HP = cotsume.HP;
                player.maxHP = cotsume.HP;
                player.slider.maxValue = cotsume.HP;
                player.sliderPVP.maxValue = cotsume.HP;
                player.spellHP = (cotsume.HP * 30) / 100;
            }

            var Player2 = gameObject.GetComponent<EnemyManager>();
            if (Player2 != null)
            {
                Player2.HP = cotsume.HP;
                Player2.Slider.maxValue = cotsume.HP;
            }

            Head.GetComponent<SpriteRenderer>().sprite = cotsume.Head;
            Chest.GetComponent<SpriteRenderer>().sprite = cotsume.Chest;
            Hip.GetComponent<SpriteRenderer>().sprite = cotsume.Hip;

            UpperArm_R.GetComponent<SpriteRenderer>().sprite = cotsume.UpperArm_R;
            LowerArm_R.GetComponent<SpriteRenderer>().sprite = cotsume.LowerArm_R;
            UpperArm_L.GetComponent<SpriteRenderer>().sprite = cotsume.UpperArm_L;
            LowerArm_L.GetComponent<SpriteRenderer>().sprite = cotsume.LowerArm_L;

            UpperLeg_R.GetComponent<SpriteRenderer>().sprite = cotsume.UpperLeg_R;
            lowerLeg_R.GetComponent<SpriteRenderer>().sprite = cotsume.LowerLeg_R;
            UpperLeg_L.GetComponent<SpriteRenderer>().sprite = cotsume.UpperLeg_L;
            lowerLeg_L.GetComponent<SpriteRenderer>().sprite = cotsume.LowerLeg_L;
        }
    }
}
