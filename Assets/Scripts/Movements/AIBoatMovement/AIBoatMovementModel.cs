using UnityEngine;

namespace LNE.Movements
{
  public class AIBoatMovementModel
  {
    public Vector2 TargetPosition { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public bool IsArrived { get; set; }

    public void CheckIfArrived()
    {
      if (Vector2.Distance(CurrentPosition, TargetPosition) < 10f)
      {
        IsArrived = true;
      }
    }

    public void RandomNewTargetPosition(Vector2 center, float radius)
    {
      float randomAngle = Random.value * Mathf.PI * 2;
      float randomRadius = Random.value * radius;
      float x = center.x + Mathf.Cos(randomAngle) * randomRadius;
      float y = center.y + Mathf.Sin(randomAngle) * randomRadius;

      IsArrived = false;

      TargetPosition = new Vector2(x, y);
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
        Mathf.Abs(TargetPosition.y - CurrentPosition.y)
        / Mathf.Abs(TargetPosition.x - CurrentPosition.x);

      float beta = 0;

      // Quadrant I
      if (
        TargetPosition.x - CurrentPosition.x > 0
        && TargetPosition.y - CurrentPosition.y > 0
      )
      {
        beta = Mathf.Atan(slopeAbs);
      }
      else
      // Quadrant II
      if (
        TargetPosition.x - CurrentPosition.x < 0
        && TargetPosition.y - CurrentPosition.y > 0
      )
      {
        beta = Mathf.PI - Mathf.Atan(slopeAbs);
      }
      // Quadrant III
      else if (
        TargetPosition.x - CurrentPosition.x < 0
        && TargetPosition.y - CurrentPosition.y < 0
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
