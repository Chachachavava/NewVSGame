using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform target;
    public float maxPullDistance = 5.0f;
    public float pullStrength = 2.0f;
    public float returnSpeed = 3.0f;
    public float stoppingThreshold = 0.1f;

    private Vector3 cameraVelocity = Vector3.zero;
    private bool isReturningToCenter = false;
    private float stoppingTimer = 0f;
    public float stoppingTimeRequired = 0.5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;
        targetPos.z = -10f;
        Vector3 toTarget = targetPos - currentPos;
        float distanceToTarget = toTarget.magnitude;
        float targetSpeed = GetTargetSpeed();
        if (targetSpeed > stoppingThreshold)
        {
            stoppingTimer = 0f;
            isReturningToCenter = false;
        }
        else
        {
            stoppingTimer += Time.deltaTime;
            if (stoppingTimer >= stoppingTimeRequired)
            {
                isReturningToCenter = true;
            }
        }

        Vector3 desiredPosition;

        if (isReturningToCenter)
        {
            desiredPosition = Vector3.Lerp(currentPos, targetPos, returnSpeed * Time.deltaTime);
        }
        else
        {
            if (distanceToTarget > maxPullDistance)
            {
                desiredPosition = targetPos - toTarget.normalized * maxPullDistance;
            }
            else
            {
                float pullFactor = Mathf.Clamp01(distanceToTarget / maxPullDistance);
                desiredPosition = currentPos + toTarget * pullFactor * pullStrength * Time.deltaTime;
            }
        }
        desiredPosition.z = -10f;

        transform.position = Vector3.SmoothDamp(currentPos, desiredPosition, ref cameraVelocity, 0.01f);
    }

    private Vector3 lastTargetPos;
    private float GetTargetSpeed()
    {
        if (target == null) return 0f;

        float speed = (target.position - lastTargetPos).magnitude / Time.deltaTime;
        lastTargetPos = target.position;
        return speed;
    }

    void OnDrawGizmosSelected()
    {
        if (target == null) return;

        Gizmos.color = isReturningToCenter ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, maxPullDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }
}