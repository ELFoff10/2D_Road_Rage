using UnityEngine;

namespace RoadRage.Tools.Extensions
{
    public static class TransformExt
    {
        public static void LookAt2D(this Transform transform, Vector3 positionTarget)
        {
            var signedAngle = Vector2.SignedAngle(transform.up, (positionTarget - transform.position));

            if (Mathf.Abs(signedAngle) >= 1e-3f)
            {
                var angles = transform.eulerAngles;
                angles.z += signedAngle;
                transform.eulerAngles = angles;
            }
        }
    }
}