using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardViewCone : MonoBehaviour
{
    public float viewRadius = 20f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Transform player;
    public Light playerTorch;
    public LayerMask graveMask;

    private float lastSeenTime = 0f; // Variable to track the last time the player was seen
    private bool isTorchOn = false; // Is torch on?
    private Vector3 torchLightPosition; // Position where torch was seen

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 viewangleA = DirectionFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewangleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    private Vector3 DirectionFromAngle(float deg, bool global)
    {
        if (!global)
            deg += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad), Mathf.Cos(deg * Mathf.Deg2Rad));
    }

    public bool CanSeePlayer()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    return true;
            }
        }

        return false;
    }

    public bool PlayerWithinRadius(float radius)
    {
        return Vector3.Distance(transform.position, player.position) < radius;
    }

    public float LastSeenTime => lastSeenTime; // Getter for the lastSeenTime variable

    // Added 3D detection rather than just the floor eh
    public bool TorchVisible(out Vector3 torchPosition)
    {
        torchPosition = Vector3.zero;

        if (playerTorch.enabled)
        {
            RaycastHit hit;
            Vector3 directionToTorch = (player.position - transform.position).normalized;

            if(Physics.Raycast(player.position, playerTorch.transform.forward, out hit, viewRadius, obstructionMask))
            {
                Vector3 directionToHit = (hit.point - transform.position).normalized;

                if(Vector3.Angle(transform.forward, directionToHit) < viewAngle / 2)
                {
                    float distanceToHit = Vector3.Distance(transform.position, hit.point);

                    if(!Physics.Raycast(transform.position, directionToHit, distanceToHit, obstructionMask))
                    {
                        torchPosition = hit.point;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool GraveVisible(out Vector3 gravePosition)
    {
        Collider[] graveCollider = Physics.OverlapSphere(transform.position, viewRadius, graveMask);
        gravePosition = Vector3.zero;

        foreach(Collider grave in graveCollider)
        {
            Vector3 directionToGrave = (grave.transform.position - transform.position).normalized;
            float angleToGrave = Vector3.Angle(transform.forward, directionToGrave);

            if(angleToGrave < viewAngle / 2)
            {
                RaycastHit hit;
                if(!Physics.Raycast(transform.position, directionToGrave, out hit, viewRadius, obstructionMask))
                {
                    gravePosition = grave.transform.position;
                    return true;
                }
            }
        }

        return false;
    }

    private void Update()
    {
        // Update lastSeenTime when the player is seen
        if (CanSeePlayer())
        {
            lastSeenTime = Time.time;
        }

        if (playerTorch.enabled)
        {
            isTorchOn = true;
        }
        else
        {
            isTorchOn = false;
        }
    }
}