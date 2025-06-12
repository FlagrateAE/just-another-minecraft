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

                Chunk chunk = new(
                    this,
                    new(x, y),
                    TerrainGenerator.GenerateTerrain(xGlobalPos, zGlobalPos)
                );

                Chunks.Add(chunk.Position, chunk);

                Instantiate(
                    _chunkPrefab,
                    new(xGlobalPos, 0, zGlobalPos),
                    Quaternion.identity,
                    parent: transform
                )
                .GetComponent<ChunkRenderer>()
                .SetChunk(chunk);
            }
        }
    }
}