using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivelAudioEffect2 : MonoBehaviour
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

    private void OnDisable()
    {
        if (nameAudio != null)
        {
            audioManager.StopSound(nameAudio);
        }
    }
}
