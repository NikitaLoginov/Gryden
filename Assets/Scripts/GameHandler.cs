﻿using System.Collections;
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

    // enemy attack

    //public bool enemyCanAttackUp = false;
    //public bool enemyCanAttackDown = false;
    //public bool enemyCanAttackLeft = false;
    //public bool enemyCanAttackRight = false;

    string enemyName;
    public string EnemyName { get { return enemyName;} private set { enemyName = value; } }

    //Animations
    public Animator playerAnim;


    public static GameHandler Instance; // singleton

    private void Awake()
    {
        Instance = this; // singleton

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");

        playerAnim = player.transform.GetChild(0).GetComponent<Animator>();

        Debug.Log("Enemy array length: " + enemies.Length);

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
