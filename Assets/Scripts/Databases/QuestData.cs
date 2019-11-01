using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public List<quest> Q = new List<quest>();

    public QuestData(QuestControllerUI questUI)
    {
        Q.Clear();
        for (int i = 0; i < questUI.quest.Length; i++)
        {
            quest test = new quest();
            test.id = questUI.quest[i].id;
            test.number = questUI.quest[i].number;
            test.nowDay = questUI.quest[i].nowDay;
            test.CheckClaim = questUI.quest[i].checkClaim;
            test.CheckReceived = questUI.quest[i].checkReceived;

            Q.Add(test);
        }
    }
}

[System.Serializable]
public class quest
{
    public string id;
    public int number;
    public int nowDay;
    public bool CheckClaim;
    public bool CheckReceived;
}
