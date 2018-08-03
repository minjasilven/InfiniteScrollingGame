using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GenerateTube : MonoBehaviour
{
    [SerializeField] private int xSize, ySize;

    private Vector3[] _vertices;
    private Mesh _mesh;
    private void Awake()
    {
        GenerateVertices();
    }

    void GenerateVertices()
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "Tube";

        // int tempi = 0;

        // if other wall added
        //_vertices = new Vector3[((xSize + 1) * (ySize + 1)) * 2];
        _vertices = new Vector3[((xSize + 1) * (ySize + 1)) * 2];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                _vertices[i] = new Vector3(x, y);
                //tempi = i;
            }
        }

        // Draw another wall
        /*for (int i = tempi, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, tempi++)
            {
                _vertices[tempi] = new Vector3(x, y, -5);
            }
        }*/

        _mesh.vertices = _vertices;
        GenerateTriangles();
    }

    private void GenerateTriangles()
    {
        int[] triangles = new int[xSize * ySize * 6];
        /*triangles[0] = 0;
        triangles[1] = xSize + 1;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = xSize + 1;
        triangles[5] = xSize + 2;*/

        /* Could be shortened:
		triangles[0] = 0;
		triangles[3] = triangles[2] = 1;
		triangles[4] = triangles[1] = xSize + 1;
		triangles[5] = xSize + 2;
		*/

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }

        _mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (_vertices == null)
            return;

        Gizmos.color = Color.black;
        for (int i = 0; i < _vertices.Length; i++)
        {
            Gizmos.DrawSphere(_vertices[i], 0.1f);
        }
    }
}
