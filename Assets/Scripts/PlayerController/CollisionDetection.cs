using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    GameObject playerObj; //????
    EnemyController2D enemy;
    PlayerControllerSimple player; //????

    Collider2D boxCollider;

    //Raycast
    RaycastHit2D hit2dDown;
    RaycastHit2D hit2dUp;
    RaycastHit2D hit2dLeft;
    RaycastHit2D hit2dRight;

    

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerControllerSimple>();

        boxCollider = GetComponent<BoxCollider2D>();

        Debug.Log($"Player's current hp: {player.currentPlayerHP}");


        //enemy stuff
        enemy = GetComponent<EnemyController2D>();

    }

    void FixedUpdate()
    {
        boxCollider.enabled = false;
        
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
        boxCollider.enabled = true;
    }

    

    void CheckRayCast()
    {
        // player attacks
        if (hit2dUp.collider != null && hit2dUp.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");

            GameHandler.Instance.GetEnemyName(hit2dUp.collider); // sending enemy name to Game Handler

            GameHandler.Instance.playerCanAttackUp = true;
        }
        if (hit2dDown.collider != null && hit2dDown.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");

            GameHandler.Instance.GetEnemyName(hit2dDown.collider); // sending enemy name to Game Handler

            GameHandler.Instance.playerCanAttackDown = true;
        }
        if (hit2dLeft.collider != null && hit2dLeft.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");

            GameHandler.Instance.GetEnemyName(hit2dLeft.collider); // sending enemy name to Game Handler

            GameHandler.Instance.playerCanAttackLeft = true;
        }
        if (hit2dRight.collider != null && hit2dRight.collider.tag == "Enemy")
        {
            Debug.Log("Ray hit enemy");

            GameHandler.Instance.GetEnemyName(hit2dRight.collider); // sending enemy name to Game Handler

            GameHandler.Instance.playerCanAttackRight = true;
        }

        //enemy attacks shouldn't be in singleton!!!!
        if (enemy != null && hit2dUp.collider != null && hit2dUp.collider.tag == "Player") // checking if enemy controller was found and collider hit something and if that something was player
        {
            Debug.Log("Ray hit player");

            enemy.enemyCanAttackUp = true;
        }

        if (enemy != null && hit2dDown.collider != null && hit2dDown.collider.tag == "Player")
        {
            Debug.Log("Ray hit player");

            enemy.enemyCanAttackDown = true;
        }
        if (enemy != null && hit2dLeft.collider != null && hit2dLeft.collider.tag == "Player")
        {
            Debug.Log("Ray hit player");

            enemy.enemyCanAttackLeft = true;

        }
        if (enemy != null && hit2dRight.collider != null && hit2dRight.collider.tag == "Player")
        {
            Debug.Log("Ray hit player");

            enemy.enemyCanAttackRight = true;
        }
    }
}
