using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
    }
    public override void PlayerUpdate(PlayerController player)
    {
        if (player.elapsedTime >= player.intervalTime)
        {
            player.elapsedTime = 0.0f;
            player.FindPath();
        }
    }

    public override void PlayerFixedUpdate(PlayerController player)
    {
        //if (player.movement.x != 0 || player.movement.y != 0)
        //{
        //    if (TurnHandler.Instance.isPlayersTurn)
        //    {
        //        player.TransitionToState(player.moveState);
        //    }
        //}

        if (TurnHandler.Instance.isPlayersTurn && Input.GetMouseButtonDown(0))
        {
            player.TransitionToState(player.moveState);
        }
    }

}
