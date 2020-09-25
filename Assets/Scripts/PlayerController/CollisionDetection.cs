using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    GameObject playerObj;
    Collider2D playerCollider;
    PlayerControllerSimple player;

    GameObject enemyObj;
    Collider2D enemyCol;
    EnemyController2D enemy;
    GameObject[] enemies;

    //Raycast
    RaycastHit2D hit2dDown;
    RaycastHit2D hit2dUp;
    RaycastHit2D hit2dLeft;
    RaycastHit2D hit2dRight;

    

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerControllerSimple>();
        playerCollider = playerObj.GetComponent<BoxCollider2D>();

        Debug.Log($"Player's current hp: {player.currentPlayerHP}");

        enemyObj = GameObject.Find("Rat");
        //enemy = enemyObj.GetComponent<EnemyController2D>();
        enemyCol = enemyObj.GetComponent<BoxCollider2D>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void FixedUpdate()
    {
        GameHandler.Instance.playerCollider.enabled = false;
        
        hit2dDown = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        hit2dUp = Physics2D.Raycast(transform.position, Vector2.up, 1.0f);
        hit2dLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.0f);
        hit2dRight = Physics2D.Raycast(transform.position, Vector2.right, 1.0f);

        //Debug.DrawRay(transform.position, -Vector2.up, Color.cyan, 10.0f);
        //Debug.DrawRay(transform.position, Vector2.up, Color.cyan, 10.0f);
        //Debug.DrawRay(transform.position, -Vector2.right, Color.cyan, 10.0f);
        //Debug.DrawRay(transform.position, Vector2.right, Color.cyan, 10.0f);


        //Debug.Log($"Raycast info: {hit2dDown.collider.tag}\n{hit2dUp.collider.tag}\n{hit2dLeft.collider.tag}\n{hit2dRight.collider.tag}"); debug doesn't work

        CheckRayCast();
        GameHandler.Instance.playerCollider.enabled = true;

    }

    

    void CheckRayCast()
    {
        //if (hit2dUp.collider == enemyCol || hit2dDown.collider == enemyCol ||
        //    hit2dLeft.collider == enemyCol || hit2dRight.collider == enemyCol)
        //{
        //    Debug.Log("Ray hit enemy");
        //    GameHandler.Instance.playerCanAttack = true;
        //}

        if (hit2dUp.collider != null && hit2dUp.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");
            GameHandler.Instance.playerCanAttackUp = true;
        }
        if (hit2dDown.collider != null && hit2dDown.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");
            GameHandler.Instance.playerCanAttackDown = true;
        }
        if (hit2dLeft.collider != null && hit2dLeft.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");
            GameHandler.Instance.playerCanAttackLeft = true;
        }
        if (hit2dRight.collider != null && hit2dRight.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");
            GameHandler.Instance.playerCanAttackRight = true;
        }
    }
}
