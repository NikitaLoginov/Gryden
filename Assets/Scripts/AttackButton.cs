using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    GameObject enemy;
    public void OnAttackButtonPress()
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

    void CalculateDamage() //finding which enemy to attack by name that we set up in CollisionDetection
    {
        enemy = GameObject.Find(GameHandler.Instance.EnemyName);
        enemy.GetComponent<EnemyController2D>().currentEnemyHP = GameHandler.Instance.TakeDamage(GameHandler.Instance.player.GetComponent<PlayerControllerSimple>().PlayerDamage,
                enemy.GetComponent<EnemyController2D>().currentEnemyHP);
    }
}
