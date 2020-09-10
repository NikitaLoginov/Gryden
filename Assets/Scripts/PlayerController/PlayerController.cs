using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Transform movePoint;
    public Vector3 movement;
    public bool isTurnEnded = false;

    float _moveSpeed = 5f;
    public float MoveSpeed { get { return _moveSpeed; } }

    PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; } }
    public readonly PlayerIdleState idleState = new PlayerIdleState();
    public readonly PlayerMoveState moveState = new PlayerMoveState();

    GameObject _movePointObject;

    // Pathfinding 
    [HideInInspector]
    public Transform startPos, endPos;
    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }
    public ArrayList pathArray;
    [HideInInspector]
    public float elapsedTime = 0.0f;
    [HideInInspector]
    public float intervalTime = 1.0f;

    private void Start()
    {
        _movePointObject = GameObject.Find("MovePoint");
        rb = this.GetComponent<Rigidbody2D>();
        movePoint = _movePointObject.GetComponent<Transform>();
        movePoint.parent = null;

        FindPath();

        TransitionToState(idleState);
    }

    private void Update()
    {
        //checking horizontal and vertical input to play from keyboard or joystick. need to implement touch\mouse movement as well.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        elapsedTime += Time.deltaTime;

        _currentState.PlayerUpdate(this);
    }
    private void FixedUpdate()
    {
        _currentState.PlayerFixedUpdate(this);
    }
    public void TransitionToState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    // Registering touch input
    public Vector2 RegisterTouch()
    {
        Vector2 touchPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            // destination node = touchPosition;
        }
        Debug.Log("touch position is " + touchPosition);
        return touchPosition;
    }

    public Vector2 RegisterMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log($"Position of mouse: {mousePosition}");
            return new Vector3 (mousePosition.x, mousePosition.y);
        }
        return new Vector2(rb.transform.position.x, rb.transform.position.y);

    }
    // finding path for player
    public void FindPath()
    {
        startPos = transform;
        endPos = startPos;
        endPos.position = RegisterMouseInput();

        StartNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(startPos.position)));
        GoalNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(endPos.position)));
        pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }

    // moving player
    public void MovePlayer()
    {
        if (pathArray == null)
            return;
        if (pathArray.Count > 0)
        {
            Node2D node2d = (Node2D)pathArray[1];
            rb.MovePosition(node2d.position);
            TurnHandler.Instance.TurnSwitcher();
        }
    }
    // Drawing gismos
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
                    Debug.DrawLine(node2d.position, nextNode.position, Color.blue); // draw line from current node to next node
                    index++;
                }
            }
        }
    }
}
