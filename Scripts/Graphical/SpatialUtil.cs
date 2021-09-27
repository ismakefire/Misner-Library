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
        #region Constants
        public const double kEpsilon = 1E-5;
        #endregion


        #region Public Methods
        /// <summary>
        /// Finds the point on an infinite plane which is closest to (0, 0, 0)
        /// </summary>
        /// <returns>The centroid.</returns>
        /// <param name="plane">Plane.</param>
        public static Vector3 FindCentroid(Plane plane)
        {
            Vector3 centroid = plane.ClosestPointOnPlane(Vector3.zero);
            
            return centroid;
        }

        /// <summary>
        /// Finds a ray following the line of intersection between two planes.
        /// </summary>
        /// <returns>The planes.</returns>
        /// <param name="planeA">Plane a.</param>
        /// <param name="planeB">Plane b.</param>
        public static Ray IntersectPlanes(Plane planeA, Plane planeB)
        {
            Vector3 rayOrigin = FindPlanePoint(planeA, planeB);
            Vector3 rayDirection = Vector3.Cross(planeA.normal, planeB.normal);

            return new Ray(rayOrigin, rayDirection);
        }

        /// <summary>
        /// Finds the point of intersection between a ray and a plane.
        /// </summary>
        /// <returns>The intersect.</returns>
        /// <param name="ray">Ray.</param>
        /// <param name="plane">Plane.</param>
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
            // Are either of our centroids already in the other plane?
            Vector3 centroidB = FindCentroid(planeB);
            if (PlaneContainsPoint(planeA, centroidB))
            {
                return centroidB;
            }

            Vector3 centroidA = FindCentroid(planeA);
            if (PlaneContainsPoint(planeB, centroidA))
            {
                return centroidA;
            }


            // This is our displacement to the other plane's center.
            Vector3 centroidDelta = centroidB - centroidA;

            // The direction to that centroid, from within our plane.
            Vector3 inplanePath = FindInplaneDirection(centroidDelta, planeA);

            // Let, planeB.GetDistanceToPoint(centroidA + inplanePath * inplaneDistance) == 0
            // Solves to: inplaneDistance = -(m_Distance + Vector3.Dot(m_Normal, centroidA)) / Vector3.Dot(m_Normal, inplanePath)
            float inplaneDistance = -planeB.GetDistanceToPoint(centroidA) / Vector3.Dot(planeB.normal, inplanePath);

            // Walking from our centroid within plane until we reach the other plane.
            Vector3 result = centroidA + inplanePath * inplaneDistance;

            return result;
        }

        /// <summary>
        /// Finds the inplane direction(normalized) towards a point in 3d space.
        /// </summary>
        /// <returns>The inplane direction.</returns>
        /// <param name="displacementPath">Displacement path.</param>
        /// <param name="plane">Plane.</param>
        private static Vector3 FindInplaneDirection(Vector3 displacementPath, Plane plane)
        {
            // Project onto the plane. Giving us a displacement towards our goal.
            Vector3 inplaneDisplacement = displacementPath - Vector3.Dot(displacementPath, plane.normal) * plane.normal;

            // Adjusting the length to one.
            return inplaneDisplacement.normalized;
        }

        /// <summary>
        /// Does the plane contain the point provided.
        /// </summary>
        /// <returns><c>true</c>, if contains point was planed, <c>false</c> otherwise.</returns>
        /// <param name="plane">Plane.</param>
        /// <param name="point">Point.</param>
        private static bool PlaneContainsPoint(Plane plane, Vector3 point)
        {
            float signedDistance = plane.GetDistanceToPoint(point);
            //return Vector3.Dot(m_Normal, point) + m_Distance;

            return (Mathf.Abs(signedDistance) < kEpsilon);
        }
        #endregion
    }
}
