using System.Collections.Generic;
using UnityEngine;

namespace Misner.Lib.Graphical
{
    /// <summary>
    /// Mesh generator.
    /// 
    /// 3d mesh building class to assist in the construction of primitives.
    /// </summary>
	public class MeshGenerator
    {
        #region Types
        /// <summary>
        /// Point.
        /// 
        /// Data related to a specific vertex during mesh generation.
        /// </summary>
        public class Point
        {
            #region Readonly Variables
            public readonly int Index;
            public readonly Vector3 Vertex;
            public readonly Vector2 Uvt;
            #endregion


            #region Construction
            public Point(int index, Vector3 vertex, Vector2 uvt)
            {
                Index = index;
                Vertex = vertex;
                Uvt = uvt;
            }
            #endregion
        }
        #endregion


        #region Private Variables
        private readonly List<Point> _points = new List<Point>();
        private readonly List<int> _indices = new List<int>();
        #endregion


        #region Public Methods
        /// <summary>
        /// Adds a new point.
        /// </summary>
        /// <returns>The point.</returns>
        /// <param name="vertex">Vertex.</param>
        /// <param name="uvt">Uvt.</param>
        public Point AddPoint(Vector3 vertex, Vector2 uvt)
        {
            int index = _points.Count;

            Point newPoint = new Point(index, vertex, uvt);
            _points.Add(newPoint);

            return newPoint;
        }

        /// <summary>
        /// Builds a triangle from the given points.
        /// </summary>
        /// <param name="p0">P0.</param>
        /// <param name="p1">P1.</param>
        /// <param name="p2">P2.</param>
        public void AddTriangle(Point p0, Point p1, Point p2)
        {
            AddTriangleInternal(p0, p1, p2);
        }

        /// <summary>
        /// Builds a quadrilateral(quad) from the given points.
        /// </summary>
        /// <param name="p0">P0.</param>
        /// <param name="p1">P1.</param>
        /// <param name="p2">P2.</param>
        /// <param name="p3">P3.</param>
        public void AddQuad(Point p0, Point p1, Point p2, Point p3)
        {
            AddTriangleInternal(p0, p1, p2);
            AddTriangleInternal(p0, p2, p3);
        }

        /// <summary>
        /// Builds a polygon from the given points.
        /// </summary>
        /// <param name="points">Points.</param>
        public void AddPolygon(Point[] points)
        {
            if (points?.Length < 3)
            {
                Debug.LogFormat("<color=#ff0000>{0}.AddPolygon(), a polygon must have at least 3 points.</color>", this.ToString());
            }

            Point p0 = points[0];
            for (int i = 2; i < points.Length; i++)
            {
                Point p1 = points[i - 1];
                Point p2 = points[i];

                AddTriangleInternal(p0, p1, p2);
            }
        }

        /// <summary>
        /// Generates a mesh, usable by Unity, from the information accumulated by the "Add" methods.
        /// </summary>
        /// <returns>The mesh.</returns>
        public Mesh BuildMesh()
        {
            Mesh mesh = new Mesh
            {
                vertices = ExtractVerts(),
                uv = ExtractUvts(),
                triangles = _indices.ToArray()
            };

            mesh.RecalculateNormals();

            return mesh;
        }
        #endregion


        #region Complex Generation Methods
        /// <summary>
        /// Adds the convex hull of an extruded triangle to the mesh.
        /// </summary>
        /// <param name="triangle">Triangle.</param>
        /// <param name="path">Path.</param>
        public void ExtrudeTriangle(Point[] triangle, Vector3 path)
        {
            if (triangle.Length != 3)
            {
                Debug.LogFormat("<color=#ff0000>{0}.ExtrudeTriangle(), parameter 'triangle' must have exactly three vertices.</color>", this.ToString());
                return;
            }

            // Add our base triangle, backwards.
            AddTriangleInternal(triangle[2], triangle[1], triangle[0]);

            // Add our translated triangle, keeps the points for quad generation.
            Point dst0 = AddPoint(triangle[0].Vertex + path, triangle[0].Uvt);
            Point dst1 = AddPoint(triangle[1].Vertex + path, triangle[1].Uvt);
            Point dst2 = AddPoint(triangle[2].Vertex + path, triangle[2].Uvt);
            AddTriangleInternal(dst0, dst1, dst2);

            // Creating a cylinder of quads around the polygon.
            AddQuad(triangle[0], triangle[1], dst1, dst0);
            AddQuad(triangle[1], triangle[2], dst2, dst1);
            AddQuad(triangle[2], triangle[0], dst0, dst2);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Builds a vertex position array from our accumulated points.
        /// </summary>
        /// <returns>The verts.</returns>
        private Vector3[] ExtractVerts()
        {
            List<Vector3> verts = new List<Vector3>();
            
            foreach (Point point in _points)
            {
                verts.Add(point.Vertex);
            }
            
            return verts.ToArray();
        }

        /// <summary>
        /// Builds a vertex UV coordinate array from our accumulated points.
        /// </summary>
        /// <returns>The uvts.</returns>
        private Vector2[] ExtractUvts()
        {
            List<Vector2> uvts = new List<Vector2>();
            
            foreach (Point point in _points)
            {
                uvts.Add(point.Uvt);
            }
            
            return uvts.ToArray();
        }

        /// <summary>
        /// Builds a triangle from the given points.
        /// 
        /// Shared internal use only, to centralize this implementation.
        /// </summary>
        /// <param name="p0">P0.</param>
        /// <param name="p1">P1.</param>
        /// <param name="p2">P2.</param>
        private void AddTriangleInternal(Point p0, Point p1, Point p2)
        {
            _indices.Add(p0.Index);
            _indices.Add(p1.Index);
            _indices.Add(p2.Index);
        }
        #endregion
    }
}
