using System.Linq;
using UnityEngine;

public class TurnHandlerNew : MonoBehaviour
{
    //This script handles turns.
    //Each turn of every entity consist of fazes. Once all active fazes are done entity waits
    //This script give commands to AI entities to execute their actions and make sure those actions are complete
    //This script controls whether it's player turn or not
    public enum TurnFasez
    {
        Normal,
        Waiting
    }

    public static TurnHandlerNew instance;
    public TurnFasez faze;

    public bool isPlayerTurn;
    private IMoveable[] _enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if(instance != null)
            Destroy(gameObject);

        faze = TurnFasez.Normal;
        isPlayerTurn = true;
        _enemies = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IMoveable>().ToArray();
    }

    private void Start()
    {
    }

    private void Update()
    {
        MoveEnemies();
    }

    void MoveEnemies()
    {
        if (!isPlayerTurn)
        {
            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].GetGoalPosition();
                _enemies[i].MoveToPlayer();
            }

            isPlayerTurn = true;
            faze = TurnFasez.Normal;
        }
    }

    public void SwitchTurn()
    {
        faze = TurnFasez.Normal;
    }

    public void ChangeTurn()
    {
        if (faze == TurnFasez.Waiting)
            faze = TurnFasez.Normal;
        isPlayerTurn = false;
    }
}
