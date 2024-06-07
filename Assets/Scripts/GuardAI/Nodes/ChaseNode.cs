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
    private float maxLostDuration = 5f;

    public ChaseNode(NavMeshAgent agent, Transform playerTransform, float chaseRadius, GuardViewCone viewCone)
    {
        this.agent = agent;
        this.playerTransform = playerTransform;
        this.chaseRadius = chaseRadius;
        this.viewCone = viewCone;
        this.lastKnownPosition = agent.transform.position; // Set last known position
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
            ResumePatrol(); // Does nothing YET
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

    private void ResumePatrol()
    {
        Debug.Log("Resuming patrol");
    }

    // Method to check if the node is running
    public bool IsRunning()
    {
        // check if player is within chase radius, and the AI can see it. OR if the player was seen recently and thius hasn't timed out
        return Vector3.Distance(agent.transform.position, playerTransform.position) <= chaseRadius
            && viewCone.CanSeePlayer()
            || Time.time - viewCone.LastSeenTime <= maxLostDuration;
    }
}

