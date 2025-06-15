using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Chunk))]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ChunkMeshGenerator : MonoBehaviour
{
    private BlockRegistry _blockRegistry;

    private Chunk _chunk;
    private Mesh _chunkMesh;
    private readonly List<Vector3> _vertices = new();
    private readonly List<Vector2> _uvs = new();
    private readonly List<int> _triangles = new();

    public void LoadData(BlockRegistry blockRegistry) => _blockRegistry = blockRegistry;

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

        GetComponent<MeshFilter>().mesh = _chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = _chunkMesh;
    }

    private void GenerateBlock(Vector3Int position, BlockId block)
    {
        if (block == BlockId.Air) return;

        var textureData = _blockRegistry.GetInfo(block).TextureData;

        if (_chunk.GetBlockChecked(position + Vector3Int.right) == BlockId.Air)
            GenerateRightSide(position, textureData);
        if (_chunk.GetBlockChecked(position + Vector3Int.left) == BlockId.Air)
            GenerateLeftSide(position, textureData);
        if (_chunk.GetBlockChecked(position + Vector3Int.forward) == BlockId.Air)
            GenerateFrontSide(position, textureData);
        if (_chunk.GetBlockChecked(position + Vector3Int.back) == BlockId.Air)
            GenerateBackSide(position, textureData);
        if (_chunk.GetBlockChecked(position + Vector3Int.up) == BlockId.Air)
            GenerateTopSide(position, textureData);
        if (_chunk.GetBlockChecked(position + Vector3Int.down) == BlockId.Air)
            GenerateBottomSide(position, textureData);
    }

    private void GenerateFrontSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Front);

        _uvs.Add(new Vector2(uv11.x, uv00.y));
        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        _uvs.Add(new Vector2(uv00.x, uv11.y));
        AddTriangles();
    }

    private void GenerateBackSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Back);

        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv00.x, uv11.y));
        _uvs.Add(new Vector2(uv11.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        AddTriangles();
    }

    private void GenerateLeftSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Left);

        _uvs.Add(new Vector2(uv11.x, uv00.y));
        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        _uvs.Add(new Vector2(uv00.x, uv11.y));
        AddTriangles();
    }

    private void GenerateRightSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Right);

        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv00.x, uv11.y));
        _uvs.Add(new Vector2(uv11.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        AddTriangles();
    }

    private void GenerateTopSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Top);

        _uvs.Add(new Vector2(uv00.x, uv11.y));
        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        _uvs.Add(new Vector2(uv11.x, uv00.y)); 
        AddTriangles();
    }

    private void GenerateBottomSide(Vector3Int blockPosition, BlockTextureData textureData)
    {
        _vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        _vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        _vertices.Add(new Vector3(1, 0, 1) + blockPosition);

        var (uv00, uv11) = CalculateUVs(textureData.Bottom);

        _uvs.Add(new Vector2(uv11.x, uv00.y));
        _uvs.Add(new Vector2(uv00.x, uv00.y));
        _uvs.Add(new Vector2(uv11.x, uv11.y));
        _uvs.Add(new Vector2(uv00.x, uv11.y));
        AddTriangles();
    }

    private (Vector2, Vector2) CalculateUVs(Vector2Int texturePosition)
    {
        float ts = 1f / BlockTextureData.AtlasColumns;
        int maxRow = BlockTextureData.AtlasRows - 1;
        Vector2 uv00 = new(texturePosition.x * ts, (maxRow - texturePosition.y) * ts);
        Vector2 uv11 = uv00 + new Vector2(ts, ts);

        return (uv00, uv11);
    }

    private void AddTriangles()
    {
        int i = _vertices.Count;
        _triangles.Add(i - 4);
        _triangles.Add(i - 3);
        _triangles.Add(i - 2);
        _triangles.Add(i - 3);
        _triangles.Add(i - 1);
        _triangles.Add(i - 2);
    }
}
