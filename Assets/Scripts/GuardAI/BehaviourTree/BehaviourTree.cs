using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Needed additions:

/*
 * AI to Player shooting and visa versa
 * AI and player health
 * animations
 * sounds(?????)
 * Fleeing at low health
 * Reinforcements
 * Alert (guard death?)
 */

public class BehaviorTree : MonoBehaviour
{
    private Node topNode;
    private NavMeshAgent agent;
    public Transform playerTransform;
    private GuardViewCone viewCone;
    public float chaseRadius = 20f;
    public float rotationSpeed = 20f;
    public float sightRange = 10f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public float speed = 1f;

    private PatrolNode patrolNode;
    private HostileNode hostileNode;
    private ChaseNode chaseNode;
    private HearingNode hearingNode;
    private InvestigateNode investigationNode;
    private AlertNode alertNode;
    private KillPlayerNode killPlayerNode;

    public bool isDead = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        viewCone = GetComponent<GuardViewCone>();
        
        // Create nodes
        patrolNode = new PatrolNode(agent, 300f, speed);
        chaseNode = new ChaseNode(agent, playerTransform, chaseRadius, viewCone);
        hostileNode = new HostileNode(this.transform, playerTransform, sightRange, viewAngle, playerMask, viewCone, chaseNode, rotationSpeed);
        hearingNode = new HearingNode(agent.transform, agent, playerTransform, viewCone);
        investigationNode = new InvestigateNode(agent, viewCone, playerTransform);
        alertNode = new AlertNode(viewCone);
        killPlayerNode = new KillPlayerNode(this.transform, playerTransform);



        hostileNode.SetHearingNode(hearingNode);

        // Build behavior tree
        topNode = new SelectorNode(new List<Node>
        {
            new SequenceNode(new List<Node> { hostileNode, chaseNode, killPlayerNode, hearingNode }),
            patrolNode
        });
    }

    private void Update()
    {
        // Evaluate the behavior tree
        if (!isDead)
        {
            topNode.Evaluate();
        }

        /*
        Animator anim = this.transform.GetComponent<Animator>();

        if(agent.speed < 5)
        {
            anim.SetInteger("Transition", 0);
        } else if(agent.speed < 5)
        {
            anim.SetInteger("Transition", 1);
        }else if (isDead)
        {
            anim.SetInteger("Transition", 2);
        }
            */
    }
}
