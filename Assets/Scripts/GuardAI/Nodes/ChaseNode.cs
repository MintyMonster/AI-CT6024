using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{

    private NavMeshAgent agent;
    private Transform player;

    public ChaseNode(NavMeshAgent agent, Transform player)
    {
        this.agent = agent;
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        agent.SetDestination(player.position);
        state = NodeState.RUNNING;
        return state;
    }
}
