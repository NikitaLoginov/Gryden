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

    public bool playerCanAttackUp = false;
    public bool playerCanAttackDown = false;
    public bool playerCanAttackLeft = false;
    public bool playerCanAttackRight = false;

    public bool enemyCanAttack = false;

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
        playerCollider = player.GetComponent<BoxCollider2D>();
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
}
