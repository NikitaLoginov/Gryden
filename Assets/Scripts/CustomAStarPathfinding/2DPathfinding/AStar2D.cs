using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar2D : MonoBehaviour
{
    public static PriorityQueue2D openList;
    public static HashSet<Node2D> closeList;

    static float HeuristicEstimateCost(Node2D curNode, Node2D goalNode)
    {
        Vector2 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude;
    }

    public static ArrayList FindPath(Node2D start, Node2D goal)
    {
        openList = new PriorityQueue2D();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);

        closeList = new HashSet<Node2D>();
        Node2D node = null;

        while (openList.Length != 0)
        {
            node = openList.First();
            // Check if the current node is a target node
            if (node.position == goal.position)
                return CalculatePath2D(node);
            // Create an ArrayList to store neighboring nodes
            ArrayList neighbours = new ArrayList();

            GridManager2D.Instance.GetNeighbours(node, neighbours);

            for (int i = 0; i < neighbours.Count; i++)
            {
                Node2D neighbourNode = (Node2D)neighbours[i];

                if (!closeList.Contains(neighbourNode))
                {
                    float cost = HeuristicEstimateCost(node, neighbourNode);
                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;

                    if (!openList.Contains(neighbourNode))
                        openList.Push(neighbourNode);
                }
            }

            // Add the current node to the closed list
            closeList.Add(node);
            // and remove it from openList
            openList.Remove(node);
        }

        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found!");
            return null;
        }
        return CalculatePath2D(node);
    }

    public static ArrayList CalculatePath2D(Node2D node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}
