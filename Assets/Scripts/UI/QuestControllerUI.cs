using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class QuestControllerUI : MonoBehaviour
{
    [Header("Get Coins")]
    public Coins coins;

    [Header("Info Local Create Button")]
    public Transform local;
    public GameObject prefab;
    public string idCheck;
    public int numberQuestClaim;

    private QuestButtonUI[] buttonUI;

    [Header("Quest")]
    public Quest[] quest;

    [Header("Test warning Quest")]
    public Animation animQuest;

    void Start()
    {
        LoadQuest(); //
        CreateNewSlotSkin();
        buttonUI = GetComponentsInChildren<QuestButtonUI>();
    }
    void CreateNewSlotSkin()
    {
        foreach (Quest item in quest)
        {
            if (item)
            {
                GameObject T = Instantiate(prefab) as GameObject;
                T.transform.SetParent(local);
                T.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                var infoBtn = T.GetComponent<QuestButtonUI>();
                infoBtn.quest = item;
                infoBtn.idQuest = item.id;
                infoBtn.nameQuest.text = item.Description;
                infoBtn.CostPresent.text = item.present.ToString();
                infoBtn.image.sprite = item.iconQuest;
                infoBtn.numberProcess.text = item.number + " / " + item.numberMax;
                if (item.checkClaim)
                {
                    infoBtn.Claim.SetActive(true);
                    if (item.checkReceived)
                    {
                        infoBtn.process.SetActive(false);
                        infoBtn.Claim.SetActive(false);
                    }
                }
            }
        }
    }

    public void SaveQuest()
    {
        SaveSystem.SaveQuest(this);
    }
    public void LoadQuest()
    {
        string pathQuest = Application.persistentDataPath + "/Quest.dat";
        if (File.Exists(pathQuest))
        {
            QuestData data = SaveSystem.LoadQuest();

            for (int i = 0; i < quest.Length; i++)
            {
                for (int k = 0; k < data.Q.Count; k++)
                {
                    if (quest[i].id == data.Q[k].id)
                    {
                        quest[i].number = data.Q[k].number;
                        quest[i].checkClaim = data.Q[k].CheckClaim;
                        quest[i].checkReceived = data.Q[k].CheckReceived;
                    }
                }
            }
        }
    }
    public void ResetQuest()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].number = 0;
            quest[i].checkClaim = false;
            quest[i].checkReceived = false;
        }

        SaveQuest();
        LoadQuest();
    }

    public void checkQuest()
    {
        var sum = 0;
        for (int i = 0; i < quest.Length; i++)
        {
            if (quest[i].checkClaim)
            {
                sum++;
            }

            if (quest[i].id == "08")
            {
                numberQuestClaim = quest[i].number;
            }
        }

        if (numberQuestClaim != sum)
        {
            numberQuestClaim = sum;

            SaveQuest();
            LoadQuest();
        }
    }

    private void OnEnable()
    {
        checkQuest();
    }
}
