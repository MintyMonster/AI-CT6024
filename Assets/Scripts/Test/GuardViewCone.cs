using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardViewCone : MonoBehaviour
{
    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 120f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Transform player;

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
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Distance(transform.position, player.position) < viewRadius)
            {
                if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
                {
                    if (!Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, viewRadius, obstructionMask))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool PlayerWithinRadius(float radius)
        => Vector3.Distance(transform.position, player.position) < radius;

}
