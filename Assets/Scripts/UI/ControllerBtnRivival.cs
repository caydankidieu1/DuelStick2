using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBtnRivival : MonoBehaviour
{
    public Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        image.raycastTarget = false;
        StartCoroutine(wait());
    }

    void OnDisable()
    {
        image.raycastTarget = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        image.raycastTarget = true;
        yield return 0;
    }
}
