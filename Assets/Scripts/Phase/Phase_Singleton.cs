using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_Singleton : MonoBehaviour
{
    private static Phase_Singleton _instance = null;

    public static Phase_Singleton Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
