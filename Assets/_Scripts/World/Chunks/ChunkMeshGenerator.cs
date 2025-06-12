using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Chunk))]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ChunkMeshGenerator : MonoBehaviour
{
    private Chunk _chunk;
    private Mesh _chunkMesh;
    private readonly List<Vector3> _vertices = new();
    private readonly List<Vector2> _uvs = new();
    private readonly List<int> _triangles = new();

    private void Start()
    {
        _chunk = GetComponent<Chunk>();

        Regenerate();
    }

    public void Regenerate()
    {
        _vertices.Clear();
        _uvs.Clear();
        _triangles.Clear();
        _chunkMesh = new Mesh();

        for (int y = 0; y < Chunk.Height; y++)
        {
            for (int x = 0; x < Chunk.Width; x++)
            {
                for (int z = 0; z < Chunk.Width; z++)
                {
                    Vector3Int position = new(x, y, z);
                    BlockId block = _chunk.GetBlock(position);

                    GenerateBlock(position, block);
                }
            }
        }

        _chunkMesh.vertices = _vertices.ToArray();
        _chunkMesh.uv = _uvs.ToArray();
        _chunkMesh.triangles = _triangles.ToArray();

        _chunkMesh.RecalculateNormals();
        _chunkMesh.RecalculateBounds();
        _chunkMesh.Optimize();

        GetComponent<MeshFilter>().mesh = _chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = _chunkMesh;
    }

    private void GenerateBlock(Vector3Int position, BlockId block)
    {
        if (block == BlockId.Air) return;

        if (_chunk.GetBlockChecked(position + Vector3Int.right) == 0) GenerateRightSide(position);
        if (_chunk.GetBlockChecked(position + Vector3Int.left) == 0) GenerateLeftSide(position);
        if (_chunk.GetBlockChecked(position + Vector3Int.forward) == 0) GenerateFrontSide(position);
        if (_chunk.GetBlockChecked(position + Vector3Int.back) == 0) GenerateBackSide(position);
        if (_chunk.GetBlockChecked(position + Vector3Int.up) == 0) GenerateTopSide(position);
        if (_chunk.GetBlockChecked(position + Vector3Int.down) == 0) GenerateBottomSide(position);
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastSquareVertices();
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);

        AddLastSquareVertices();
    }

    private void GenerateFrontSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastSquareVertices();
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);

        AddLastSquareVertices();
    }

    private void GenerateTopSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastSquareVertices();
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);

        AddLastSquareVertices();
    }

    private void AddLastSquareVertices()
    {
        _uvs.Add(new Vector2(0, 0));
        _uvs.Add(new Vector2(0, 1));
        _uvs.Add(new Vector2(1, 0));
        _uvs.Add(new Vector2(1, 1));


        _triangles.Add(_vertices.Count - 4);
        _triangles.Add(_vertices.Count - 3);
        _triangles.Add(_vertices.Count - 2);

        _triangles.Add(_vertices.Count - 3);
        _triangles.Add(_vertices.Count - 1);
        _triangles.Add(_vertices.Count - 2);
    }
}
