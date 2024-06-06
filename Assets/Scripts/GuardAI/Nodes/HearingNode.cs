using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HearingNode : Node
{
    private Transform guard;
    private NavMeshAgent agent;
    private Transform player;
    private GuardViewCone viewCone;
    private bool heardNoise = false;
    private Vector3 noisePosition;

    public HearingNode(Transform guard, NavMeshAgent agent, Transform player, GuardViewCone viewCone)
    {
        this.guard = guard;
        this.agent = agent;
        this.player = player;
        this.viewCone = viewCone;
        CharacterControl.OnPlayerNoise += HandlePlayerNoise;
    }

    public override NodeState Evaluate()
    {
        if (heardNoise)
        {
            heardNoise = false;

            if (HasLOSToNoise())
            {
                TurnTowardsNoise();
                if (viewCone.CanSeePlayer())
                {
                    return NodeState.SUCCESS;
                }
            }
        }

        return NodeState.FAILURE;
    }

    private void HandlePlayerNoise(Vector3 position)
    {
        noisePosition = position;
        heardNoise = true;
    }

    private void TurnTowardsNoise()
    {
        Vector3 direction = (noisePosition - guard.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        guard.rotation = Quaternion.Slerp(guard.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private bool HasLOSToNoise()
    {
        Vector3 directionToNoise = (noisePosition - guard.position).normalized;
        float distanceToNoise = Vector3.Distance(guard.position, noisePosition);

        if(!Physics.Raycast(guard.position, directionToNoise, distanceToNoise, viewCone.obstructionMask))
        {
            return true;
        }

        return false;
    }
}
