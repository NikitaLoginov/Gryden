using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    public abstract void EnterState(PlayerController player);
    public abstract void PlayerUpdate(PlayerController player);
    public abstract void PlayerFixedUpdate(PlayerController player);
}
