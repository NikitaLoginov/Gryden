using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{
    Rigidbody2D _rb;
    int _clickCounter = 0;

    Transform startPos;
    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }
    public ArrayList pathArray;

    //player stats
    float _startPlayerHP = 3.0f;

    public float currentPlayerHP;

    float _playerDMG = 1;
    public float PlayerDamage
    {
        get { return _playerDMG; }
        set { _playerDMG = value; }
    }

    //Enemy
    GameObject enemy;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        currentPlayerHP = _startPlayerHP;
    }

    void Update()
    {
        if (TurnHandler.Instance.isPlayersTurn)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                FindPath();
                _clickCounter++;

                if (Input.GetMouseButtonDown(0) && _clickCounter > 0 && CheckIfCanBump())
                {
                    BumpAttack();
                }
                if (Input.GetMouseButtonDown(0) && _clickCounter > 1)
                {
                    MovePlayer();
                    Debug.Log("Player Moved");
                    _clickCounter = 0;
                    TurnHandler.Instance.TurnSwitcher();
                }
            }
        }
        //check if dead
        GameHandler.Instance.DropDead(currentPlayerHP, this.gameObject);
    }

   
    private bool CheckIfCanBump() // checking if enemy is in bump range
    {
        if (GameHandler.Instance.playerCanAttackUp || GameHandler.Instance.playerCanAttackDown
            || GameHandler.Instance.playerCanAttackLeft || GameHandler.Instance.playerCanAttackRight)
            return true;
        else
            return false;
    }
    void BumpAttack() //checking from what side we have enemy and bumping it
    {
        if (GameHandler.Instance.playerCanAttackUp)
        {
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkUp");
            CalculateDamage();
            GameHandler.Instance.playerCanAttackUp = false;
        }
        if (GameHandler.Instance.playerCanAttackDown)
        {
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkDown");
            CalculateDamage();
            GameHandler.Instance.playerCanAttackDown = false;
        }
        if (GameHandler.Instance.playerCanAttackLeft)
        {
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkLeft");
            CalculateDamage();
            GameHandler.Instance.playerCanAttackLeft = false;
        }
        if (GameHandler.Instance.playerCanAttackRight)
        {
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkRight");
            CalculateDamage();
            GameHandler.Instance.playerCanAttackRight = false;
        }

        Debug.Log("Player can't attack");
    }

    private void CalculateDamage()
    {
        enemy = GameObject.Find(GameHandler.Instance.EnemyName);
        enemy.GetComponent<EnemyController2D>().currentEnemyHP = GameHandler.Instance.TakeDamage(GameHandler.Instance.player.GetComponent<PlayerControllerSimple>().PlayerDamage,
                enemy.GetComponent<EnemyController2D>().currentEnemyHP);
    }

    private void MovePlayer()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 1)
        {
            Node2D node2d = (Node2D)pathArray[1];
            _rb.MovePosition(node2d.position);
        }
    }


    public void FindPath()
    {
        startPos = transform;

        StartNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                    (GridManager2D.Instance.GetGridIndex(startPos.position)));

        if (RegisterMouseInput() != null)
        {
            GoalNode = new Node2D(GridManager2D.Instance.GetGridCellCenter
                        (GridManager2D.Instance.GetGridIndex((Vector2)RegisterMouseInput())));
        }
        else
        {
            GoalNode = StartNode;
        }

        pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }
    Vector2? RegisterMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log($"Position of mouse: {mousePosition}");
            return new Vector2(mousePosition.x, mousePosition.y);
        }
        return null;
    }


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
                    Debug.DrawLine(node2d.position, nextNode.position, Color.red); // draw line from current node to next node
                    index++;
                }
            }
        }
    }
}
