using UnityEngine;

namespace Misner.Lib.Graphical
{
    /// <summary>
    /// Spatial util.
    /// 
    /// Library of Plane, Ray, and Vector methods. For both converting
    /// betweening and discovering properties in space.
    /// </summary>
	public class SpatialUtil
    {
        #region Public Methods
        public static Vector3 FindCentroid(Plane plane)
        {
            Vector3 centroid = plane.ClosestPointOnPlane(Vector3.zero);
            
            return centroid;
        }

        public static Ray IntersectPlanes(Plane planeA, Plane planeB)
        {
            Vector3 rayOrigin = FindPlanePoint(planeA, planeB);
            Vector3 rayDirection = Vector3.Cross(planeA.normal, planeB.normal);

            return new Ray(rayOrigin, rayDirection);
        }

        public static Vector3 Intersect(Ray ray, Plane plane)
        {
            // Solve for rayDistance:  0f = plane.GetDistanceToPoint(ray.GetPoint(rayDistance))
            float rayDistance = -plane.GetDistanceToPoint(ray.origin) / Vector3.Dot(plane.normal, ray.direction);

            Vector3 intersectionPoint = ray.GetPoint(rayDistance);

            return intersectionPoint;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Finds a point inside both planes.
        /// </summary>
        /// <returns>The plane point.</returns>
        /// <param name="planeA">Plane a.</param>
        /// <param name="planeB">Plane b.</param>
        private static Vector3 FindPlanePoint(Plane planeA, Plane planeB)
        {
            // Our centroids already have a plane coordinate of zero. So we ask,
            // is my centroid already inside(plane coord = 0) the other plane?

            Vector3 centroidB = FindCentroid(planeB); // (?, 0)
            if (PlaneContainsPoint(planeA, centroidB))
            {
                return centroidB; // (0, 0)
            }


            Vector3 centroidA = FindCentroid(planeA); // (0, ?)
            if (PlaneContainsPoint(planeB, centroidA))
            {
                return centroidA; // (0, 0)
            }


            // This is our displacement to the other plane's center.
            Vector3 centroidDelta = centroidB - centroidA;

            // The direction of that centroid, inside our plane.
            Vector3 inplanePath = FindInplaneDirection(centroidDelta, planeA);

            // Let, planeB.GetDistanceToPoint(centroidA + inplanePath * inplaneDistance) == 0
            // Solves to: inplaneDistance = -(m_Distance + Vector3.Dot(m_Normal, centroidA)) / Vector3.Dot(m_Normal, inplanePath)
            float inplaneDistance = -planeB.GetDistanceToPoint(centroidA) / Vector3.Dot(planeB.normal, inplanePath);


            Vector3 result = centroidA + inplanePath * inplaneDistance;

            //Debug.LogFormat("<color=#ff00ff>{0}.FindPlanePoint(), centroidDelta = {1}, inplanePath = {2}, inplaneDistance = {3}, result = {4}</color>", "SpatialUtil", centroidDelta.ToString("N3"), inplanePath.ToString("N3"), inplaneDistance, result.ToString("N3"));

            return result;
        }

        private static Vector3 FindInplaneDirection(Vector3 displacementPath, Plane plane)
        {
            // Project onto the plane. Giving us a displacement towards our goal.
            Vector3 inplaneDisplacement = displacementPath - Vector3.Dot(displacementPath, plane.normal) * plane.normal;

            // Adjusting the length to one.
            return inplaneDisplacement.normalized;
        }

        private static bool PlaneContainsPoint(Plane plane, Vector3 point)
        {
            float signedDistance = plane.GetDistanceToPoint(point);
            //return Vector3.Dot(m_Normal, point) + m_Distance;

            return (Mathf.Abs(signedDistance) < 0.01f);
        }
        #endregion
    }
}
