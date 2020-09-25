using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public void OnAttackButtonPress()
    {
        //if (GameHandler.Instance.playerCanAttack)
        //{
        //    CalculateDamageForEnemy();

        //    //animation

        //    GameHandler.Instance.playerCanAttack = false;
        //}

        if (GameHandler.Instance.playerCanAttackUp)
        {
            CalculateDamage();
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkUp");
            GameHandler.Instance.playerCanAttackUp = false;
        }
        if (GameHandler.Instance.playerCanAttackDown)
        {
            CalculateDamage();
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkDown");
            GameHandler.Instance.playerCanAttackDown = false;
        }
        if (GameHandler.Instance.playerCanAttackLeft)
        {
            CalculateDamage();
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkLeft");
            GameHandler.Instance.playerCanAttackLeft = false;
        }
        if (GameHandler.Instance.playerCanAttackRight)
        {
            CalculateDamage();
            GameHandler.Instance.playerAnim.SetTrigger("PlayerAtkRight");
            GameHandler.Instance.playerCanAttackRight = false;
        }

        Debug.Log("Player can't attack");
    }

    void CalculateDamage() //REFACTOR FOR MULTIPLE ENEMIES!!
    {
        GameHandler.Instance.enemies[0].GetComponent<EnemyController2D>().currentEnemyHP = GameHandler.Instance.TakeDamage(GameHandler.Instance.player.GetComponent<PlayerControllerSimple>().PlayerDamage,
                GameHandler.Instance.enemies[0].GetComponent<EnemyController2D>().currentEnemyHP);
    }
}
