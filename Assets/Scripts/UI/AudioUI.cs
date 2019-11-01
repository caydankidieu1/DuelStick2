using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    private Audio audioManager;
    public string nameAudio1;

    private void Start()
    {
        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.Log("No found any Source");
        }
    }

    public void onClick()
    {
        if (nameAudio1 != null)
        {
            audioManager.PlaySound(nameAudio1);
        }
    }
}
