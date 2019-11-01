
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 2.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolum = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
    public bool loop;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.loop = loop;
        source.volume = volume * (1 + Random.Range(-randomVolum / 2f, randomVolum / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f)); ;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Mute(bool setMute)
    {
        source.mute = setMute;
    }
}

public class Audio : MonoBehaviour
{
    public static Audio instance;

    [SerializeField]
    Sound[] sounds;
    public Setting setting;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("ori");
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void TurnOnMusic()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Mute(false);
        }
    }
    public void TurnOffMusic()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Mute(true);
        }
    }
}
