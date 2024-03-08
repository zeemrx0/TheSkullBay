using UnityEngine;

[CreateAssetMenu(
  fileName = "_WatercraftMovementData",
  menuName = "Game Data/Watercraft Movement Data"
)]
public class WatercraftMovementData : ScriptableObject
{
  [field: Header("Move")]
  [field: SerializeField]
  public float MoveSpeed { get; private set; } = 200f;

  [field: SerializeField]
  public float MaxMoveSpeed { get; private set; } = 20f;

  [field: Header("Steer")]
  [field: SerializeField]
  private float _steerSpeed = 60f;
  public float SteerSpeed => _steerSpeed * 10f;

  [field: SerializeField]
  private float _maxSteerSpeed = 40f;
  public float MaxSteerSpeed
  {
    get => _maxSteerSpeed / 100f;
  }

  [field: SerializeField]
  public float AngleThreshold { get; private set; } = 45f;

  public float CurrentMoveSpeed { get; private set; } = 0f;

  public float CurrentSteerSpeed { get; private set; } = 0f;
}
