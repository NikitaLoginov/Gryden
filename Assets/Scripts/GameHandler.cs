using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject[] nodes;
    public GameObject player;
    public BoxCollider2D playerCollider;

    //attack
    public bool playerCanAttack = false; // refactor

    public bool playerCanAttackUp = false;
    public bool playerCanAttackDown = false;
    public bool playerCanAttackLeft = false;
    public bool playerCanAttackRight = false;

    public bool enemyCanAttack = false;

    public static GameHandler Instance; // singleton

    private void Awake()
    {
        Instance = this; // singleton

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<BoxCollider2D>();

        nodes = FindObjectsOfType<GameObject>(); //????

        Debug.Log("Enemy array length: " + enemies.Length);

    }

    public float TakeDamage(float damage, float hp)
    {
        hp -= damage;
        return hp;
    }
}
