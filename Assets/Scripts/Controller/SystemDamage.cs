using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDamage : MonoBehaviour
{
    [Header("Time Delay Win and Lose")]
    public float timeDelayWin = 1f;
    public float timeDelayLose = 1f;

    [Header("Damage Base")]
    public float damageBase = 5;
    public float KnockBase = 1;

    [Header("System Info Player")] // Save Information Player
    public Vector3 local;
    public float trueDamagePlayer = 0;
    public float trueDamageSkillPlayer = 0;
    public float trueKnockBPlayer = 0;
    public bool trueDeath;
    public bool knockBack;

    [Header("System Info Enemy")] // Save Information Enemy
    public float trueDamageEnemy = 0;
    public float trueDamageSkillEnemy = 0;
    public float trueKnockBEnemy = 0;
    public bool trueDeathAllEnemy;

    [Header("Info Battle")]
    public bool victory;
    public bool loser;
    public bool secondChange;

    [Header("---------------")]
    public StarController StarC;
    public BattleUI battleUI;
    public float timeCombo = 3f;

    [Header("Quest")]
    public QuestControllerUI QuestController;
    public static SystemDamage instance;

    [Header("Info CD weapon P1 vs P2")]
    public float timeP1;
    public float timeP2;

    [Header("Check Time when Player Die without time")]
    public float valueTime;
    [SerializeField] private float firstTimeDie;
    [SerializeField] private float secondTimeDie;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        else
        {
            instance = this;
        }

    }
    private void Update()
    {
        gameOver();
        Victory();
        //Debug.Log(local);

        if (knockBack)
        {
            StartCoroutine(wait());
        }

        firstTimeDie += Time.deltaTime;
    }

    public void GetTimeForDeath()
    {
        valueTime = firstTimeDie - secondTimeDie;
        secondTimeDie = firstTimeDie;

        if (firstTimeDie >= 1000)
	    {
            valueTime = 0;
            firstTimeDie = 0;
            secondTimeDie = 0;
	    }

        Debug.Log("show time die");
    }

    void gameOver()
    {
        if (trueDeath && secondChange == false)
        {
            battleUI.GameOver();
            loser = true;
            QuestController.SaveQuest();
            //StartCoroutine(DelayGameOver());
        }

        if (trueDeath && secondChange)
        {
            battleUI.GameOver2();
            loser = true;
            QuestController.SaveQuest();
            //StartCoroutine(DelayGameOver());
        }
    }
    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(timeDelayLose);
        battleUI.GameOver();
        loser = true;
        yield return 0;
    }
    void Victory()
    {
        if (trueDeathAllEnemy)
        {
            battleUI.GameWin();
            victory = true;
            QuestController.SaveQuest();
            //StartCoroutine(DelayVictory());

            //StarC.testAllMapClear();
        }
    }
    IEnumerator DelayVictory()
    {
        yield return new WaitForSeconds(timeDelayWin);
        battleUI.GameWin();
        victory = true;
        yield return 0;
    }
    public void hidePopUp()
    {
        trueDeath = false;
        trueDeathAllEnemy = false;
        victory = false;
        loser = false;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(.1f);
        knockBack = false;
        yield return 0;
    }
}
