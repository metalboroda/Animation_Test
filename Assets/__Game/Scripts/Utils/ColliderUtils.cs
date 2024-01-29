using UnityEngine;

namespace Animation_Test
{
  public class ColliderUtils : MonoBehaviour
  {
    public static Vector3 GetNearestPointOnColliderBounds(Collider collider, Vector3 targetPoint)
    {
      if (collider == null)
      {
        return Vector3.zero;
      }

      Bounds bounds = collider.bounds;
      Vector3 closestPoint = bounds.ClosestPoint(targetPoint);

      return closestPoint;
    }
  }
}