using System;
using UnityEngine;

public class Node2D : IComparable
{
    public float nodeTotalCost;
    public float estimatedCost;
    public bool bObstacle;
    public Node2D parent;
    public Vector2 position;

    public Node2D()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
    }
    public Node2D(Vector2 pos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }

    public int CompareTo(object obj)
    {
        Node2D node2d = (Node2D)obj;
        // Negative value means object comes before this in a sort order
        if (this.estimatedCost < node2d.estimatedCost)
            return -1;
        if (this.estimatedCost > node2d.estimatedCost)
            return 1;
        return 0;
    }
}
