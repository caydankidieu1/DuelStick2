using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotsumeControllerEnemy : MonoBehaviour
{
    public bool checkChangeColor;

    [Header("-------------------------------")]
    public Cotsume cotsume;
    public ControllerSystemSkins ControllerSkins;

    [Header("Only Enemy to change HP")]
    public float HP;
    public int id;

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

    [SerializeField] private Color colorCotsume;

    private void Start()
    {
        actionCotsume();
    }

    void actionCotsume()
    {
        if (id > 0)
        {
            for (int i = 0; i < ControllerSkins.cotsume.Length; i++)
            {
                if (ControllerSkins.cotsume[i].id == id)
                {
                    cotsume = ControllerSkins.cotsume[i];
                }
            }
        }
        else
        {
            List<Cotsume> testAllSkins = new List<Cotsume>();

            testAllSkins.Clear();
            for (int i = 0; i < ControllerSkins.cotsume.Length; i++)
            {
                if (ControllerSkins.cotsume[i].checkBuy)
                {
                    testAllSkins.Add(ControllerSkins.cotsume[i]);
                }
            }

            Debug.Log(testAllSkins.Count);
            if (testAllSkins.Count >= 1)
            {
                cotsume = testAllSkins[Random.Range(0, (int)testAllSkins.Count)];
            }
            else
            {
                cotsume = ControllerSkins.cotsume[52];
            }
            

            //cotsume = ControllerSkins.cotsume[0];
        }

        if (cotsume)
        {
            var infoEnemyManager = gameObject.GetComponent<EnemyManager>();
            //infoEnemyManager.HP = cotsume.HP;
            if (HP != 0)
            {
                infoEnemyManager.HP = HP;
                infoEnemyManager.maxHP = HP;
            }
            else
            {
                infoEnemyManager.HP = cotsume.HP;
                infoEnemyManager.maxHP = cotsume.HP;
            }
            

            if (infoEnemyManager.Slider)
            {
                infoEnemyManager.Slider.maxValue = HP;
            }

            var infoHead = Head.GetComponent<SpriteRenderer>();
            var indoChest = Chest.GetComponent<SpriteRenderer>();
            var infoHip = Hip.GetComponent<SpriteRenderer>();
            var infoUpperAR = UpperArm_R.GetComponent<SpriteRenderer>();
            var infoLowerAR = LowerArm_R.GetComponent<SpriteRenderer>();
            var infoUpperAL = UpperArm_L.GetComponent<SpriteRenderer>();
            var infoLowerAL = LowerArm_L.GetComponent<SpriteRenderer>();
            var infoUpperLR = UpperLeg_R.GetComponent<SpriteRenderer>();
            var infoLowerLR = lowerLeg_R.GetComponent<SpriteRenderer>();
            var infoUpperLL = UpperLeg_L.GetComponent<SpriteRenderer>();
            var infoLowerLL = lowerLeg_L.GetComponent<SpriteRenderer>();


            infoHead.sprite = cotsume.Head;
            indoChest.sprite = cotsume.Chest;
            infoHip.sprite = cotsume.Hip;

            infoUpperAR.sprite = cotsume.UpperArm_R;
            infoLowerAR.sprite = cotsume.LowerArm_R;
            infoUpperAL.sprite = cotsume.UpperArm_L;
            infoLowerAL.sprite = cotsume.LowerArm_L;

            infoUpperLR.sprite = cotsume.UpperLeg_R;
            infoLowerLR.sprite = cotsume.LowerLeg_R;
            infoUpperLL.sprite = cotsume.UpperLeg_L;
            infoLowerLL.sprite = cotsume.LowerLeg_L;

            if (checkChangeColor)
            {
                colorCotsume = new Color(0, 0, 0, 1f);

                infoHead.color = colorCotsume;
                indoChest.color = colorCotsume;
                infoHip.color = colorCotsume;
                infoUpperAR.color = colorCotsume;
                infoLowerAR.color = colorCotsume;
                infoUpperAL.color = colorCotsume;
                infoLowerAL.color = colorCotsume;
                infoUpperLR.color = colorCotsume;
                infoLowerLR.color = colorCotsume;
                infoUpperLL.color = colorCotsume;
                infoLowerLL.color = colorCotsume;
            }
        }

    }
}
