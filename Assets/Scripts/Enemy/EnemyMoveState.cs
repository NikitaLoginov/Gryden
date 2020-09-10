using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public override void EnemyFixedUpdate(EnemyController2D enemy)
    {
        enemy.MoveBody();
        Debug.Log("Enemy Moved.");
        if (TurnHandler.Instance.isEnemyTurn)
        { 
            TurnHandler.Instance.TurnSwitcher();
            enemy.TransitionToState(enemy.idleState);
        }
    }

    public override void EnemyUpdate(EnemyController2D enemy)
    {
    }

    public override void EnterState(EnemyController2D enemy)
    {
    }
    
}
