using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    private float accumulated = float.MaxValue;
    private GraphNode predecessor = null;

    public float Accumulated
    {
        get => accumulated;
        set => accumulated = value;
    }

    public GraphNode Predecessor
    {
        get => predecessor;
        set => predecessor = value;
    }

    public override bool Equals(object other)
    {
        if (other == null)
            return false;

        if (other is GraphNode graphNode)
            return GetInstanceID() == graphNode.GetInstanceID();

        return false;
    }

    public override int GetHashCode()
    {
        return gameObject.GetInstanceID();
    }
}