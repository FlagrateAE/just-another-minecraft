using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    public const int ChunkWidth = 16;
    public const int ChunkHeight = 64;
    public BlockType[,,] Blocks = new BlockType[ChunkWidth, ChunkHeight, ChunkWidth];


    private Mesh _chunkMesh;
    private List<Vector3> _vertices = new();
    private List<int> _triangles = new();


    private void Start()
    {
        _chunkMesh = new Mesh();
        Blocks[0, 0, 0] = BlockType.Grass;

        for (int y = 0; y < ChunkHeight; y++)
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkWidth; z++)
                {
                    Vector3Int position = new(x, y, z);
                    BlockType block = GetBlockAtPosition(position);

                    GenerateBlock(position, block);
                }
            }
        }

        _chunkMesh.vertices = _vertices.ToArray();
        _chunkMesh.triangles = _triangles.ToArray();

        _chunkMesh.RecalculateNormals();
        _chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = _chunkMesh;
    }

    public BlockType GetBlockAtPosition(Vector3Int position)
    {
        if (position.x >= 0 && position.x < ChunkWidth &&
            position.y >= 0 && position.y < ChunkHeight &&
            position.z >= 0 && position.z < ChunkWidth)
        {
            return Blocks[position.x, position.y, position.z];
        }
        else
        {
            return BlockType.Air;
        }
    }


    private void GenerateBlock(Vector3Int position, BlockType blockType)
    {
        if (blockType == BlockType.Air) return;

        if (GetBlockAtPosition(position + Vector3Int.right) == 0) GenerateRightSide(position);
        if (GetBlockAtPosition(position + Vector3Int.left) == 0) GenerateLeftSide(position);
        if (GetBlockAtPosition(position + Vector3Int.forward) == 0) GenerateFrontSide(position);
        if (GetBlockAtPosition(position + Vector3Int.back) == 0) GenerateBackSide(position);
        if (GetBlockAtPosition(position + Vector3Int.up) == 0) GenerateTopSide(position);
        if (GetBlockAtPosition(position + Vector3Int.down) == 0) GenerateBottomSide(position);
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
        _triangles.Add(_vertices.Count - 4);
        _triangles.Add(_vertices.Count - 3);
        _triangles.Add(_vertices.Count - 2);

        _triangles.Add(_vertices.Count - 3);
        _triangles.Add(_vertices.Count - 1);
        _triangles.Add(_vertices.Count - 2);
    }

}
