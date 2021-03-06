﻿using System.Collections;
using UnityEngine;

public class PriorityQueue
{
    private ArrayList nodes = new ArrayList();

    public int Length
    {
        get { return this.nodes.Count; }
    }

    public bool Contains(object node)
    {
        return this.nodes.Contains(node);
    }

    public Node First()
    {
        if (this.nodes.Count > 0)
            return (Node)this.nodes[0];

        return null;
    }

    public void Push(Node node)
    {
        this.nodes.Add(node);
        // Ensure the list is sorted
        this.nodes.Sort(); //Sort() calls Node's object re-arranged CompareTo method that sorts node according to estimated value
    }

    public void Remove(Node node)
    {
        this.nodes.Remove(node);
        this.nodes.Sort();
    }
}
