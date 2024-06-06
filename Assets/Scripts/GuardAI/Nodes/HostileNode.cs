using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNode : Node
{
    private Transform guard;
    private Transform player;
    private float sightRange;
    private float viewAngle;
    private LayerMask layerMask;

    public HostileNode(Transform guard, Transform player, float sightRange, float viewAngle, LayerMask layerMask)
    {
        this.guard = guard;
        this.player = player;
        this.sightRange = sightRange;
        this.viewAngle = viewAngle;
        this.layerMask = layerMask;
    }

    public override NodeState Evaluate()
    {
        // Sight logic here

        state = NodeState.FAILURE; // Change this to running upon implementation
        return state;
    }
}
