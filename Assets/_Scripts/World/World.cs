using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;

    public readonly Dictionary<Vector2Int, Chunk> Chunks = new();

    private void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int xGlobalPos = x * Chunk.Width;
                int zGlobalPos = y * Chunk.Width;

                var renderer = Instantiate(
                    _chunkPrefab,
                    new(xGlobalPos, 0, zGlobalPos),
                    Quaternion.identity,
                    parent: transform
                ).GetComponent<ChunkRenderer>();

                Chunk chunk = new(
                    this,
                    new(x, y),
                    TerrainGenerator.GenerateTerrain(xGlobalPos, zGlobalPos),
                    renderer
                );

                renderer.LoadChunk(chunk);

                Chunks.Add(chunk.Position, chunk);
            }
        }
    }

    public void SetBlock(Vector3Int blockPosition, BlockType blockType)
    {
        Chunk chunk = GetChunkFromGlobalPosition(blockPosition);
        chunk.SetBlock(blockPosition, blockType);
    }

    private Chunk GetChunkFromGlobalPosition(Vector3Int blockPosition)
    {
        return Chunks[
            new Vector2Int(
                blockPosition.x / Chunk.Width,
                blockPosition.z / Chunk.Width
            )
        ];
    }
}