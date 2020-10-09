using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyController2D enemy)
    {
    }

    public override void EnemyUpdate(EnemyController2D enemy)
    {
        BumpAttack(enemy);
        if (TurnHandler.Instance.isEnemyTurn)
        {
            TurnHandler.Instance.TurnSwitcher();
            enemy.TransitionToState(enemy.idleState);
        }
    }

    public override void EnemyFixedUpdate(EnemyController2D enemy)
    {
    }

    void BumpAttack(EnemyController2D enemy)
    {
        Debug.Log("Enemy attacks");
        //animation
        if (enemy.enemyCanAttackUp)
        {
            enemy.enemyAnimator.SetTrigger("BAUp");
            enemy.enemyCanAttackUp = false;
        }
        if (enemy.enemyCanAttackDown)
        {
            enemy.enemyAnimator.SetTrigger("BADown");
            enemy.enemyCanAttackDown = false;
        }
        if (enemy.enemyCanAttackLeft)
        { 
            enemy.enemyAnimator.SetTrigger("BALeft");
            enemy.enemyCanAttackLeft = false;
        }
        if (enemy.enemyCanAttackRight)
        { 
            enemy.enemyAnimator.SetTrigger("BARight");
            enemy.enemyCanAttackRight = false;
        }

        enemy.playerController.currentPlayerHP = GameHandler.Instance.TakeDamage(enemy.EnemyDamage,
            enemy.playerController.currentPlayerHP);
    }
}
