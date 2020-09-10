using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;

    GameObject objStartCube, objEndCube;
    float elapsedTime = 0.0f;
    //Interval time between pathfinding
    public float intervalTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

        pathArray = new ArrayList();
        FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }

       // Debug.Log("Number of rows " + (int)GridManager.Instance.numOfRows);
       // Debug.Log("Number of columns " + (int)GridManager.Instance.numOfColumns);
    }

    private void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        startNode = new Node(GridManager.Instance.GetGridCellCenter
                    (GridManager.Instance.GetGridIndex(startPos.position)));
        goalNode = new Node(GridManager.Instance.GetGridCellCenter
                    (GridManager.Instance.GetGridIndex(endPos.position)));

        pathArray = AStar.FindPath(startNode, goalNode);
    }

    // draw path with Gismos
    void OnDrawGizmos()
    {
        if (pathArray == null) // if we didn't find path - return
            return;

        if (pathArray.Count > 0) // if path is longer that 0
        {
            int index = 1;      // indexator
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count) // if path has more than 1 node
                {
                    Node nextNode = (Node)pathArray[index]; // next node
                    Debug.DrawLine(node.position, nextNode.position, Color.green); // draw line from current node to next node
                    index++; 
                }
            }
        }
    }
}
