using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2DAIN : MonoBehaviour
{
    Rigidbody2D _rb2d;
    Transform startPos, endPos;
    GameObject _player;
    float elapsedTime = 0.0f;

    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }
    public bool HaveMoved { get; set; }
    public ArrayList pathArray;
    public float intervalTime = 1.0f;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        FindPath();
    }


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
            MoveBody();
        }
    }

    void FindPath()
    {
        startPos = transform;
        endPos = _player.transform;

        StartNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(startPos.position)));
        GoalNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(endPos.position)));
        pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }

    void MoveBody()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            Node2D node2d = (Node2D)pathArray[1];
            _rb2d.MovePosition(node2d.position);
            TurnHandler.Instance.TurnSwitcher();
        }
    }

    // draw path with Gismos
    void OnDrawGizmos()
    {
        if (pathArray == null) // if we didn't find path - return
            return;

        if (pathArray.Count > 0) // if path is longer that 0
        {
            int index = 1;      // indexator
            foreach (Node2D node2d in pathArray)
            {
                if (index < pathArray.Count) // if path has more than 1 node
                {
                    Node2D nextNode = (Node2D)pathArray[index]; // next node
                    Debug.DrawLine(node2d.position, nextNode.position, Color.green); // draw line from current node to next node
                    index++;
                }
            }
        }
    }
}
