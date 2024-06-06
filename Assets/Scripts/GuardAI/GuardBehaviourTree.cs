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


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Create Nodes
        Node patrolNode = new PatrolNode(agent);
        Node chaseNode = new ChaseNode(agent, player);
        Node hostileNode = new HostileNode(transform, player, sightRange, viewAngle, playerLayer);
        Node curiousNode = new CuriousNode(agent, player, hearingRange);
        Node alertNode = new AlertNode(transform, deadGuardLayer);
        Node reinforceNode = new ReinforceNode(currentGuardCount, minGuardRequired);
        Node fleeNode = new FleeNode(fleeDistance, agent, player, health);

        // Behaviour tree
        topNode = new SelectorNode(new List<Node>
        {
            new SequenceNode(new List<Node> { alertNode, reinforceNode }),
            new SequenceNode(new List<Node> { hostileNode, chaseNode }),
            new SequenceNode(new List<Node> { curiousNode }),
            new SequenceNode(new List<Node> { patrolNode, fleeNode })
        });
    }

    // Update is called once per frame
    void Update()
    {
        topNode.Evaluate();
    }
}
