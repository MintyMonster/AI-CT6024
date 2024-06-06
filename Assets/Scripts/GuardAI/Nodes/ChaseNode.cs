using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float chaseRadius;
    private GuardViewCone viewCone;
    private Vector3 lastKnownPosition;
    private float maxLostDuration = 5f; // Time before giving up on the chase and resuming patrolling

    public ChaseNode(NavMeshAgent agent, Transform playerTransform, float chaseRadius, GuardViewCone viewCone)
    {
        this.agent = agent;
        this.playerTransform = playerTransform;
        this.chaseRadius = chaseRadius;
        this.viewCone = viewCone;
        this.lastKnownPosition = agent.transform.position; // Set initial last known position to current agent position
    }

    public override NodeState Evaluate()
    {
        // If the player is within the chase radius, evaluate whether to continue chasing or not
        if (Vector3.Distance(agent.transform.position, playerTransform.position) <= chaseRadius)
        {
            if (viewCone.CanSeePlayer())
            {
                // Player is within chase radius and visible, continue chasing
                lastKnownPosition = playerTransform.position;
                agent.SetDestination(lastKnownPosition);
                agent.speed = 7f;
                return NodeState.RUNNING;
            }
            else
            {
                // Player is within chase radius but not visible, continue chasing last known position
                agent.SetDestination(lastKnownPosition);
                agent.speed = 7f;
                return NodeState.RUNNING;
            }
        }

        // If the player is outside the chase radius, check if the chase has timed out
        if (Time.time - viewCone.LastSeenTime > maxLostDuration)
        {
            Debug.Log("Lost sight of player, resuming patrol");
            agent.speed = 7f;
            ResumePatrol();
            return NodeState.FAILURE;
        }
        else
        {
            agent.speed = 7f;
            // Continue chasing the player's last known position
            agent.SetDestination(lastKnownPosition);
            return NodeState.RUNNING;
        }
    }


    // Method to resume patrolling
    private void ResumePatrol()
    {
        Debug.Log("Resuming patrol");
    }

    // Method to check if the node is running
    public bool IsRunning()
    {
        // The node is running if the player is within the chase radius and visible,
        // or if the player was seen recently and the chase has not timed out
        return Vector3.Distance(agent.transform.position, playerTransform.position) <= chaseRadius
            && viewCone.CanSeePlayer()
            || Time.time - viewCone.LastSeenTime <= maxLostDuration;
    }
}

