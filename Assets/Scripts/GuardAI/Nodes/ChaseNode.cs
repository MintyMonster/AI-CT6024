using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{

    private NavMeshAgent agent;
    private Transform player;
    private float chaseRadius;
    private GuardViewCone viewCone;
    public bool playerLost = true;

    public ChaseNode(NavMeshAgent agent, Transform player, float chaseRadius, GuardViewCone viewCone)
    {
        this.agent = agent;
        this.player = player;
        this.viewCone = viewCone;
        this.chaseRadius = chaseRadius;
    }

    public override NodeState Evaluate()
    {

        if (!playerLost && (!viewCone.CanSeePlayer() && viewCone.PlayerWithinRadius(chaseRadius)))
        {
            Debug.Log("Chasing the player");
            agent.SetDestination(player.position);
            state = NodeState.RUNNING;
        }
        else if (viewCone.CanSeePlayer())
        {
            Debug.Log("Player is in sight again, stopping chase");
            state = NodeState.SUCCESS;
        }
        else
        {
            Debug.Log("Player out of chase radius, stopping chase");
            agent.SetDestination(agent.transform.position); // Stop chasing
            state = NodeState.FAILURE;
        }
        return state;
    }

    public void SetPlayerLost(bool lost)
    {
        playerLost = lost;
    }
}
