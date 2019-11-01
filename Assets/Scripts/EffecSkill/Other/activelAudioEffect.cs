using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activelAudioEffect : MonoBehaviour
{
    private Audio audioManager;
    public string nameAudio;

    void Start()
    {
        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.LogError("No found Audio Manager");
        }

        if (nameAudio != null)
        {
            audioManager.PlaySound(nameAudio);
        }
    }
}
