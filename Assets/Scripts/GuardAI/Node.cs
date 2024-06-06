using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public enum NodeState { SUCCESS, RUNNING, FAILURE }
    public NodeState state;

    public NodeState State { get { return state; } }

    public abstract NodeState Evaluate();
}
