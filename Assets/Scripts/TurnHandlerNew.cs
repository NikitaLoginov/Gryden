using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandlerNew : MonoBehaviour
{
    public bool isPlayersTurn = true;
    public bool isEnemyTurn;

    public static TurnHandlerNew Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void TurnSwitcher() // still bit buggy but kinda works. needs testing
    {

        if (isPlayersTurn)
        {
            isPlayersTurn = false;
            isEnemyTurn = true;
        }
        else if (isEnemyTurn)
        {
            isPlayersTurn = true;
            isEnemyTurn = false;
        }

    }
}
