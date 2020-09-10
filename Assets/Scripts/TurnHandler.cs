using UnityEngine;
using System;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour
{
    public bool isPlayersTurn = true;
    public bool isEnemyTurn;

    public static TurnHandler Instance;


    private void Awake()
    {
        Instance = this;   
    }
    public void TurnSwitcher()
    {
        if (isPlayersTurn)
        {
            Debug.Log("Enemy Turn");
            isEnemyTurn = true;
            isPlayersTurn = false;
        }

        else
        {
            Debug.Log("Player's Turn");

            isEnemyTurn = false;
            isPlayersTurn = true;
        }
    }
}
