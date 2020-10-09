using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnemyFixedUpdate(EnemyController2D enemy)
    {
        if (TurnHandler.Instance.isEnemyTurn && !TurnHandler.Instance.isWaiting)
        {
            enemy.TransitionToState(enemy.moveState);
        }
    }

    public override void EnemyUpdate(EnemyController2D enemy)
    {
        if (TurnHandler.Instance.isEnemyTurn) // optimized for turn based game
        {
            enemy.elapsedTime = 0.0f;
            enemy.FindPath();

            if (enemy.enemyCanAttackUp || enemy.enemyCanAttackDown
                || enemy.enemyCanAttackLeft || enemy.enemyCanAttackRight) //enemy.pathArray.Count == 1 - checked this before
            {
                enemy.TransitionToState(enemy.attackState);
            }
        }
    }

    public override void EnterState(EnemyController2D enemy)
    {
    }

}
