using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public SelectorNode(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach(Node child in children)
        {
            NodeState childState = child.Evaluate();

            if(childState == NodeState.SUCCESS)
            {
                state = NodeState.SUCCESS;
                return state;
            }
            else if(childState == NodeState.RUNNING)
            {
                state = NodeState.RUNNING;
                return state;
            }

        }

        state = NodeState.FAILURE;
        return state;
    }
}
