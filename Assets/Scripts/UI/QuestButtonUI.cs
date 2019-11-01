using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButtonUI : MonoBehaviour
{
    public Quest quest;
    public string idQuest;
    public Text nameQuest;
    public Text CostPresent;
    public Image image;

    [Header("Button")]
    public GameObject process;
    public Text numberProcess;
    public GameObject Claim;

    public QuestControllerUI QuestUI;

    private void Start()
    {
        QuestUI = GetComponentInParent<QuestControllerUI>();
    }

    private void Update()
    {
        checknumber();
    }

    void checknumber() // kiem tra xem so nhiem vu hoan thanh du chua 
    {
        if (quest.number == 0)
        {
            process.SetActive(true);
            Claim.SetActive(false);
        }

        numberProcess.text = quest.number + " / " + quest.numberMax;

        if (quest.number >= quest.numberMax)
        {
            quest.checkClaim = true;
            Claim.SetActive(true);
        }
        else
        {
            quest.checkClaim = false;
            Claim.SetActive(false);
        }

        if (quest.checkClaim && !quest.checkReceived)
        {
            process.SetActive(false);
            Claim.SetActive(true);
        }

        if (quest.checkReceived)
        {
            process.SetActive(false);
            Claim.SetActive(false);
        }
    }

    public void ClaimQuest()
    {
        if (quest.number >= quest.numberMax)
        {
            QuestUI.coins.Add(quest.present);
            quest.checkReceived = true;
            process.SetActive(false);
            Claim.SetActive(false);

            for (int i = 0; i < QuestUI.quest.Length; i++)
            {
                if (QuestUI.quest[i].id == "08")
                {
                    QuestUI.quest[i].number++;
                }
            }

            QuestUI.SaveQuest();
        }
    }
}
