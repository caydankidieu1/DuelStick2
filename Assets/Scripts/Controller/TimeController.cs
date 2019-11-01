using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowndownLength = 2f;
    public float timedelay = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowndownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        //Time.fixedDeltaTime = Time.timeScale * .02f;
        //Time.fixedDeltaTime = 0.02f;
        //StartCoroutine("wait");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timedelay);
        Time.fixedDeltaTime = 0.02f;
        yield return 0;
    }
}
