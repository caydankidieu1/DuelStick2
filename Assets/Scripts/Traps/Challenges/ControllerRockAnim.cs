using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRockAnim : MonoBehaviour
{
    public float timeToBreak = 5f;
    public float timeToRestore = 5f;
    public Animator anim;
    [SerializeField] private bool checkUse;
    private float clone;

    private void Start()
    {
        clone = timeToBreak;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkUse)
        {
            timeToBreak -= Time.deltaTime;

            if (timeToBreak <= 0)
            {
                ActivelBreak();
                timeToBreak = 9999;
                StartCoroutine(wait());
            }
        }
    }

    public void ActivelBreak()
    {
        anim.SetBool("break", true);
    }

    public void ActivelRestore()
    {
        timeToBreak = clone;
        anim.SetBool("break", false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToRestore);
        ActivelRestore();
        yield return 0;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "player" || col.collider.tag == "enemy")
        {
            checkUse = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "player" || col.collider.tag == "enemy")
        {
            checkUse = false;
        }
    }
}
