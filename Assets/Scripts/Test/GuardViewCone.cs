using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardViewCone : MonoBehaviour
{
    public float viewRadius = 8f;
    [Range(0, 360)]
    public float viewAngle = 120f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Transform player;

    private float lastSeenTime = 0f; // Variable to track the last time the player was seen

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

        return new Vector3(Mathf.Sin(deg * Mathf.Deg2Rad), 0, Mathf.Cos(deg * Mathf.Deg2Rad));
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

    private void Update()
    {
        // Update lastSeenTime when the player is seen
        if (CanSeePlayer())
        {
            lastSeenTime = Time.time;
        }
    }
}