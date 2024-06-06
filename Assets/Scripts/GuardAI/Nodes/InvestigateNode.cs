using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigateNode : Node
{
    private NavMeshAgent agent;
    private GuardViewCone viewCone;
    private Transform player;
    private float investigationDuration = 10f;

    private float investigationStartTime;
    private bool isInvestigating = false;
    private Vector3 torchPosition;

    public InvestigateNode(NavMeshAgent agent, GuardViewCone viewCone, Transform player)
    {
        this.agent = agent;
        this.viewCone = viewCone;
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        if (viewCone.TorchVisible(out torchPosition))
        {
            if (!isInvestigating)
            {
                StartInvestigation();
            }
            else if(Time.time - investigationStartTime >= investigationDuration)
            {
                StopInvestigation();
                return NodeState.FAILURE;
            }

            return NodeState.RUNNING;
        }

        return NodeState.FAILURE;
    }

    private void StartInvestigation()
    {
        isInvestigating = true;
        investigationStartTime = Time.time;
        agent.SetDestination(torchPosition);
    }

    private void StopInvestigation()
    {
        isInvestigating = false;
    }
}
