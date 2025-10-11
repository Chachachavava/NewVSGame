using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public float smoothSpeed = 0.05f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Advanced")]
    public bool useFixedUpdate = false;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (!useFixedUpdate)
            UpdateCamera();
    }

    void FixedUpdate()
    {
        if (useFixedUpdate)
            UpdateCamera();
    }

    void UpdateCamera()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );
    }
}