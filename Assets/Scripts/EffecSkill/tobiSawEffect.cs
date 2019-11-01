using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tobiSawEffect : MonoBehaviour
{
    public float force;
    public float timeToDestroy = 5f;
    [SerializeField] private float timeToChange = 1f;

    public Rigidbody2D rigi;
    public Pull pull;
    public GameObject prefab;
    private Vector2 test;

    public float timeToRotation;

    private Audio audioManager;
    public string nameSound;

    private void Start()
    {
        audioManager = Audio.instance;
        audioManager.PlaySound(nameSound);

        test = transform.right;

        pull = FindObjectOfType<Pull>();
        StartCoroutine(wait());
    }
    private void Update()
    {
        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0)
        {
            test = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeToChange = 1;
        }

        if (gameObject.tag == "skillP")
        {
            pull.BulletClone(test * 2f);
        }
        else if (gameObject.tag == "skillE")
        {
            pull.BulletClone2(test * 2f);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeToDestroy);
        audioManager.StopSound(nameSound);
        Destroy(gameObject);
        yield return 0;
    }
}
