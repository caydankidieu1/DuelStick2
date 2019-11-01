using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCotsumeEx : MonoBehaviour
{
    public LoadData loadData;
    public ControllerSystemSkins controllerSkin;
    [Header("----------------------------- value From Excel ----------------------------")]
    List<CotsumeFrom> Cotsumes = new List<CotsumeFrom>();
    // Start is called before the first frame update
    void Start()
    {
        TextAsset cotsumeData = Resources.Load<TextAsset>("CotsumesDataNew");

        string[] data = cotsumeData.text.Split(new char[] { '\n' });
        //Debug.Log(data.Length);

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                CotsumeFrom C = new CotsumeFrom();
                int.TryParse(row[0], out C.id);
                C.name = row[1];
                int.TryParse(row[2], out C.HPbase);
                int.TryParse(row[3], out C.HPmax);
                int.TryParse(row[4], out C.cost);
                float.TryParse(row[5], out C.rate);
                int.TryParse(row[6], out C.levelunlock);
        
                Cotsumes.Add(C);
            }
        }

        foreach (CotsumeFrom item in Cotsumes)
        {
            //Debug.Log(item.name + ", " + item.id);
           // Debug.Log(item.rate);
            for (int i = 0; i < controllerSkin.cotsume.Length; i++)
            {
                if (item.id == controllerSkin.cotsume[i].id)
                {
                    controllerSkin.cotsume[i].HP = item.HPbase;
                    controllerSkin.cotsume[i].nameCotsume = item.name;
                    controllerSkin.cotsume[i].maxHPCotsume = item.HPmax;
                    controllerSkin.cotsume[i].cost = item.cost;
                    controllerSkin.cotsume[i].rateLuckySpin = item.rate;
                    controllerSkin.cotsume[i].levelUnlock = item.levelunlock;
                }
            }
        }

        loadData.SaveAndLoadSkins();
    }
}
