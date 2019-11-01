using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollision : MonoBehaviour
{
    void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = 0; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }


}
