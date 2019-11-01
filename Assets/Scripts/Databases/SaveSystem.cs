using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    #region Setting
    public static void SaveSetting(SettingControllerUI setting)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Setting.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingData data = new SettingData(setting);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingData LoadSetting()
    {
        string path = Application.persistentDataPath + "/Setting.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingData data = formatter.Deserialize(stream) as SettingData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion

    #region Star
    public static void SaveStar(StarController star)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Star.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        StarData data = new StarData(star);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static StarData loadStar()
    {
        string path = Application.persistentDataPath + "/Star.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StarData data = formatter.Deserialize(stream) as StarData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion

    #region Weapon
    public static void SaveWeapon(WeaponStoreUI weapon)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Weapon.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        WeaponData data = new WeaponData(weapon);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static WeaponData LoadWeapon()
    {
        string path = Application.persistentDataPath + "/Weapon.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WeaponData data = formatter.Deserialize(stream) as WeaponData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion

    #region Skin
    public static void SaveSkin(SkinUI skin)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Skin.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        SkinData data = new SkinData(skin);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SkinData LoadSkin()
    {
        string path = Application.persistentDataPath + "/Skin.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SkinData data = formatter.Deserialize(stream) as SkinData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion

    #region Quest
    public static void SaveQuest(QuestControllerUI quest)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Quest.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        QuestData data = new QuestData(quest);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static QuestData LoadQuest()
    {
        string path = Application.persistentDataPath + "/Quest.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            QuestData data = formatter.Deserialize(stream) as QuestData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion

    #region Power
    public static void SavePower(PowerUI pow)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Power.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PowerData data = new PowerData(pow);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PowerData LoadPower()
    {
        string path = Application.persistentDataPath + "/Power.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PowerData data = formatter.Deserialize(stream) as PowerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }
    }
    #endregion
}
