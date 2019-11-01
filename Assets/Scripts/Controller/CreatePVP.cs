using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePVP : MonoBehaviour
{
    [Header("Want to Create Player and Enemy")]
    public GameObject wave;
    [Space]
    public GameObject Played1;
    public GameObject Played2;

    [SerializeField] private ControllerPvP PVP;

    public void ActionWavePVP()
    {
        GetComponent<SettingHomeUI>().unActivelAllButton();
        StartCoroutine(CLoneActionPVP());
    }
    public IEnumerator CLoneActionPVP()
    {
        yield return new WaitForSeconds(0.75f);

        GetComponentInParent<SettingMenuUI>().GamePlayUIPVP(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(Played1, map.GetComponent<LocalForPVP>().played1.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForPVP>().played1.transform.position;

        GameObject T;
        T = Instantiate(Played2, map.GetComponent<LocalForPVP>().played2.transform);
        T.SetActive(true);
        T.transform.position = map.GetComponent<LocalForPVP>().played2.transform.position;

        yield return 0;
    }

    public void ReloadWavePVP()//test
    {
        PVP.HidePopUp();

        Destroy(FindObjectOfType<LocalForPVP>().gameObject);
     
        GetComponentInParent<SettingMenuUI>().GamePlayUIPVP(); // hien thi UI battle va all UI khac
        GameObject map;
        map = Instantiate(wave) as GameObject;
        map.SetActive(true);
        //wave.SetActive(true);

        GameObject P;
        P = Instantiate(Played1, map.GetComponent<LocalForPVP>().played1.transform);
        P.SetActive(true);
        P.transform.position = map.GetComponent<LocalForPVP>().played1.transform.position;

        GameObject T;
        T = Instantiate(Played2, map.GetComponent<LocalForPVP>().played2.transform);
        T.SetActive(true);
        T.transform.position = map.GetComponent<LocalForPVP>().played2.transform.position;
        
    }
}
