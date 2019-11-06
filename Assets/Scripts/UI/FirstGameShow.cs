using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGameShow : MonoBehaviour
{
    public CanvasGroup canvas;
    void Awake()
    {
        var test = PlayerPrefs.GetString(Tags.beginAll);

        if (test == "true")
        {
            ManagerAds.Ins.ShowBanner();
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
        }
        else if (test == "false" || test == null)
        {
            ManagerAds.Ins.HideBanner();
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey(Tags.beginAll);
    }
}
