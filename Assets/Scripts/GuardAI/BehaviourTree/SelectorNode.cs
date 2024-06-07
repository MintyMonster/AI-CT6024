using System.Collections.Generic;

public class SelectorNode : Node
{
    private List<Node> children = new List<Node>();

    public SelectorNode(List<Node> children)
    {
        this.children = children;
    }

    // The basics behind the evaluate function
    public override NodeState Evaluate()
    {
        bool anyChildSucceeded = false;

        foreach (Node child in children)
        {
            NodeState childState = child.Evaluate();

            if (childState == NodeState.SUCCESS)
            {
                return NodeState.SUCCESS;
            }
            else if (childState == NodeState.RUNNING)
            {
                return NodeState.RUNNING;
            }
        }

        return anyChildSucceeded ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
