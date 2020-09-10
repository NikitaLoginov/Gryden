using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnterState(EnemyController2D enemy);
    public abstract void EnemyUpdate(EnemyController2D enemy);
    public abstract void EnemyFixedUpdate(EnemyController2D enemy);
}
