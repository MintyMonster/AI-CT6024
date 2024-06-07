using UnityEngine;
using UnityEngine.AI;

// Slowly losing my mind...
public class HostileNode : Node
{
    // variables
    private Transform guard;
    private Transform player;
    private float sightRange;
    private float viewAngle;
    private LayerMask layerMask;
    private GuardViewCone viewCone;
    private ChaseNode chaseNode;
    private float rotationSpeed;
    private HearingNode hearingNode;

    // constructor
    public HostileNode(Transform guard, Transform player, float sightRange, float viewAngle, LayerMask layerMask, GuardViewCone viewCone, ChaseNode chaseNode, float rotationSpeed)
    {
        this.guard = guard;
        this.player = player;
        this.sightRange = sightRange;
        this.viewAngle = viewAngle;
        this.layerMask = layerMask;
        this.viewCone = viewCone;
        this.chaseNode = chaseNode;
        this.rotationSpeed = rotationSpeed;
    }

    // Setting the hearing node seperately because putting it in the constructor broke things?
    public void SetHearingNode(HearingNode hearingNode)
    {
        this.hearingNode = hearingNode;
    }


    // Regular evaluate stuff
    public override NodeState Evaluate()
    {
        // Check if player can be seen
        if (viewCone.CanSeePlayer())
        {
            StopAgent();
            RotateTowardsPlayer();
            Shoot();
            return NodeState.RUNNING;
        }
        else if (hearingNode != null && hearingNode.Evaluate() == NodeState.SUCCESS)
        {
            StopAgent();
            return NodeState.RUNNING;
        }
        else
        {
            ResumeAgent();
            return NodeState.FAILURE;
        }
    }

    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Shooting at player!");
    }

    // Self explanator via name
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - guard.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        guard.rotation = Quaternion.Slerp(guard.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void StopAgent()
    {
        
    }

    private void ResumeAgent()
    {
        
    }
}