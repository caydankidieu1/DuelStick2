using UnityEngine;

public class LoadData : MonoBehaviour
{
    public StarController starController;
    public SkinUI skinUI;
    public WeaponStoreUI weaponStoreUI;
    public SettingControllerUI settingControllerUI;
    public QuestControllerUI questControllerUI;
    public PowerUI powerUI;

    private void Awake()
    {
        //starController.LoadStar();
        skinUI.LoadSkin();
        weaponStoreUI.Loadweapon();
        settingControllerUI.LoadSetting();
        questControllerUI.LoadQuest();
        powerUI.LoadData();
    }

    public void SaveAndLoadSkins()
    {
        skinUI.SaveSkin();
        skinUI.LoadSkin();
    }
    public void SaveAndLoadWeapon()
    {
        weaponStoreUI.SaveWeapon();
        weaponStoreUI.Loadweapon();
    }
    public void SaveAndLoadPower()
    {
        powerUI.SaveData();
        powerUI.LoadData();
    }

    public void SaveAndLoadQuest()
    {
        questControllerUI.SaveQuest();
        questControllerUI.LoadQuest();
    }

    public void resetSkin()
    {
        skinUI.Reset();
    }
    public void resetWeapon()
    {
        weaponStoreUI.Reset();
    }
    public void resetQuest()
    {
        questControllerUI.ResetQuest();
    }
}
