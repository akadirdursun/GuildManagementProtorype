using System.Collections.Generic;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public static class HexUtility
    {
        public static Vector3[] GetCorners()
        {
            var outerRadius = HexGridSettings.OuterRadius;
            var innerRadius = GetInnerRadius();
            var corners = new[]
            {
                new Vector3(0f, 0f, outerRadius),
                new Vector3(innerRadius, 0f, 0.5f * outerRadius),
                new Vector3(innerRadius, 0f, -0.5f * outerRadius),
                new Vector3(0f, 0f, -outerRadius),
                new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
                new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
            };
            return corners;
        }

        public static float GetInnerRadius()
        {
            return HexGridSettings.OuterRadius * 0.866025404f;
        }

        public static Mesh GenerateHexagonMesh()
        {
            var corners = GetCorners();
            return GenerateHexagonMesh(corners);
        }

        public static Mesh GenerateHexagonMesh(Vector3[] corners)
        {
            var hexMesh = new Mesh();
            hexMesh.name = "HexMesh";
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            var center = Vector3.zero;
            var cornerCount = corners.Length;
            for (int i = 0; i < 6; i++)
            {
                AddTriangle(center,
                    corners[i],
                    corners[(i + 1) % cornerCount],
                    ref vertices,
                    ref triangles);
            }

            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.RecalculateNormals();
            return hexMesh;
        }

        private static void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, ref List<Vector3> vertices,
            ref List<int> triangles)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }
    }
}