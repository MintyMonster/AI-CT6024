using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// broken node
// Makes chasenode walk slowly?
public class KillPlayerNode : Node
{
    private Transform guard;
    private Transform player;
    private float killDistance = 5f;

    public KillPlayerNode(Transform guard, Transform player)
    {
        this.guard = guard;
        this.player = player;
    }

    // When the AI reached within a distance of the player, kill the player.
    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(guard.position, player.position);

        if(distance <= killDistance)
        {
            Debug.Log("Player killed");
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }


}
