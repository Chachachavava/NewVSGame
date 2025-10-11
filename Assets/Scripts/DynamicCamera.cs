using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform target;
    public float deadZoneWidth = 3.0f;
    public float deadZoneHeight = 2.0f;
    public float cameraStiffness = 0.1f;

    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 desiredCameraPosition;

    void Update()
    {
        if (target == null) return;

        Vector3 currentCameraPos = transform.position;
        Vector3 targetPos = target.position;
        Vector3 delta = targetPos - currentCameraPos;
        Vector3 push = Vector3.zero;

        if (Mathf.Abs(delta.x) > deadZoneWidth / 2)
        {
            float pushX = delta.x - (Mathf.Sign(delta.x) * deadZoneWidth / 2);
            push.x = pushX;
        }

        if (Mathf.Abs(delta.y) > deadZoneHeight / 2)
        {
            float pushY = delta.y - (Mathf.Sign(delta.y) * deadZoneHeight / 2);
            push.y = pushY;
        }

        desiredCameraPosition = currentCameraPos + push;
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredCameraPosition,
            ref cameraVelocity,
            cameraStiffness,
            Mathf.Infinity,
            Time.deltaTime
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZoneWidth, deadZoneHeight, 0));
    }
}