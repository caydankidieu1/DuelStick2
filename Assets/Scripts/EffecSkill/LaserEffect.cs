using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    public LayerMask layer;
    private LineRenderer _lineRenderer;
    public GameObject EndLaser;
    public GameObject PointDame;

    private Audio audioManager;
    public string nameSource;
    public float timetoLoop = 0.3f;
    private float cloneTime;

    void Start()
    {
        audioManager = Audio.instance;
        if (audioManager == null)
        {
            Debug.Log("No Found any Source");
        }

        PointDame.tag = gameObject.tag;
        _lineRenderer = GetComponent<LineRenderer>();
        timetoLoop = cloneTime;
    }

    void Update()
    {
        cloneTime -= Time.deltaTime;
        if (cloneTime <= 0)
        {
            audioManager.PlaySound(nameSource);
            cloneTime = timetoLoop;
        }
        

        _lineRenderer.SetPosition(0, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 100, layer);

        if (hit.collider)
        {
            _lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
            EndLaser.SetActive(true);
            PointDame.SetActive(true);
            EndLaser.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            PointDame.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.right * 2000);
            //_lineRenderer.SetPosition(1, transform.up * 2000);
            EndLaser.SetActive(false);
            PointDame.SetActive(false);
        }
    }
}
