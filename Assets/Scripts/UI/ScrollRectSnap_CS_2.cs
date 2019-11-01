using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap_CS_2 : MonoBehaviour
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
    //private bool activelBtnEquip = false;

    [Header("Value min, max")]
    public float lerpSpeed = 5f;
    private float cloneLerpSpeed;
    public float value = 400;
    public float max = 100;
    [Space]

    [Header("new weapons")]
    public Transform Local;
    public GameObject prefab;
    public WeaponStoreUI weapons;

    [Header("Check Player")]
    public bool checkP1;
    public int localWeaponsP1;
    public bool checkP2;
    public int localWeaponsP2;

    private GameObject[] AllBtnGameObject;

    void Start()
    {
        cloneLerpSpeed = lerpSpeed;
    }

    public void create()
    {
        bttn = new Button[weapons.weapon.Length];
        AllBtnGameObject = new GameObject[weapons.weapon.Length];

        for (int i = 0; i < weapons.weapon.Length; i++)
        {
            GameObject T = Instantiate(prefab);

            AllBtnGameObject[i] = T;

            T.transform.SetParent(Local);

            var rectImage = T.GetComponent<NewControllerBtn>();

            rectImage.Weapons = weapons.weapon[i];
            rectImage.activelScaleImage();

            var Rect = T.GetComponent<RectTransform>();
            Rect.anchoredPosition = new Vector2(i * bttnDistance, 0f);
            Rect.localScale = new Vector3(1, 1, 1);


            rectImage.image.color = new Color(255, 255, 255, 0.2f);
            rectImage.image.GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
            rectImage.image.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 30));

            bttn[i] = T.GetComponent<Button>();
        }
    }
    public void createOnlyBuy()
    {
        var sum = 0;
        for (int i = 0; i < weapons.weapon.Length; i++)
        {
            if (weapons.weapon[i].checkBuy)
            {
                sum++;
            }
        }

        List<Weapon> btn = new List<Weapon>();

        for (int i = 0; i < weapons.weapon.Length; i++)
        {
            if (weapons.weapon[i].checkBuy)
            {
                btn.Add(weapons.weapon[i]);
            }
        }

        bttn = new Button[btn.Count];
        AllBtnGameObject = new GameObject[btn.Count];

        for (int i = 0; i < btn.Count; i++)
        {
            if (btn[i].checkBuy)
            {
                GameObject T = Instantiate(prefab);

                AllBtnGameObject[i] = T;

                T.transform.SetParent(Local);
                var rectImage = T.GetComponent<NewControllerBtn>();
                rectImage.Weapons = btn[i];
                rectImage.activelScaleImage();
                var Rect = T.GetComponent<RectTransform>();
                Rect.anchoredPosition = new Vector2(i * bttnDistance, 0f);
                rectImage.image.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 50));
                Rect.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                bttn[i] = T.GetComponent<Button>();
            }
        }

        bttnLength = bttn.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];
        if (bttn.Length > 1)
        {
            bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
        }
        
        for (int i = 0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            max = Mathf.Max(distance);
        }
    }
    /*
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
    }
    */
    private void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            var newMax = max - (bttnDistance * 2);
            if (distReposition[i] > newMax && bttn.Length > 5)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }

            var newMin = -max + (bttnDistance * 2);
            if (distReposition[i] < newMin && bttn.Length > 5)
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

                    var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                    infoBtn.text.gameObject.SetActive(true);
                    infoBtn.image.color = new Color(255, 255, 255, 1f);
                    bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
                    EquipP1();
                }
                else
                {
                    var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                    infoBtn.text.gameObject.SetActive(false);
                    infoBtn.image.color = new Color(255, 255, 255, 0.25f);
                    bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.6f, .6f, .6f);
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

        if (Mathf.Abs(newX) >= Mathf.Abs(position) - 1f && Mathf.Abs(newX) <= Mathf.Abs(position) + 1 && !messageSend)
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
        //btn.image.color = new Color(255, 255, 255, 1f);
        localWeaponsP1 = minButtonNum;
        localWeaponsP2 = minButtonNum;
    }
    void SendMessageFromButton(int btnIndex)
    {
        var btn = bttn[btnIndex].GetComponent<NewControllerBtn>();

        localWeaponsP1 = btnIndex;
        localWeaponsP2 = btnIndex;

        EquipP1();
    }
    public void EquipP1()
    {
        if (checkP1)
        {
            var btn = bttn[localWeaponsP1].GetComponent<NewControllerBtn>();

            if (btn.Weapons.checkBuy)
            {
                weapons.ResetAllWeaponP1();
                btn.Weapons.checkUseP1 = true;
                weapons.SaveWeapon();
            }
           /* else 
            {
                weapons.ResetAllWeaponP1();
                weapons.DefaultWeaponP1();
            }*/
        }
    }
    public void StartDrag()
    {
        messageSend = false;
        //activelBtnEquip = false;
        lerpSpeed = cloneLerpSpeed;
        dragging = true;

        targetNearestButton = true;

        if (checkP1)
        {
            localWeaponsP1 = 0;
        }

        if (checkP2)
        {
            localWeaponsP2 = 0;
        }
    }
    public void EndDrag()
    {
        dragging = false;
    }
    public void Next()
    {
        targetNearestButton = false;
        if (minButtonNum == bttn.Length - 1)
        {
            //minButtonNum = 0;

            for (int i = 0; i < bttn.Length; i++)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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
                if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
                {
                    sum++;
                }
            }

            if (sum > 1)
            {
                for (int i = minButtonNum + 1; i < bttn.Length; i++)
                {
                    if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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

        for (int a = 0; a < bttn.Length; a++)
        {
            if (a == minButtonNum)
            {
                a = minButtonNum;
                //Debug.Log(bttn[minButtonNum].name);

                checkNameAndUse();

                var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                infoBtn.text.gameObject.SetActive(true);
                infoBtn.image.color = new Color(255, 255, 255, 1f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
                EquipP1();
            }
            else
            {
                var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                infoBtn.text.gameObject.SetActive(false);
                infoBtn.image.color = new Color(255, 255, 255, 0.25f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.6f, .6f, .6f);
            }
        }

        checkNameAndUse();
        EquipP1();
    }
    public void Back()
    {
        targetNearestButton = false;
        if (minButtonNum == 0)
        {
            //minButtonNum = bttn.Length - 1;

            for (int i = bttn.Length - 1; i > 0; i--)
            {
                if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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
                if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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
                        if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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
                        if (bttn[i].GetComponent<NewControllerBtn>().Weapons.checkBuy)
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

        for (int a = 0; a < bttn.Length; a++)
        {
            if (a == minButtonNum)
            {
                a = minButtonNum;
                //Debug.Log(bttn[minButtonNum].name);

                checkNameAndUse();

                var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                infoBtn.text.gameObject.SetActive(true);
                infoBtn.image.color = new Color(255, 255, 255, 1f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
                EquipP1();
            }
            else
            {
                var infoBtn = bttn[a].GetComponent<NewControllerBtn>();
                infoBtn.text.gameObject.SetActive(false);
                infoBtn.image.color = new Color(255, 255, 255, 0.25f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.6f, .6f, .6f);
            }
        }

        checkNameAndUse();
        EquipP1();
    }
    public void StartRandom()
    {
        targetNearestButton = false;

        int randomTest = Random.Range(0, bttn.Length);
        minButtonNum = randomTest;

        for (int a = 0; a < bttn.Length; a++)
        {
            if (a == minButtonNum)
            {
                a = minButtonNum;
                checkNameAndUse();

                bttn[a].GetComponent<NewControllerBtn>().image.color = new Color(255, 255, 255, 1f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
                EquipP1();
            }
            else
            {
                bttn[a].GetComponent<NewControllerBtn>().image.color = new Color(255, 255, 255, 0.25f);
                bttn[a].GetComponent<RectTransform>().localScale = new Vector3(.6f, .6f, .6f);
            }
        }

        checkNameAndUse();
        EquipP1();
    }
    void getMaxDistance()
    {
        max = bttn.Length * bttnDistance;
    }
    private void OnEnable()
    {
        createOnlyBuy();

        if (bttn.Length > 0)
        {
            checkNameAndUse();
        }

        getMaxDistance();
    }
    private void OnDisable()
    {
        if (AllBtnGameObject != null)
        {
            for (int i = 0; i < AllBtnGameObject.Length; i++)
            {
                Destroy(AllBtnGameObject[i]);
            }

            AllBtnGameObject = null;
        }
       
    }
}
