using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNailOnBox : MonoBehaviour
{
    private float time;
    public Transform localOrigin;
    public Transform localDir;
    public float speedChange = 0.1f;
    public NewNails[] nails;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speedChange;
        if (((int)time) % 2 == 0)
        {
            for (int i = 0; i < nails.Length; i++)
            {
                if (nails[i].Type == 0)
                {
                    nails[i].local.position = new Vector2(nails[i].local.position.x, localDir.position.y);
                }
                else
                {
                    nails[i].local.position = new Vector2(nails[i].local.position.x, localOrigin.position.y);
                }
            }
        }
        else
        {
            for (int i = 0; i < nails.Length; i++)
            {
                if (nails[i].Type == 1)
                {
                    nails[i].local.position = new Vector2(nails[i].local.position.x, localDir.position.y);
                }
                else
                {
                    nails[i].local.position = new Vector2(nails[i].local.position.x, localOrigin.position.y);
                }
            }
        }
    }
}

[System.Serializable]
public class NewNails
{
    public Transform local;
    [Range(0, 1)]
    public int Type;
}
