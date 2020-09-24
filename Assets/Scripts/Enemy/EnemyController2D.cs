using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public Transform startPos, endPos;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public float elapsedTime = 0.0f;

    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }
    public ArrayList pathArray;
    [HideInInspector]
    public float intervalTime = 1.0f;

    EnemyBaseState _currentState;
    public EnemyBaseState CurrentState { get { return _currentState; } }
    public readonly EnemyIdleState idleState = new EnemyIdleState();
    public readonly EnemyMoveState moveState = new EnemyMoveState();
    public readonly EnemyAttackState attackState = new EnemyAttackState();

    //enemy stats
    float _startEnemyHP = 1;
    public float StartEnemyHP
    { 
        get { return _startEnemyHP; }
        set { _startEnemyHP = value; }
    }

    public float currentEnemyHP;

    float _enemyDMG = 1;
    public float EnemyDamage
    {
        get { return _enemyDMG; }
        set { _enemyDMG = value; }
    }

    void Start()
    {
        currentEnemyHP = _startEnemyHP;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        FindPath();

        TransitionToState(idleState);
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        _currentState.EnemyUpdate(this);

        DropDead();

    }

    private void FixedUpdate()
    {
        _currentState.EnemyFixedUpdate(this);
    }

    public void FindPath()
    {
        startPos = transform;
        endPos = player.transform;

        StartNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(startPos.position)));
        GoalNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(endPos.position)));
        pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }

    public void MoveBody()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            Node2D node2d = (Node2D)pathArray[1];
            rb2d.MovePosition(node2d.position);
            TurnHandler.Instance.TurnSwitcher();
        }
    }

    public void TransitionToState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    void DropDead()
    {
        if (currentEnemyHP <= 0)
        {
            //play animation
            Destroy(gameObject);
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
