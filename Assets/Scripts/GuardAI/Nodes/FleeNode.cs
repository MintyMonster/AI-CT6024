using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeNode : Node
{
    private int fleeDistance;
    private NavMeshAgent agent;
    private Transform player;
    private float health;

    public FleeNode(int fleeDistance, NavMeshAgent agent, Transform player, float health)
    {
        this.fleeDistance = fleeDistance;
        this.agent = agent;
        this.player = player;
        this.health = health;
    }

    public override NodeState Evaluate()
    {
        // Implement fleeing logic

        state = NodeState.FAILURE;
        return state;
    }
}
