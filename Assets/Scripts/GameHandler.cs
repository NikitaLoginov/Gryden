using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] nodes;
    public GameObject player;

    //attack

    public bool playerCanAttackUp = false;
    public bool playerCanAttackDown = false;
    public bool playerCanAttackLeft = false;
    public bool playerCanAttackRight = false;

    string enemyName;
    public string EnemyName { get { return enemyName;} private set { enemyName = value; } }

    //Animations
    public Animator playerAnim;

    public static GameHandler Instance; // singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // singleton
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerAnim = player.GetComponent<Animator>();

        Debug.Log("Enemy array length: " + enemies.Length);
        
        Instantiate(player, player.transform.position,Quaternion.identity);
        var position = new Vector3(2.5f, 8.5f, 0f);
        
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = i == 0 ? new Vector3(position.x, position.y, position.z) : new Vector3(position.x += 3, position.y, position.z); // for 1st enemy : for others
            Instantiate(enemies[i], enemies[i].transform.position,Quaternion.identity);
        }

    }

    public float TakeDamage(float damage, float hp)
    {
        hp -= damage;
        return hp;
    }

    public string GetEnemyName(Collider2D collider)
    {
        EnemyName = collider.name;
        return EnemyName;
    }

    // death method
    public void DropDead(float currentHP, GameObject obj)
    {
        if (currentHP <= 0)
        {
            //play animation
            Destroy(obj);

            // stop enemy from attacking player if not near. not sure if needed
            //if (enemyCanAttackDown)
            //    enemyCanAttackDown = false;
            //if (enemyCanAttackUp)
            //    enemyCanAttackUp = false;
            //if (enemyCanAttackLeft)
            //    enemyCanAttackLeft = false;
            //if (enemyCanAttackRight)
            //    enemyCanAttackRight = false;
        }
    }
}
