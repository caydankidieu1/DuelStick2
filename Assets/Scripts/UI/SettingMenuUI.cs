using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingMenuUI : MonoBehaviour
{
    public static bool GameIsBattled = false;
    public static bool GameIsHomed = false;
    public static bool GameIsDailly = false;
    public static bool GameIsPaused = false;

    [Space]
    public GameObject HomeMenuUI;
    [Space]


    public GameObject BattleUI;
    public GameObject BattleUIPVP;
    public GameObject MainScreenUI;
    public GameObject LukySpinUI;
    public GameObject GameMenuUI; //pauseUI

    public GameObject popUp;
    public GameObject popUpPVP;

    private SettingHomeUI settingHomeUI;
    private BattleUI battleUI;

    [Header("----------------------------------")]
    public WeaponStoreUI weaponStoreUI;
    public SkinUI skinUI;
    public SystemDamage system;

    private BattleUI test;
    public BattleUI allBattleUI;

    private void Start()
    {
        defaultUI();
        settingHomeUI = GetComponentInChildren<SettingHomeUI>();
        battleUI = GetComponentInChildren<BattleUI>();
    }
    public void checkPauseMenu()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Resume()
    {
        GameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        GameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Home()
    {
        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(true);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);
        GameIsHomed = true;

        if (FindObjectOfType<Local>())
        {
            Destroy(FindObjectOfType<Local>().gameObject);
        }
        if (FindObjectOfType<LocalForPVP>())
        {
            Destroy(FindObjectOfType<LocalForPVP>().gameObject);
        }
        if (FindObjectOfType<LocalForSurvival>())
        {
            Destroy(FindObjectOfType<LocalForSurvival>().gameObject);
        }

        Time.timeScale = 1f;
        settingHomeUI.backHome();

        activelAnimatorHome();
    }
    public void HomeClone()
    {
        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(true);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);
        GameIsHomed = true;

        Time.timeScale = 1f;
        settingHomeUI.Adventure();

        activelAnimatorHome();
    }
    public void HomeCloneOnStore()
    {
        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(true);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);
        GameIsHomed = true;

        Time.timeScale = 1f;
        settingHomeUI.CloneOpenIAP2();

        activelAnimatorHome();
    }
    public void GamePlayUI()
    {
        BattleUI.SetActive(true);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(false);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);
    }
    public void GamePlayUIPVP()
    {
        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(true);
        MainScreenUI.SetActive(false);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);
    }
    public void ActivelLucky()
    {
        /*BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(false);
        LukySpinUI.SetActive(true);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);*/

        if (GetComponent<SettingHomeUI>().rightHome.activeSelf)
        {
            activelAnimatorHome();
        }
        StartCoroutine(CloneLuck());
    }
    public IEnumerator CloneLuck()
    {
        yield return new WaitForSeconds(0.75f);

        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(false);
        LukySpinUI.SetActive(true);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);

        yield return 0;
    }
    void defaultUI()
    {
        HomeMenuUI.SetActive(true);
        MainScreenUI.SetActive(true);
        BattleUIPVP.SetActive(false);
        BattleUI.SetActive(false);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);

        string pathSkin = Application.persistentDataPath + "/Skin.dat";
        if (File.Exists(pathSkin))
        {
            skinUI.LoadSkin();
        }

        string pathWeapon = Application.persistentDataPath + "/Weapon.dat";
        if (File.Exists(pathWeapon))
        {
            weaponStoreUI.Loadweapon();
        }
    }
    public void ChoiceLevel()
    {
        BattleUI.SetActive(false);
        BattleUIPVP.SetActive(false);
        MainScreenUI.SetActive(true);
        LukySpinUI.SetActive(false);
        GameMenuUI.SetActive(false);
        popUp.SetActive(false);
        popUpPVP.SetActive(false);

        if (FindObjectOfType<Local>())
        {
            Destroy(FindObjectOfType<Local>().gameObject);
        }
        if (FindObjectOfType<LocalForPVP>())
        {
            Destroy(FindObjectOfType<LocalForPVP>().gameObject);
        }
        if (FindObjectOfType<LocalForSurvival>())
        {
            Destroy(FindObjectOfType<LocalForSurvival>().gameObject);
        }

        Time.timeScale = 1f;
        settingHomeUI.Adventure();
    }
    public void Reload()
    {
        Time.timeScale = 1;
        if (FindObjectOfType<Local>())
        {
            popUp.SetActive(false);
            popUpPVP.SetActive(false);
            FindObjectOfType<Local>().Phase.ReloadWave();
        }
        if (FindObjectOfType<LocalForPVP>())
        {
            popUp.SetActive(false);
            popUpPVP.SetActive(false);
            GetComponent<CreatePVP>().ReloadWavePVP();
        }
        if (FindObjectOfType<LocalForSurvival>())
        {
            popUp.SetActive(false);
            popUpPVP.SetActive(false);
            GetComponent<CreateSurvival>().ReloadWaveSur();
        }
    }
    public void Reload2()
    {
        ManagerAds.Ins.ShowRewardedVideo(isSuccess =>
        {
            if (isSuccess)
            {
                Time.timeScale = 1;
                if (FindObjectOfType<Local>())
                {
                    popUp.SetActive(false);
                    popUpPVP.SetActive(false);
                    FindObjectOfType<Local>().Phase.RivivalPlayer();
                }

                if (FindObjectOfType<LocalForSurvival>())
                {
                    popUp.SetActive(false);
                    popUpPVP.SetActive(false);
                    GetComponent<CreateSurvival>().RivivalPlayer();
                }

                //
                system.trueDeath = false;
                system.loser = false;
                //

                allBattleUI.activelChangeTest();

            }
            else
            {
                allBattleUI.time = 0;
                allBattleUI.activelChangeTest();
                allBattleUI.GameOver3();
            }
        });
    }
    public void activelAnimatorHome()
    {
        if (MainScreenUI != null)
        {
            Animator animator = MainScreenUI.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");

                animator.SetBool("open", !isOpen);
            }
        }
    }
    public void activelClone()
    {
        if (MainScreenUI != null)
        {
            Animator animator = MainScreenUI.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("open", false);
            }
        }
    }
    public void activelClone2()
    {
        if (MainScreenUI != null)
        {
            Animator animator = MainScreenUI.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("open", true);
            }
        }
    }
}
