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

    // This took me longer than I care to admit
    private void OnDrawGizmos()
    {

        // This is just for me
        Gizmos.color = Color.yellow;

        Vector3 viewangleA = DirectionFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewangleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    // this is just a directional function with some maths (See me previous TankMaths library for some insight)
    // Updated from 2D to 3D for optimal stuffs
    private Vector3 DirectionFromAngle(float deg, bool global)
    {
        if (!global)
            deg += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad), Mathf.Cos(deg * Mathf.Deg2Rad));
    }

    // Check to see if the AI can see the player
    public bool CanSeePlayer()
    {
        // Get colliders
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // Ensure colliders array isnt empty
        if (rangeChecks.Length != 0)
        {
            // targeting and direction
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // get angle
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {

                // get distance
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Raycast, if hits palyer and nothing else, there you go
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    return true;
            }
        }

        return false;
    }

    // radius check
    public bool PlayerWithinRadius(float radius)
    {
        return Vector3.Distance(transform.position, player.position) < radius;
    }

    public float LastSeenTime => lastSeenTime; // Getter for the lastSeenTime variable

    // Added 3D detection rather than just the floor eh
    // This was a torch thing, but it broke the ChaseNode
    public bool TorchVisible(out Vector3 torchPosition)
    {
        // reset torch pos
        torchPosition = Vector3.zero;

        // check enabled
        if (playerTorch.enabled)
        {

            // raycasting
            RaycastHit hit;
            Vector3 directionToTorch = (player.position - transform.position).normalized;

            // Raycast from torch/player
            if(Physics.Raycast(player.position, playerTorch.transform.forward, out hit, viewRadius, obstructionMask))
            {
                // dir
                Vector3 directionToHit = (hit.point - transform.position).normalized;

                // angle
                if(Vector3.Angle(transform.forward, directionToHit) < viewAngle / 2)
                {

                    // distance from player to the hit
                    float distanceToHit = Vector3.Distance(transform.position, hit.point);

                    // Raycast
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


    // This also managed to break the chasenode?
    public bool GraveVisible(out Vector3 gravePosition)
    {
        // Get colliders
        Collider[] graveCollider = Physics.OverlapSphere(transform.position, viewRadius, graveMask);
        gravePosition = Vector3.zero;

        // loop colliders
        foreach(Collider grave in graveCollider)
        {
            // direction and angles
            Vector3 directionToGrave = (grave.transform.position - transform.position).normalized;
            float angleToGrave = Vector3.Angle(transform.forward, directionToGrave);

            // check if in viewcone
            if(angleToGrave < viewAngle / 2)
            {
                // Raycasting checks
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