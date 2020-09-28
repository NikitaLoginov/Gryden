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
        enemy.playerController.currentPlayerHP = GameHandler.Instance.TakeDamage(enemy.EnemyDamage,
            enemy.playerController.currentPlayerHP);
    }
}
