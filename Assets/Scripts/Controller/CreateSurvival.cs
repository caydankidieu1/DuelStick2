using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSurvival : MonoBehaviour
{
    [Header("Want to Create Player and Enemy")]
    public GameObject[] wave;
    public GameObject player;

    public void ActionWaveSur()
    {
        /*GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave[Random.Range(0 ,(int)wave.Length)]) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(player, map.GetComponent<LocalForSurvival>().player.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForSurvival>().player.transform.position;*/

        /*Destroy(FindObjectOfType<LocalForPVP>().gameObject);
        Destroy(FindObjectOfType<LocalForSurvival>().gameObject);
        Destroy(FindObjectOfType<Local>().gameObject);*/

        GetComponent<SettingHomeUI>().unActivelAllButton();
        StartCoroutine(CloneActionWaveSur());
    }
    public IEnumerator CloneActionWaveSur()
    {
        yield return new WaitForSeconds(0.75f);

        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave[Random.Range(0, (int)wave.Length)]) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(player, map.GetComponent<LocalForSurvival>().player.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForSurvival>().player.transform.position;

        yield return 0;
    }
    public void ReloadWaveSur()//test
    {
        //PVP.HidePopUp();

        Destroy(FindObjectOfType<LocalForSurvival>().gameObject);

        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave[Random.Range(0, (int)wave.Length)]) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(player, map.GetComponent<LocalForSurvival>().player.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForSurvival>().player.transform.position;
    }
    public void RivivalPlayer()//test
    {
        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = FindObjectOfType<LocalForSurvival>().gameObject;
        map.SetActive(true);
        map.GetComponent<LocalForSurvival>().secondchange = true;

        GameObject P;
        P = Instantiate(player, map.GetComponent<LocalForSurvival>().player.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForSurvival>().player.transform.position;
        P.GetComponent<PlayerManager>().Block();

        map.GetComponent<LocalForSurvival>().playerManager = P.GetComponent<PlayerManager>();
        var test = map.GetComponentsInChildren<AI>();
        if (test.Length > 0)
        {
            for (int i = 0; i < test.Length; i++)
            {
                test[i].target = P.transform;
            }
        }
    }
    public void RivivalPlayerClone()//test
    {
        GetComponentInParent<SettingMenuUI>().GamePlayUI(); // hien thi UI battle va all UI khac
        GameObject map;
        map = FindObjectOfType<LocalForSurvival>().gameObject;
        map.SetActive(true);
        map.GetComponent<LocalForSurvival>().secondchange = true;
    }
}
