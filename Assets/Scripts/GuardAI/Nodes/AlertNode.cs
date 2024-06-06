using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertNode : Node
{
    private bool alerted = false;
    private float alertDuration = 30f;
    private float alertEndTime;
    private GuardViewCone viewCone;

    public AlertNode(GuardViewCone viewCone)
    {
        this.viewCone = viewCone;
    }

    public override NodeState Evaluate()
    {
        Vector3 gravePosition;
        if(viewCone.GraveVisible(out gravePosition))
        {
            Debug.Log("Grave found");
            alertEndTime = Time.time + alertDuration;
            viewCone.viewRadius = 45f;
            return NodeState.SUCCESS;
        }
        else if(Time.time < alertEndTime)
        {
            viewCone.viewRadius = 45f;
            return NodeState.SUCCESS;
        }
        else
        {
            Debug.Log("Alert mode ended");
            return NodeState.FAILURE;
        }
    }
}
