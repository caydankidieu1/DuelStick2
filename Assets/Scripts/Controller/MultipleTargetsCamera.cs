using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetsCamera : MonoBehaviour
{
    #region Singleton
    public static MultipleTargetsCamera Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private Camera camera;
    [SerializeField] private float minDistanceForZoom = 10f;
    [SerializeField] private float maxPossibleDistance = 20f;
    [SerializeField] private float smoothing = 0.5f;
    [SerializeField] private float minSize = 7;
    [SerializeField] private float maxSize = 15;

    [SerializeField] private List<Transform> targets = new List<Transform>();

    private Vector3 velocity;

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        centerPoint.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            centerPoint,
            ref velocity,
            smoothing);
    }

    private void Zoom()
    {
        float greatestDistance = GetGreatestDistance();

        if (greatestDistance < minDistanceForZoom)
        {
            greatestDistance = 0f;
        }

        float newSize = Mathf.Lerp(minSize, maxSize, greatestDistance / maxPossibleDistance);

        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newSize, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        Bounds bounds = EncapsulateTargets();
        return bounds.size.x > bounds.size.y ? bounds.size.x : bounds.size.y;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = EncapsulateTargets();

        Vector3 center = bounds.center;
        //center.z = -10;

        return center;
    }

    private Bounds EncapsulateTargets()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }
}
