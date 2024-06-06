using UnityEngine;
using UnityEngine.AI;

public class PatrolNode : Node
{
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float patrolRadius;
    private float Speed = 3.5f;

    public PatrolNode(NavMeshAgent agent, float patrolRadius, float speed)
    {
        this.agent = agent;
        this.patrolRadius = patrolRadius;
        this.startPosition = agent.transform.position;
        this.Speed = speed;
    }

    public override NodeState Evaluate()
    {
        agent.speed = Speed;
        HandleDestinationReached();

        return NodeState.RUNNING;
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
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
            finalPos = hit.position;

        return finalPos;
    }
}
