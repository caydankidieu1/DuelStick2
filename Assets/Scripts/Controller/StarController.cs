using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StarController : MonoBehaviour
{
    public test[] Phase;

    public GameObject Beginner;
    public GameObject Inter;
    public GameObject Advan;
    public GameObject Exper;
    public GameObject Master;

    public Image dot1;
    public Image dot2;
    public Image dot3;
    public Image dot4;
    public Image dot5;

    private int sumMapClear;
    public WeaponStoreUI AllWeapon;

    private void Awake()
    {
        LoadStar();//
    }
    private void Start()
    {
        //LoadStar();//
        //OnBeginner();
    }

    private void Update()
    {
        //check();
    }
    void check()
    {
        foreach (test item in Phase)
        {
            item.Star = item.phase.IsStar;
        }
    }
    public void SaveStar()
    {
        SaveSystem.SaveStar(this);
    }
    public void LoadStar()
    {
        string path = Application.persistentDataPath + "/Star.dat";
        if (File.Exists(path))
        {
            StarData data = SaveSystem.loadStar();

            for (int i = 0; i < Phase.Length; i++)
            {
                //Phase[i].phase.IsStar = data.Star[i];
                //Debug.Log(data.Star[i]);
            }
        }
    }
    public void OnBeginner()
    {
        dot1.color = new Color(255, 255, 255, 1f);
        dot2.color = new Color(255, 255, 255, 0.47f);
        dot3.color = new Color(255, 255, 255, 0.47f);
        dot4.color = new Color(255, 255, 255, 0.47f);
        dot5.color = new Color(255, 255, 255, 0.47f);

        Beginner.SetActive(true);
        Inter.SetActive(false);
        Advan.SetActive(false);
        Exper.SetActive(false);
        Master.SetActive(false);
    }
    public void Intermediate()
    {
        dot1.color = new Color(255, 255, 255, 0.47f);
        dot2.color = new Color(255, 255, 255, 1f);
        dot3.color = new Color(255, 255, 255, 0.47f);
        dot4.color = new Color(255, 255, 255, 0.47f);
        dot5.color = new Color(255, 255, 255, 0.47f);

        Beginner.SetActive(false);
        Inter.SetActive(true);
        Advan.SetActive(false);
        Exper.SetActive(false);
        Master.SetActive(false);
    }
    public void Advance()
    {
        dot1.color = new Color(255, 255, 255, 0.47f);
        dot2.color = new Color(255, 255, 255, 0.47f);
        dot3.color = new Color(255, 255, 255, 1f);
        dot4.color = new Color(255, 255, 255, 0.47f);
        dot5.color = new Color(255, 255, 255, 0.47f);

        Beginner.SetActive(false);
        Inter.SetActive(false);
        Advan.SetActive(true);
        Exper.SetActive(false);
        Master.SetActive(false);
    }
    public void Expert()
    {
        dot1.color = new Color(255, 255, 255, 0.47f);
        dot2.color = new Color(255, 255, 255, 0.47f);
        dot3.color = new Color(255, 255, 255, 0.47f);
        dot4.color = new Color(255, 255, 255, 1f);
        dot5.color = new Color(255, 255, 255, 0.47f);

        Beginner.SetActive(false);
        Inter.SetActive(false);
        Advan.SetActive(false);
        Exper.SetActive(true);
        Master.SetActive(false);
    }
    public void Master_()
    {
        dot1.color = new Color(255, 255, 255, 0.47f);
        dot2.color = new Color(255, 255, 255, 0.47f);
        dot3.color = new Color(255, 255, 255, 0.47f);
        dot4.color = new Color(255, 255, 255, 0.47f);
        dot5.color = new Color(255, 255, 255, 1f);

        Beginner.SetActive(false);
        Inter.SetActive(false);
        Advan.SetActive(false);
        Exper.SetActive(false);
        Master.SetActive(true);
    }

    public void testAllMapClear()
    {
        var test = 0;
        foreach (test item in Phase)
        {
            if (item.phase.checkClear)
            {
                test++;
            }
        }

        if (test != sumMapClear)
        {
            PlayerPrefs.SetInt("level", test);
            AllWeapon.CheckWeaponByLevel();
            Debug.Log("Number map Clear :" + test);
            sumMapClear = test;
        }

        //Debug.Log("Number map Clear :" + test);
    }
}

[System.Serializable]
public class test{
    public Phase phase;
    public int Star;
}
