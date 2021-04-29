using System.Collections.Generic;
using UnityEngine;

namespace Misner.Lib.Graphics
{
	public class MeshGenerator
    {
        #region Types
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
        public Point AddPoint(Vector3 vertex, Vector2 uvt)
        {
            int index = _points.Count;

            Point newPoint = new Point(index, vertex, uvt);
            _points.Add(newPoint);

            return newPoint;
        }

        public void AddTriangle(Point p0, Point p1, Point p2)
        {
            AddTriangleInternal(p0, p1, p2);
        }

        public void AddQuad(Point p0, Point p1, Point p2, Point p3)
        {
            AddTriangleInternal(p0, p1, p2);
            AddTriangleInternal(p0, p2, p3);
        }

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


        #region Private Methods
        private Vector3[] ExtractVerts()
        {
            List<Vector3> verts = new List<Vector3>();
            
            foreach (Point point in _points)
            {
                verts.Add(point.Vertex);
            }
            
            return verts.ToArray();
        }

        private Vector2[] ExtractUvts()
        {
            List<Vector2> uvts = new List<Vector2>();
            
            foreach (Point point in _points)
            {
                uvts.Add(point.Uvt);
            }
            
            return uvts.ToArray();
        }

        private void AddTriangleInternal(Point p0, Point p1, Point p2)
        {
            _indices.Add(p0.Index);
            _indices.Add(p1.Index);
            _indices.Add(p2.Index);
        }
        #endregion
    }
}
