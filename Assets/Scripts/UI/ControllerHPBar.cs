using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerHPBar : MonoBehaviour
{
    public Image MainHp;
    public Image CloneHp;
    private float clone;
    private bool activel;
    public float duration = 0.5f;

    private const float DAMAGED_HEATH_SHRINK_TIMER_MAX = 1f;
    private float damagedHealthShrinkTimer;

    [Header("----------------------------- Test --------------------")]
    public float start;

    void Start()
    {
        CloneHp.fillAmount = 1.5f;
        clone = MainHp.fillAmount;
        start = MainHp.fillAmount;
    }
    // Update is called once per frame


    /*  void Update()
      {
          if (MainHp.fillAmount > CloneHp.fillAmount)
          {
              clone = 1;
              CloneHp.fillAmount = 1;
              start = MainHp.fillAmount;
          }
      }

      public void attack()
      {
          if (gameObject.activeSelf == true)
          {
              StartCoroutine(test());
          }
      }

      public IEnumerator test()
      {
          if (CloneHp.fillAmount > MainHp.fillAmount)
          {
              float end = MainHp.fillAmount;

              for (float timer = 0; timer < duration; timer += Time.deltaTime)
              {
                  float progress = timer / duration;
                  CloneHp.fillAmount = (float)Mathf.Lerp(start, end, progress);

                  //Debug.Log(CloneHp.fillAmount);

                  yield return new WaitForEndOfFrame();
              }

              start = MainHp.fillAmount;
              //clone = MainHp.fillAmount;
              CloneHp.fillAmount = MainHp.fillAmount;
          }

      }*/


    void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0)
        {
            if (MainHp.fillAmount < CloneHp.fillAmount)
            {
                float shrinkSpeed = 1f;
                CloneHp.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    public void attack()
    {
        if (CloneHp.fillAmount == 0)
        {
            CloneHp.fillAmount = 1;
        }
        damagedHealthShrinkTimer = DAMAGED_HEATH_SHRINK_TIMER_MAX;
    }

}
