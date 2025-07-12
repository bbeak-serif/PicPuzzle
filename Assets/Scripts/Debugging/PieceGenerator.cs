using UnityEngine;
using System.Collections.Generic;

public class PieceGenerator : MonoBehaviour {
    private const float PIECE_SIZE = 1.0f;
    private const float PROTRUSION_DEPTH = 0.2f;
    private const float PROTRUSION_WIDTH_RATIO = 0.3f;

    public Mesh GeneratePieceMesh(int bottomType, int leftType, int topType, int rightType) {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();

        vertices = GetOuterMesh(bottomType, leftType, topType, rightType);
        triangles = GetTriangles(vertices.Count);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    private List<Vector3> GetOuterMesh(int bottomType, int leftType, int topType, int rightType) {
        List<Vector3> vertices = new List<Vector3>();

        Vector3 center = new Vector3(PIECE_SIZE / 2, PIECE_SIZE / 2);
        vertices.Add(center);


        Vector3 bottomLeft = new Vector3(0, 0, 0);
        vertices.Add(bottomLeft);

        Vector3 leftMiddle = Vector3.zero;
        if (leftType == 0) {
            leftMiddle = new Vector3(0, PIECE_SIZE / 2, 0);
        } else if (leftType == 1) {
            leftMiddle = new Vector3(-PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        } else if (leftType == 2) {
            leftMiddle = new Vector3(PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        }
        vertices.Add(leftMiddle);

        Vector3 TopLeft = new Vector3(0, PIECE_SIZE, 0);
        vertices.Add(TopLeft);

        Vector3 TopMiddle = Vector3.zero;
        if (topType == 0) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE, 0);
        } else if (topType == 1) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE + PROTRUSION_DEPTH, 0);
        } else if (topType == 2) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE - PROTRUSION_DEPTH, 0);
        }
        vertices.Add(TopMiddle);

        Vector3 TopRight = new Vector3(PIECE_SIZE, PIECE_SIZE, 0);
        vertices.Add(TopRight);

        Vector3 rightMiddle = Vector3.zero;
        if (rightType == 0) {
            rightMiddle = new Vector3(PIECE_SIZE, PIECE_SIZE / 2, 0);
        } else if (rightType == 1) {
            rightMiddle = new Vector3(PIECE_SIZE + PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        } else if (rightType == 2) {
            rightMiddle = new Vector3(PIECE_SIZE - PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        }
        vertices.Add(rightMiddle);

        Vector3 rightBottom = new Vector3(PIECE_SIZE, 0, 0);
        vertices.Add(rightBottom);

        Vector3 bottomMiddle = Vector3.zero;
        if (bottomType == 0) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, 0, 0);
        } else if (bottomType == 1) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, -PROTRUSION_DEPTH, 0);
        } else if (bottomType == 2) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, +PROTRUSION_DEPTH, 0);
        }
        vertices.Add(bottomMiddle);


        return vertices;
    }

    private List<int> GetTriangles(int _verticesCount) {
        List<int> triangles = new List<int>();

        int verticesCount = _verticesCount - 1;
        for (int i = 0; i < verticesCount; i++){
            int p1 = i + 1;
            int p2 = (i + 1) % verticesCount + 1;

            triangles.Add(0);
            triangles.Add(p1);
            triangles.Add(p2);
        }

        return triangles;
    }
}
