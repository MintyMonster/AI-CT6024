using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceNode : Node
{
    private int currentGuardCount;
    private int minGuardRequired;

    public ReinforceNode(int currentGuardCount, int minGuardRequired)
    {
        this.currentGuardCount = currentGuardCount;
        this.minGuardRequired = minGuardRequired;
    }

    public override NodeState Evaluate()
    {
        // implement reinforce logic here

        state = NodeState.FAILURE; // Change to nodestate running upon implementation
        return state;
    }
}
