using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWave : MonoBehaviour
{
    public CreatePVP localGet;
    public GameObject waver;
    public Sprite myImage;
    public Image seft;

    private void Start()
    {
        seft = GetComponent<Image>();
    }

    private void Update()
    {
        seft.sprite = myImage;
    }

    public void showMap()
    {
        localGet.wave = waver;
    }
}
