public abstract class Node
{
    public enum NodeState { RUNNING, SUCCESS, FAILURE };

    public abstract NodeState Evaluate();
}
