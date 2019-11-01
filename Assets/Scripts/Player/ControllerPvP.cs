using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPvP : MonoBehaviour
{
    public SystemDamage system;
    [Header("----------- Player 1 ---------------")]
    public Slider P1Sider;
    public Button P1Skill;
    public Button P1Thow;
    public Button P1Jump;

    [Header("----------- Player 2 ---------------")]
    public Slider P2Sider;
    public Button P2Skill;
    public Button P2Thow;
    public Button P2Jump;

    [Header("-------------- PoP Up -------------")]
    public GameObject PoPUP;

    [Header("Pop-up Player 1")]
    public GameObject POPP1;
    public Text P1Heashot;
    public Text P1Combo;

    [Header("Pop-up Player 2")]
    public GameObject POPP2;
    public Text P2Heashot;
    public Text P2Combo;

    [Header("All Btn fire")]
    public Image btnFireCloneP1;
    public float timeCDP1;
    [SerializeField] private bool activel1;
    [SerializeField] private float number1;

    public Image btnFireCloneP2;
    public float timeCDP2;
    [SerializeField] private bool activel2;
    [SerializeField] private float number2;

    private void Start()
    {
        PoPUP.SetActive(false);
        POPP1.SetActive(false);
        POPP2.SetActive(false);
    }
    private void Update()
    {
        if (system && system.timeP1 != 0)
        {
            timeCDP1 = system.timeP1;
        }
        else if (system && system.timeP1 == 0)
        {
            timeCDP1 = 0;
        }

        if (system && system.timeP2 != 0)
        {
            timeCDP2 = system.timeP2;
        }
        else if (system && system.timeP2 == 0)
        {
            timeCDP2 = 0;
        }

        if (activel1 == true)
        {
            number1 += Time.deltaTime * 3f;
            if ((int)number1 % 2 == 0)
            {
                btnFireCloneP1.color = new Color(255, 0, 0, 1);
            }
            else
            {
                btnFireCloneP1.color = new Color(255, 0, 0, 0);
            }
        }

        if (activel2 == true)
        {
            number2 += Time.deltaTime * 3f;
            if ((int)number2 % 2 == 0)
            {
                btnFireCloneP2.color = new Color(255, 0, 0, 1);
            }
            else
            {
                btnFireCloneP2.color = new Color(255, 0, 0, 0);
            }
        }
    }

    public void Player1Win(int headshots, int combos)
    {
        PoPUP.SetActive(true);
        POPP1.SetActive(true);
        POPP2.SetActive(false);
        P1Heashot.text = headshots.ToString();
        P1Combo.text = combos.ToString();
    }
    public void Player2Win(int headshots, int combos)
    {
        PoPUP.SetActive(true);
        POPP1.SetActive(false);
        POPP2.SetActive(true);
        P2Heashot.text = headshots.ToString();
        P2Combo.text = combos.ToString();
    }
    public void HidePopUp()
    {
        PoPUP.SetActive(false);
        POPP1.SetActive(false);
        POPP2.SetActive(false);
    }

    public void activelBtnClone()
    {
        if (timeCDP1 != 0 && activel1)
        {
            activel1 = false;
            var timer = timeCDP1;
            btnFireCloneP1.fillAmount = 0;
            StartCoroutine(test(0, 1, timer));
        }
    }
    public IEnumerator test(float start, float end, float duration)
    {
        btnFireCloneP1.color = new Color(btnFireCloneP1.color.r, btnFireCloneP1.color.g, btnFireCloneP1.color.b, 1);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            btnFireCloneP1.fillAmount = (float)Mathf.Lerp(start, end, progress);
            yield return null;
        }

        btnFireCloneP1.fillAmount = end;
        activel1 = true;
    }

    public void activelBtnClone2()
    {
        if (timeCDP2 != 0 && activel2)
        {
            activel2 = false;
            var timer = timeCDP2;
            btnFireCloneP2.fillAmount = 0;
            StartCoroutine(test2(0, 1, timer));
        }
    }
    public IEnumerator test2(float start, float end, float duration)
    {
        btnFireCloneP2.color = new Color(btnFireCloneP2.color.r, btnFireCloneP2.color.g, btnFireCloneP2.color.b, 1);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            btnFireCloneP2.fillAmount = (float)Mathf.Lerp(start, end, progress);
            yield return null;
        }

        btnFireCloneP2.fillAmount = end;
        activel2 = true;
    }

    private void OnEnable()
    {
        activel1 = true;
        btnFireCloneP1.fillAmount = 1;
        activel2 = true;
        btnFireCloneP2.fillAmount = 1;

        ManagerAds.Ins.HideBanner();
    }

    private void OnDisable()
    {
        ManagerAds.Ins.ShowBanner();
    }
}
