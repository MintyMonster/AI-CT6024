using UnityEngine;
using UnityEngine.AI;

public class HostileNode : Node
{
    private Transform guard;
    private Transform player;
    private float sightRange;
    private float viewAngle;
    private LayerMask layerMask;
    private GuardViewCone viewCone;
    private ChaseNode chaseNode;
    private float rotationSpeed;
    private HearingNode hearingNode;

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

    public void SetHearingNode(HearingNode hearingNode)
    {
        this.hearingNode = hearingNode;
    }

    public override NodeState Evaluate()
    {
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