using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This broke the chasning alongside the other ones that broke it and didn't have time to fix
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

    // This basically searched in the viewcone to see if the AI could see a grave. If the AI could see a grave, it would stay alert, with a bigger detection radius. E.g "More alert"
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
