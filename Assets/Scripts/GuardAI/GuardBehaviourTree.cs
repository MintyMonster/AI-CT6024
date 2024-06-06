using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviourTree : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private Node topNode;
    public int currentGuardCount { get; set; } = 7;
    public LayerMask playerLayer;
    public LayerMask deadGuardLayer;
    public float sightRange = 10f;
    public float viewAngle = 120f;
    public float hearingRange = 10f;
    public int fleeDistance = 50;
    public int minGuardRequired = 2;
    public float health = 100f;
    public float Speed = 10f;
    private GuardViewCone viewCone;
    public float chaseRadius = 20f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        viewCone = GetComponent<GuardViewCone>();

        // Create Nodes
        Node patrolNode = new PatrolNode(agent, Speed);
        ChaseNode chaseNode = new ChaseNode(agent, player, chaseRadius, viewCone);
        Node hostileNode = new HostileNode(transform, player, sightRange, viewAngle, playerLayer, viewCone, chaseNode);
        Node curiousNode = new CuriousNode(agent, player, hearingRange);
        Node alertNode = new AlertNode(transform, deadGuardLayer);
        Node reinforceNode = new ReinforceNode(currentGuardCount, minGuardRequired);
        Node fleeNode = new FleeNode(fleeDistance, agent, player, health);

        // Behaviour tree
        topNode = new SelectorNode(new List<Node>
        {
            new SequenceNode(new List<Node> { alertNode, reinforceNode }),
            new SelectorNode(new List<Node>
            {
            hostileNode, // Use HostileNode directly
            patrolNode
            }),
            curiousNode,
            fleeNode
        });
    }

    // Update is called once per frame
    void Update()
    {
        topNode.Evaluate();
    }
}
