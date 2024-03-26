using UnityEngine;

[CreateAssetMenu(
  fileName = "_HumanMovementData",
  menuName = "Movements/Human Movement"
)]
public class HumanMovementData : ScriptableObject
{
  [field: Header("Move")]
  [field: SerializeField]
  public float MoveSpeed { get; private set; } = 200f;
}
