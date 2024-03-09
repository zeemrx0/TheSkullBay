using UnityEngine;

namespace LNE.Movements
{
  public class AIWatercraftMovementModel
  {
    public Vector3 TargetPosition { get; set; }
    public Vector3 CurrentPosition { get; set; }
    public bool IsArrived { get; set; }

    public void CheckIfArrived()
    {
      if (Vector3.Distance(CurrentPosition, TargetPosition) < 10f)
      {
        IsArrived = true;
      }
    }

    public void RandomNewTargetPosition(Vector3 center, float radius)
    {
      float randomAngle = Random.value * Mathf.PI * 2;
      float randomRadius = Random.value * radius;
      float x = center.x + Mathf.Cos(randomAngle) * randomRadius;
      float z = center.z + Mathf.Sin(randomAngle) * randomRadius;

      IsArrived = false;

      TargetPosition = new Vector3(x, center.y, z);
    }

    // Positive = Right, Negative = Left
    public float CheckTargetIsOnWhichSide(Transform transform)
    {
      float alpha = -transform.eulerAngles.y + 90f;
      while (alpha < 0)
      {
        alpha += 360;
      }

      float slopeAbs =
        Mathf.Abs(TargetPosition.z - CurrentPosition.z)
        / Mathf.Abs(TargetPosition.x - CurrentPosition.x);

      float beta = 0;

      // Quadrant I
      if (
        TargetPosition.x - CurrentPosition.x > 0
        && TargetPosition.z - CurrentPosition.z > 0
      )
      {
        beta = Mathf.Atan(slopeAbs);
      }
      else
      // Quadrant II
      if (
        TargetPosition.x - CurrentPosition.x < 0
        && TargetPosition.z - CurrentPosition.z > 0
      )
      {
        beta = Mathf.PI - Mathf.Atan(slopeAbs);
      }
      // Quadrant III
      else if (
        TargetPosition.x - CurrentPosition.x < 0
        && TargetPosition.z - CurrentPosition.z < 0
      )
      {
        beta = Mathf.PI + Mathf.Atan(slopeAbs);
      }
      // Quadrant IV
      else
      {
        beta = Mathf.PI * 2f - Mathf.Atan(slopeAbs);
      }

      // Beta to degrees
      beta = beta * 180f / Mathf.PI;
      beta %= 360;

      float betaMinusAlpha = beta - alpha;
      while (betaMinusAlpha < 0)
      {
        betaMinusAlpha += 360;
      }
      float alphaMinusBeta = alpha - beta;
      while (alphaMinusBeta < 0)
      {
        alphaMinusBeta += 360;
      }
      if (betaMinusAlpha < alphaMinusBeta)
      {
        return -betaMinusAlpha;
      }
      else
      {
        return alphaMinusBeta;
      }
    }
  }
}
