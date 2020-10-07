using UnityEngine;
using System;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour
{
    public bool isPlayersTurn = true;
    public bool isEnemyTurn;
    public bool isWaiting;

    //time
    float elaspedTime = 0.0f;
    float intervalTime = 1.0f;

    public static TurnHandler Instance;


    private void Awake()
    {
        Instance = this;   
    }

    private void Update()
    {
        
        elaspedTime += Time.deltaTime;
    }

    public void TurnSwitcher() // enemy moves once in two turns. need to fix
    {

        if (isPlayersTurn && !isWaiting)
        {
            Debug.Log("Enemy Turn");
            isPlayersTurn = false;
            isEnemyTurn = true;
            isWaiting = true;
        }

        else if (isEnemyTurn && !isWaiting)
        {
            Debug.Log("Player's Turn");

            isEnemyTurn = false;
            isPlayersTurn = true;
            isWaiting = true;
        }

        if (elaspedTime < intervalTime)
        {
            isWaiting = true;
        }
        if (elaspedTime >= intervalTime)
        {
            isWaiting = false;
            elaspedTime = 0.0f;
        }
    }
}
