using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public void OnAttackButtonPress()
    {
        if (GameHandler.Instance.playerCanAttack)
        { 
            GameHandler.Instance.enemies[0].GetComponent<EnemyController2D>().currentEnemyHP = GameHandler.Instance.TakeDamage(GameHandler.Instance.player.GetComponent<PlayerControllerSimple>().PlayerDamage,
                GameHandler.Instance.enemies[0].GetComponent<EnemyController2D>().currentEnemyHP);

            //animation

            GameHandler.Instance.playerCanAttack = false;
        }
        Debug.Log("Player can't attack");
    }
}
