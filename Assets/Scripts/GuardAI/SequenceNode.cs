using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public SequenceNode(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach(Node child in children)
        {
            NodeState childState = child.Evaluate();

            if(childState == NodeState.FAILURE)
            {
                state = NodeState.FAILURE;
                return state;
            }
            else if(childState == NodeState.RUNNING)
            {
                state = NodeState.RUNNING;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
