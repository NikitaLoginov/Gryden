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
    float intervalTime = 2.0f;

    public static TurnHandler Instance;


    private void Awake()
    {
        Instance = this;   
    }

    private void Update()
    {
        
        elaspedTime += Time.deltaTime;
        if (isWaiting)
        { 
            Wait();
            TurnSwitcher();
        }
    }

    public void TurnSwitcher() // still bit buggy but kinda works. needs testing
    {

        if (isPlayersTurn)
        {
            Debug.Log("Wait for it");

            
            isPlayersTurn = false;
            isWaiting = true;
        }

        else if (isWaiting)
        {
            Debug.Log("Enemy Turn");

            isEnemyTurn = false;
            isPlayersTurn = false;
        }
        else if (isEnemyTurn)
        {
            isPlayersTurn = true;
            isEnemyTurn = false;
        }
        else if (!isWaiting) // has to be last in order!
        {
            isPlayersTurn = false;
            isEnemyTurn = true;
        }

    }

    void Wait()
    { 
        if (elaspedTime >= intervalTime)
        {
            isWaiting = false;
            elaspedTime = 0.0f;
        }
    
    }
}
