using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillController : MonoBehaviour
{
    public PowerUI powerUI;
    [Space]
    public QuestControllerUI questControllerUI;
    public Quest questSpell;
    [Space]
    public Power pow1;
    public Power pow2;
    public Power pow3;

    [Header("Button")]
    public Button skill;
    public Button thowAndPick;
    public Button Jump;

    public Button spell1;
    public Text amount1;
    [Space]
    public Button spell2;
    public Text amount2;
    [Space]
    public Button spell3;
    public Text amount3;

    private int cloneSpeel1;
    private int cloneSpeel2;
    private int cloneSpeel3;

    private void OnEnable()
    {
        if (pow1.amount >= 10)
        {
            cloneSpeel1 = 10;
        }
        else if (pow1.amount < 10)
        {
            cloneSpeel1 = pow1.amount;
        }

        if (pow2.amount >= 10)
        {
            cloneSpeel2 = 10;
        }
        else if (pow2.amount < 10)
        {
            cloneSpeel2 = pow2.amount;
        }

        if (pow3.amount >= 10)
        {
            cloneSpeel3 = 10;
        }
        else if (pow3.amount < 10)
        {
            cloneSpeel3 = pow3.amount;
        }
    }
    void Update()
    {
        #region 
        amount1.text = cloneSpeel1.ToString();
        amount2.text = cloneSpeel2.ToString();
        amount3.text = cloneSpeel3.ToString();
        //amount4.text = cloneSpeel4.ToString();

        if (cloneSpeel1 > 0)
        {
            spell1.enabled = true;
        }
        else
        {
            spell1.enabled = false;
        }
        //
        if (cloneSpeel2 > 0)
        {
            spell2.enabled = true;
        }
        else
        {
            spell2.enabled = false;
        }
        //
        if (cloneSpeel3 > 0)
        {
            spell3.enabled = true;
        }
        else
        {
            spell3.enabled = false;
        }
        #endregion
    }
    public void consumePow1()
    {
        if (pow1.amount > 0)
        {
            pow1.amount--;
            cloneSpeel1--;
            questSpell.number++;
            questControllerUI.SaveQuest();
            powerUI.SaveData();
        }
    }
    public void consumePow2()
    {
        if (pow2.amount > 0)
        {
            pow2.amount--;
            cloneSpeel2--;
            questSpell.number++;
            questControllerUI.SaveQuest();
            powerUI.SaveData();
        }
    }
    public void consumePow3()
    {
        if (pow3.amount > 0)
        {
            pow3.amount--;
            cloneSpeel3--;
            questSpell.number++;
            questControllerUI.SaveQuest();
            powerUI.SaveData();
        }
    }
}
