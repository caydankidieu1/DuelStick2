using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap_CS3 : MonoBehaviour
{
    //public Variables
    public RectTransform panel;
    public Button[] bttn;
    public RectTransform center;
    public int startButton = 1;

    //private Variables
    [Header("Value")]
    public float[] distance;
    public float[] distReposition;
    private bool dragging = false;
    public int bttnDistance;
    public int minButtonNum;
    public int bttnLength;
    private bool messageSend = false;
    private bool targetNearestButton = true;
    private bool activelScale;
    //private bool activelBtnEquip = false;

    [Header("Value min, max")]
    public float lerpSpeed = 5f;
    [SerializeField] private float cloneLerpSpeed;
    public float value = 400;
    public float max = 100;
    public float duration = 0.5f;
    private void Start()
    {
        Sortbtn();
        cloneLerpSpeed = lerpSpeed;
        bttnLength = bttn.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);

        for (int i = 0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            max = Mathf.Max(distance);
        }
    }
    private void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            if (distReposition[i] > max)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }

            if (distReposition[i] < -max)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
        }

        if (targetNearestButton)
        {
            float minDistance = Mathf.Min(distance);

            for (int a = 0; a < bttn.Length; a++)
            {
                if (minDistance == distance[a])
                {
                    //Debug.Log(minDistance);

                    minButtonNum = a;
                    //Debug.Log(bttn[minButtonNum].name);

                    var btn = bttn[minButtonNum].GetComponent<Phase>();
                    btn.showBackGround();

                    //bttn[minButtonNum].gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                    /*  if (!dragging && activelScale)
                      {
                          var value2 = Mathf.Lerp(1.5f, 1f, minDistance * 0.1f);
                          bttn[minButtonNum].gameObject.transform.localScale = new Vector3(value2, value2, value2);
                      }*/
                }
                else
                {
                    bttn[a].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }

        if (!dragging)
        {
            LerpToBttn(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }
    void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * lerpSpeed);

        if (Mathf.Abs(position - newX) < 15f)
        {
            newX = position;
        }

        if (Mathf.Abs(newX) >= Mathf.Abs(position) - 1f && Mathf.Abs(newX) <= Mathf.Abs(position) + 1 && !messageSend)
        {
            messageSend = true;
            SendMessageFromButton(minButtonNum);
        }

        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }
    void SendMessageFromButton(int btnIndex)
    {
        var btn = bttn[btnIndex].GetComponent<Phase>();
        btn.showBackGround();
        activelScale = true;
        StartCoroutine(test());
    }
    public IEnumerator test()
    {
        float start = 1f;
        float end = 1.5f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            var value2 = Mathf.Lerp(start, end, progress);
            bttn[minButtonNum].gameObject.transform.localScale = new Vector3(value2, value2, value2);
            yield return null;
        }

        bttn[minButtonNum].gameObject.transform.localScale = new Vector3(end, end, end);
    }
    public void StartDrag()
    {
        messageSend = false;
        activelScale = false;
        lerpSpeed = cloneLerpSpeed;
        dragging = true;

        targetNearestButton = true;
    }
    public void EndDrag()
    {
        dragging = false;
    }
    public void Sortbtn()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            var Rect = bttn[i].GetComponent<RectTransform>();
            Rect.anchoredPosition = new Vector2(i * bttnDistance, 0f);
            Rect.localScale = new Vector3(1, 1, 1);
        }
    }


    public void Next()
    {
        targetNearestButton = false;
        if (minButtonNum == bttn.Length - 1)
        {
            minButtonNum = 0;
        }
        else
        {
            minButtonNum++;
        }

        for (int i = 0; i < bttn.Length; i++)
        {
            if (i != minButtonNum)
            {
                bttn[i].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        SendMessageFromButton(minButtonNum);
    }
    public void Back()
    {
        targetNearestButton = false;
        if (minButtonNum == 0)
        {
            minButtonNum = bttn.Length - 1;
        }
        else
        {
            minButtonNum--;
        }

        for (int i = 0; i < bttn.Length; i++)
        {
            if (i != minButtonNum)
            {
                bttn[i].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        SendMessageFromButton(minButtonNum);
    }
    public void StartRandom()
    {
        targetNearestButton = false;

        int randomTest = Random.Range(0, bttn.Length);
        minButtonNum = randomTest;

        for (int i = 0; i < bttn.Length; i++)
        {
            if (i != minButtonNum)
            {
                bttn[i].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        SendMessageFromButton(minButtonNum);
    }
}
