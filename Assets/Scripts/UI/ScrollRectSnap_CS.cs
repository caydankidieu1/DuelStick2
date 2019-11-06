using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap_CS : MonoBehaviour
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

    [Header("Value min, max")]
    public float lerpSpeed = 5f;
    public float value = 400;
    public float max = 100;
    [Space]

    [Header("new Skins")]
    public Transform Local;
    public GameObject prefab;
    public SkinUI skins;

    [Header("Btn")]
    public Text nameSkins;
    public Button btnEquip;

    [Header("Check Player")]
    public bool checkUse;
    public bool checkP1;
    public int localSkinsP1;
    public bool checkP2;
    public int localSkinsP2;

    public void create()
    {
        bttn = new Button[skins.cotsume.Length];

        for (int i = 0; i < skins.cotsume.Length; i++)
        {
            GameObject T = Instantiate(prefab);//, Local.transform.position, Quaternion.identity);
            T.transform.SetParent(Local);

            var rectImage = T.GetComponent<NewControllerBtn>();

            rectImage.Skins = skins.cotsume[i];
            rectImage.activelScaleImage();

            var Rect = T.GetComponent<RectTransform>();
            Rect.anchoredPosition = new Vector2(i * bttnDistance, 0f);
            Rect.localScale = new Vector3(1, 1, 1);

            T.GetComponent<NewControllerBtn>().image.color = new Color(255, 255, 255, 0.2f);

            bttn[i] = T.GetComponent<Button>();
        }
    }
    private void Start()
    {
        create();
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

        checkUse = false;
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
                    minButtonNum = a;
                    //Debug.Log(bttn[minButtonNum].name);

                    checkNameAndUse();
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

        if (Mathf.Abs(position - newX) < 1f)
        {
            newX = position;
        }

        if (Mathf.Abs(newX) >= Mathf.Abs(position) -1f && Mathf.Abs(newX) <= Mathf.Abs(position) + 1 && !messageSend)
        {
            messageSend = true;
            SendMessageFromButton(minButtonNum);
        }

        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }
    void checkNameAndUse()
    {
        var btn = bttn[minButtonNum].GetComponent<NewControllerBtn>();
        nameSkins.text = btn.Skins.nameCotsume;

        if (checkP1)
        {
            checkUse = btn.Skins.checkUseP1;
        }

        if (checkP2)
        {
            checkUse = btn.Skins.checkUseP2;
        }

        localSkinsP1 = minButtonNum;
        localSkinsP2 = minButtonNum;

        if (btn.Skins.checkBuy)
        {
            btn.image.color = new Color(255, 255, 255, 1f);
        }
        else
        {
            btn.image.color = new Color(255, 255, 255, 0.2f);
        }

        EquipP1();
    }
    void SendMessageFromButton(int btnIndex)
    {
        var btn = bttn[btnIndex].GetComponent<NewControllerBtn>();
        nameSkins.text = btn.Skins.nameCotsume;

        if (btn.Skins.checkBuy)
        {
            EquipP1();
        }

        localSkinsP1 = btnIndex;
        localSkinsP2 = btnIndex;
    }

    public void EquipP1()
    {
        if (checkP1 && localSkinsP1 != 0)
        {
            var btn = bttn[localSkinsP1].GetComponent<NewControllerBtn>();

            if (btn.Skins.checkBuy)
            {
                skins.ResetAllCotsumeP1();
                checkUse = true;
                btn.Skins.checkUseP1 = true;
                skins.SaveSkin();
            }
        }
    }

    public void StartDrag()
    {
        checkUse = false;
        messageSend = false;
        lerpSpeed = 5f;
        dragging = true;

        targetNearestButton = true;

        if (checkP1)
        {
            localSkinsP1 = 0;
        }

        if (checkP2)
        {
            localSkinsP2 = 0;
        }
    }
    public void EndDrag()
    {
        dragging = false;
    }
    public void GotoButton() // End List
    {
        int buttonIndex = bttn.Length;
        targetNearestButton = false;
        minButtonNum = buttonIndex - 1;

        checkNameAndUse();
    }

    public void Next()
    {
        targetNearestButton = false;
        if (minButtonNum == bttn.Length - 1)
        {
            //minButtonNum = 0;

            for (int i = 0; i < bttn.Length; i++)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                {
                    minButtonNum = i;
                    break;
                }
            }
        }
        else
        {
            //minButtonNum++;

            var sum = 0;
            for (int i = 0; i < bttn.Length; i++)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                {
                    sum++;
                }
            }

            if (sum > 1)
            {
                for (int i = minButtonNum + 1; i < bttn.Length; i++)
                {
                    if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                    {
                        minButtonNum = i;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Giu nguyen vi tri");
            }
        }

        checkNameAndUse();
    }

    public void Back()
    {
        targetNearestButton = false;
        if (minButtonNum == 0)
        {
            //minButtonNum = bttn.Length - 1;

            for (int i = bttn.Length - 1; i > 0; i--)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                {
                    minButtonNum = i;
                    break;
                }
            }
        }
        else
        {
            //minButtonNum--;

            var sum = 0;
            for (int i = 0; i < bttn.Length; i++)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                {
                    sum++;
                }
            }

            if (sum > 1)
            {
                if (minButtonNum == 1)
                {
                    for (int i = minButtonNum - 1; i >= 0; i--)
                    {
                        if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                        {
                            minButtonNum = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = minButtonNum - 1; i > 0; i--)
                    {
                        if (bttn[i].GetComponent<NewControllerBtn>().Skins.checkBuy)
                        {
                            minButtonNum = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Giu nguyen vi tri");
            }
        }

        checkNameAndUse();
    }
}
