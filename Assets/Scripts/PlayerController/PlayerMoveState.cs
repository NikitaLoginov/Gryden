using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
    }
    public override void PlayerUpdate(PlayerController player)
    {
    }

    public override void PlayerFixedUpdate(PlayerController player)
    {
        //Move(player);
        //if (player.transform.position == player.movePoint.transform.position)
        //{
        //    Debug.Log("Player Moved.");
        //    TurnHandler.Instance.TurnSwitcher();
        //    player.TransitionToState(player.idleState);
        //}

        player.MovePlayer();
        Debug.Log("Player moved.");
        player.TransitionToState(player.idleState);

    }

    //Move player with keyboard
    void Move(PlayerController player)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, player.movePoint.position, player.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(player.transform.position, player.movePoint.position) <= .05f)
        {
            if (Mathf.Abs(player.movement.x) == 1f)
            { 
                player.movePoint.position += new Vector3(player.movement.x, 0f, 0f);
            }
            if (Mathf.Abs(player.movement.y) == 1f)
            { 
                player.movePoint.position += new Vector3(0f, player.movement.y, 0f);
            }
        }
    }
}
