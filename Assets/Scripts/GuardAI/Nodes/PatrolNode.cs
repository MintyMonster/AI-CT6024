using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNode : Node
{
    private NavMeshAgent agent;
    private float walkRadius = 300f;
    public bool canMove { get; set; } = true;
    public float Speed { get; set; } = 35f;

    public PatrolNode(NavMeshAgent agent)
    {
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {

        if (canMove)
        {
            agent.speed = Speed;
            HandleDestinationReached();
        }

        state = NodeState.RUNNING;
        return state;

    }

    private void HandleRandomRoam()
        => agent.SetDestination(GetRandomPosition());

    private void HandleDestinationReached()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    HandleRandomRoam();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
            finalPos = hit.position;

        return finalPos;
    }


}
