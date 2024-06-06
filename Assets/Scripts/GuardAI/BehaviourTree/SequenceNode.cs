using System.Collections.Generic;

public class SequenceNode : Node
{
    private List<Node> children = new List<Node>();

    public SequenceNode(List<Node> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;

        foreach (Node child in children)
        {
            NodeState childState = child.Evaluate();

            if (childState == NodeState.RUNNING)
            {
                anyChildRunning = true;
            }
            else if (childState == NodeState.FAILURE)
            {
                return NodeState.FAILURE;
            }
        }

        return anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
    }
}
