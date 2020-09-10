using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTestMovementScript : MonoBehaviour
{
    Rigidbody _rb;
    Vector3 _transform;
    Transform startPos, endPos;
    public Node StartNode { get; set; }
    public Node GoalNode { get; set; }
    public ArrayList pathArray;

    GameObject objStartCube;
    float elapsedTime = 0.0f;
    public float intervalTime = 1.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        objStartCube = GameObject.FindGameObjectWithTag("Start");
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
        if (TurnHandler.Instance.isEnemyTurn)
        {
            MoveTelo();
        }
    }

    void FindPath()
    {
        startPos = transform;
        endPos = objStartCube.transform;

        StartNode = new Node(GridManager.Instance.GetGridCellCenter
                    (GridManager.Instance.GetGridIndex(startPos.position)));
        GoalNode = new Node(GridManager.Instance.GetGridCellCenter
                    (GridManager.Instance.GetGridIndex(endPos.position)));
        pathArray = AStar.FindPath(StartNode, GoalNode);
    }
    void MoveTelo()
    {
        if (pathArray == null) // if we didn't find path - return
            return;

        if (pathArray.Count > 0)
        {
            Node node = (Node)pathArray[1]; // defining next node in path
            _rb.MovePosition(node.position); // moving to next node in path (every time update() is fired
            TurnHandler.Instance.TurnSwitcher(); // swithing turns
        }
    }
}
