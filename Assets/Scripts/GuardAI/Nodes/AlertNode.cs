using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertNode : Node
{
    private Transform guard;
    private LayerMask deadGuardMask;

    public AlertNode(Transform guard, LayerMask deadGuardMask)
    {
        this.guard = guard;
        this.deadGuardMask = deadGuardMask;
    }

    public override NodeState Evaluate()
    {
        // Implement alert node logic here

        state = NodeState.FAILURE; // Change to nodestate running when implemented
        return state;
    }
}
