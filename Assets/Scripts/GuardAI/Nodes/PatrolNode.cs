using UnityEngine;
using UnityEngine.AI;

// This is just some basic random patrolling with a spgere
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

    // Sets new destination
    private void HandleRandomRoam()
        => agent.SetDestination(GetRandomPosition());

    // Handles when the navmeshagent reaches it's destination
    private void HandleDestinationReached()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    HandleRandomRoam();
    }

    // Gets a random position in the world
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
