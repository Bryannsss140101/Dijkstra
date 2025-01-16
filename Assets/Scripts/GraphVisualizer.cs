using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PriorityQueue<T>
{
    private List<KeyValuePair<float, T>> elements = new();

    public int Count => elements.Count;

    public void Enqueue(float key, T value)
    {
        var pair = new KeyValuePair<float, T>(key, value);

        int index = elements.BinarySearch(pair,
            Comparer<KeyValuePair<float, T>>.Create((a, b) => b.Key.CompareTo(a.Key)));

        if (index < 0)
            index = ~index;

        elements.Insert(index, pair);
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("Queue are empty.");

        var item = elements[0].Value;

        elements.RemoveAt(0);

        return item;
    }
}

[System.Serializable]
public struct Edge
{
    public GraphNode from;
    public GraphNode to;
    public float weight;

    public Edge(GraphNode from, GraphNode to, float weight)
    {
        this.from = from;
        this.to = to;
        this.weight = weight;
    }
}

public class Graph
{
    private Dictionary<GraphNode, Dictionary<GraphNode, Edge>> adjacencyMatrix;
    private PriorityQueue<GraphNode> notVisited;

    public List<GraphNode> nodes => adjacencyMatrix.Keys.ToList();

    public Graph()
    {
        adjacencyMatrix = new();
        notVisited = new();
    }

    public void AddEdge(Edge edge)
    {
        var from = edge.from;
        var to = edge.to;
        var weight = edge.weight;

        if (!adjacencyMatrix.ContainsKey(from))
            adjacencyMatrix[from] = new();
        adjacencyMatrix[from][to] = new(from, to, weight);


        if (!adjacencyMatrix.ContainsKey(to))
            adjacencyMatrix[to] = new();
        adjacencyMatrix[to][from] = new(to, from, weight);
    }

    public void Dijkstra(GraphNode node)
    {
        node.Accumulated = 0f;

        notVisited.Enqueue(0f, node);

        while (notVisited.Count > 0)
        {
            var nodeAux = notVisited.Dequeue();

            foreach (var pair in adjacencyMatrix[nodeAux])
            {
                var edge = pair.Value;

                if (edge.to.Accumulated > nodeAux.Accumulated + edge.weight)
                {
                    edge.to.Accumulated = nodeAux.Accumulated + edge.weight;
                    edge.to.Predecessor = nodeAux;

                    notVisited.Enqueue(-edge.to.Accumulated, edge.to);
                }
            }
        }
    }

    public Stack<GraphNode> GetPath(GraphNode origin, GraphNode destination)
    {
        Stack<GraphNode> path = new();

        var node = destination;

        path.Push(node);

        while (path.Peek() != origin)
        {
            path.Push(node.Predecessor);
            node = node.Predecessor;
        }

        return path;
    }
}

public class GraphVisualizer : MonoBehaviour
{
    [SerializeField] private List<Edge> edges;
    [SerializeField] private GraphNode origin;
    [SerializeField] private GraphNode destination;

    private Graph graph;

    public Graph Graph { get => graph; }

    private void Awake()
    {
        graph = new();

        foreach (var edge in edges)
            graph.AddEdge(edge);

        graph.Dijkstra(origin);
    }

    private void OnDrawGizmos()
    {
        if (edges.Count <= 0)
            return;

        Gizmos.color = Color.red;

        foreach (var edge in edges)
        {
            if (edge.from == null || edge.to == null)
                return;

            Gizmos.DrawLine(
                edge.from.transform.position,
                edge.to.transform.position);
        }
    }

    public Vector3 GetNodePosition()
    {
        var nodes = graph.GetPath(origin, destination);

        if (nodes.Count > 0)
            return nodes.Pop().transform.position;

        return new();
    }
}