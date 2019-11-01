using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LocalForPVP : MonoBehaviour
{
    [Header("-------")]
    public CinemachineVirtualCamera camera;
    public Transform group;
    public Transform played1;
    public Transform played2;
    public bool checkEnemy;
    public int sumEnemy;
    public SystemDamage system;
    public ControllerPvP PVP;
    public WeaponManager weaponManager;

    [Header("value Weapon Drop")]
    public Transform[] localDropWeapon;
    public int numberAcceptWeapon = 2;
    public int numberW;
    public float timeToBornWeapon = 5f;
    public float timeToDestroyWeapon = 5f;
    private float timecloneBorn;
    private float timecloneDestroy;
    private bool checkToDestroyALL;
    public List<GameObject> AllWeapon = new List<GameObject>();

    [Header("value Pop-Up Player 1")]
    public int sumHitsP1;
    public int sumHitHeadsP1;
    public int comboP1; // so combo cao nhat ma trong wave dat duoc trong 3s

    [Header("value Pop-Up Player 2")]
    public int sumHitsP2;
    public int sumHitHeadsP2;
    public int comboP2; // so combo cao nhat ma trong wave dat duoc trong 3s

    [Space]
    [SerializeField]
    private int comboCloneP1;
    [SerializeField]
    private int comboCloneP2;

    [SerializeField] private float comboTestTimeP1;
    [SerializeField] private float comboTestTimeP2;
    private float timetest;
    [Header("Check who die")]
    public bool Player1Death;
    public bool Player2Death;

    [Header("Other VFX")]
    private Vector3 localClone;
    public GameObject VFX1;
    public GameObject VFX2;

    private void Start()
    {
        numberW = 0;
        timecloneBorn = timeToBornWeapon;
        timecloneDestroy = timeToDestroyWeapon;
        timetest = system.timeCombo;
        Player1Death = false;
        Player2Death = false;
        group.gameObject.SetActive(true);
    }
    private void Update()
    {
        camera.Follow = group.transform;
        camera.LookAt = null;
        

        SumComboP1();
        SumComboP2();

        if (Player1Death)
        {
            StartCoroutine(waitToSendP2());
        }
        else if (Player2Death)
        {
            StartCoroutine(waitToSendP1());
        }

        //CheckALlWeaponOnMap();
    }

    IEnumerator waitToSendP1()
    {
        yield return new WaitForSeconds(system.timeDelayWin + 1f);
        if (PVP)
        {
            PVP.Player1Win(sumHitHeadsP2, comboP2);
        }
        Player1Death = false;
        Player2Death = false;
        PVP = null;
        yield return 0;
    }
    IEnumerator waitToSendP2()
    {
        yield return new WaitForSeconds(system.timeDelayWin + 1f);
        if (PVP)
        {
            PVP.Player2Win(sumHitHeadsP1, comboP1);
        }
        Player1Death = false;
        Player2Death = false;
        PVP = null;
        yield return 0;
    }

    #region Player2 Info damage receive
    void deathP2() // neu Player 2 chết
    {
        Player2Death = true;
    }
    void sumHitP2() // nhan tu P2 so hit bi trung
    {
        sumHitsP2++;
        comboTestTimeP2 = timetest;

        if (comboTestTimeP2 >= 0)
        {
            comboCloneP2++;
        }
    }
    void SumComboP2()
    {
        comboTestTimeP2 -= Time.deltaTime;

        if (comboTestTimeP2 <= 0f)
        {
            if (comboCloneP2 > comboP2)
            {
                comboP2 = comboCloneP2;
                comboCloneP2 = 0;
            }
            else
            {
                comboCloneP2 = 0;
            }
        }
    }
    void sumHitHeadP2() // tinh tong so lan hit vao dau
    {
        sumHitHeadsP2++;
    }
    #endregion

    #region Player1 Info damage receive
    void deathP1() // neu Player 2 chết
    {
        Player1Death = true;
    }
    void sumHitP1()
    {
        sumHitsP1++;
        comboTestTimeP1 = timetest;

        if (comboTestTimeP1 >= 0)
        {
            comboCloneP1++;
        }
    }
    void SumComboP1()
    {
        comboTestTimeP1 -= Time.deltaTime;

        if (comboTestTimeP1 <= 0f)
        {
            if (comboCloneP1 > comboP1)
            {
                comboP1 = comboCloneP1;
                comboCloneP1 = 0;
            }
            else
            {
                comboCloneP1 = 0;
            }
        }
    }
    void sumHitHeadP1()
    {
        sumHitHeadsP1++;
    }
    #endregion

    #region DropWeapon
    public void CheckALlWeaponOnMap()
    {
        timeToBornWeapon -= Time.deltaTime;

        if (timeToBornWeapon <= 0)
        {
            var test = GetComponentsInChildren<WeaponController>();
            for (int i = 0; i < test.Length; i++)
            {
                if (test[i].tag == "unweapon")
                {
                    Debug.Log(test[i].name);
                    numberW++;
                }
            }

            if (numberW < numberAcceptWeapon)
            {
                //do drop weapon
                Debug.Log("ok");
                DropWeapon();

                checkToDestroyALL = false;
            }
            else
            {
                checkToDestroyALL = true;
            }

            numberW = 0;
            //numberW = 5;
            timeToBornWeapon = timecloneBorn;
        }

        if (checkToDestroyALL)
        {
            timeToDestroyWeapon -= Time.deltaTime;
            if (timeToDestroyWeapon <= 0)
            {
                var test = GetComponentsInChildren<WeaponController>();
                if (test.Length > 0)
                {
                    for (int i = 0; i < test.Length; i++)
                    {
                        if (test[i].tag == "unweapon")
                        {
                            VFX1.SetActive(false);
                            VFX2.SetActive(false);
                            AllWeapon.Add(test[i].gameObject);
                            test[i].gameObject.SetActive(false);
                        }
                    }
                }
                timeToDestroyWeapon = timecloneDestroy;
            }
        }
    }
    public void DropWeapon()
    {
        localClone = localDropWeapon[Random.Range(0, (int)localDropWeapon.Length)].transform.position;
        Weapon test = RandomWeaponOrigin();
        GameObject T = Instantiate(test.weaponPrefab, localClone, Quaternion.identity);

        T.transform.SetParent(gameObject.transform);
        T.transform.localScale = new Vector3(test.Scale.x, test.Scale.y, test.Scale.z);

        //
        VFX1.SetActive(true);
        VFX2.SetActive(true);

        VFX1.transform.position = new Vector3(localClone.x, localClone.y - 2, localClone.z);
        VFX2.transform.position = new Vector3(localClone.x, localClone.y - 2, localClone.z);
        T.transform.Rotate(0, 0, 50);
        T.GetComponent<SpriteRenderer>().enabled = true;
        T.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        T.tag = "unweapon";
        T.layer = 12;

        var col = T.GetComponents<Collider2D>();
        for (int i = 0; i < col.Length; i++)
        {
            col[i].isTrigger = true;
        }
    }
    public void DropWeapon2()
    {
        localClone = localDropWeapon[Random.Range(0, (int)localDropWeapon.Length)].transform.position;
        Weapon test = RandomWeaponOrigin();

        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            GameObject T = Instantiate(weaponManager.weaponItem[i].weaponPrefab, localClone, Quaternion.identity);
            T.gameObject.SetActive(false);
            T.transform.SetParent(gameObject.transform);
            T.transform.localScale = weaponManager.weaponItem[i].Scale;
            T.transform.Rotate(0, 0, 50);
            T.GetComponent<Collider2D>().isTrigger = true;
            T.GetComponent<SpriteRenderer>().enabled = true;
            T.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            T.tag = "unweapon";
        }

        //
        VFX1.SetActive(true);
        VFX2.SetActive(true);

        VFX1.transform.position = new Vector3(localClone.x, localClone.y - 2, localClone.z);
        VFX2.transform.position = new Vector3(localClone.x, localClone.y - 2, localClone.z);
    }
    GameObject RandomWeapon()
    {
        List<Weapon> testAllWeaponBuy = new List<Weapon>();
        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            if (weaponManager.weaponItem[i].checkBuy || weaponManager.weaponItem[i].cost <= 12500)
            {
                testAllWeaponBuy.Add(weaponManager.weaponItem[i]);
            }
        }

        //phai chac chan la luc nao cung phai co 1 vu khi da checkbuy

        Weapon weaponEnemys = testAllWeaponBuy[Random.Range(0, (int)testAllWeaponBuy.Count)];

        return weaponEnemys.weaponPrefab;
    }
    Weapon RandomWeaponOrigin()
    {
        List<Weapon> testAllWeaponBuy = new List<Weapon>();

        var test = 0;
        for (int i = 0; i < weaponManager.weaponItem.Length; i++)
        {
            if (weaponManager.weaponItem[i].checkBuy || weaponManager.weaponItem[i].cost <= 12500)
            {
                test++;
            }
        }

        Debug.Log("number :" + test);
        if (test > 1)
        {
            for (int i = 0; i < weaponManager.weaponItem.Length; i++)
            {
                if (weaponManager.weaponItem[i].checkBuy && weaponManager.weaponItem[i].id != "01" || weaponManager.weaponItem[i].cost <= 12500)
                {
                    testAllWeaponBuy.Add(weaponManager.weaponItem[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < weaponManager.weaponItem.Length; i++)
            {
                if (weaponManager.weaponItem[i].checkBuy /*&& weaponManager.weaponItem[i].id != "01"*/)
                {
                    testAllWeaponBuy.Add(weaponManager.weaponItem[i]);
                }
            }
        }


        Weapon weaponEnemys = testAllWeaponBuy[Random.Range(0, (int)testAllWeaponBuy.Count)];

        return weaponEnemys;
    }
    
    #endregion
}
