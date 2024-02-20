using UnityEngine;

[CreateAssetMenu(
  fileName = "_BoatMovementData",
  menuName = "Game Data/Boat Movement Data"
)]
public class BoatMovementData : ScriptableObject
{
  [field: SerializeField]
  public float MoveSpeed { get; private set; } = 100f;

  [field: SerializeField]
  public float SteerSpeed { get; private set; } = 5f;

  [field: SerializeField]
  public float MaxMoveSpeed { get; private set; } = 500f;

  [field: SerializeField]
  public float MaxSteerSpeed { get; private set; } = 10f;

  public float CurrentMoveSpeed { get; private set; } = 0f;

  public float CurrentSteerSpeed { get; private set; } = 0f;

  public void SetCurrentMoveSpeed(float moveSpeed)
  {
    CurrentMoveSpeed = moveSpeed;
  }

  public void SetCurrentSteerSpeed(float steerSpeed)
  {
    CurrentSteerSpeed = steerSpeed;
  }
}
