using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CuriousNode : Node
{
    private NavMeshAgent agent;
    private Transform player;
    private float hearingRange;

    public CuriousNode(NavMeshAgent agent, Transform player, float hearingRange)
    {
        this.agent = agent;
        this.player = player;
        this.hearingRange = hearingRange;
    }

    public override NodeState Evaluate()
    {
        // Implement curious logic here

        state = NodeState.FAILURE; // Change this to running upon implementation
        return state;
    }
}