using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNode : Node
{
    private Transform guard;
    private GuardViewCone viewCone;
    private ChaseNode chaseNode;
    

    public HostileNode(Transform guard, Transform player, float sightRange, float viewAngle, LayerMask layerMask, GuardViewCone viewCone, ChaseNode chaseNode)
    {
        this.guard = guard;
        this.viewCone = viewCone;
        this.chaseNode = chaseNode;
    }

    public override NodeState Evaluate()
    {
        if (viewCone.CanSeePlayer())
        {
            Shoot();
            return NodeState.SUCCESS;
        }
        else if (chaseNode.playerLost)
        {
            Debug.Log("Player lost, transitioning to patrol");
            chaseNode.SetPlayerLost(false); // Reset playerLost flag
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Shooting at player!");

        // If the player is not in sight after shooting, set the playerLost flag
        if (!viewCone.CanSeePlayer())
        {
            Debug.Log("Player lost after shooting, transitioning to patrol");
            chaseNode.SetPlayerLost(true);
        }
    }
}
