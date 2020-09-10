using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue2D 
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

    public Node2D First()
    {
        if (this.nodes.Count > 0)
            return (Node2D)this.nodes[0];

        return null;
    }

    public void Push(Node2D node)
    {
        this.nodes.Add(node);
        // Ensure the list is sorted
        this.nodes.Sort(); //Sort() calls Node's object re-arranged CompareTo method that sorts node according to estimated value
    }

    public void Remove(Node2D node)
    {
        this.nodes.Remove(node);
        this.nodes.Sort();
    }
}
