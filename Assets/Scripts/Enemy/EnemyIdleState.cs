using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnemyFixedUpdate(EnemyController2D enemy)
    {
        if (TurnHandler.Instance.isEnemyTurn)
        {
            enemy.TransitionToState(enemy.moveState);
        }
    }

    public override void EnemyUpdate(EnemyController2D enemy)
    {
        //if (enemy.elapsedTime >= enemy.intervalTime) 
        if (TurnHandler.Instance.isEnemyTurn) // optimized for turn based game
        {
            enemy.elapsedTime = 0.0f;
            enemy.FindPath();
        }
    }

    public override void EnterState(EnemyController2D enemy)
    {
    }

}
