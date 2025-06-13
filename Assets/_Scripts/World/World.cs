using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class World : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;

    public readonly Dictionary<Vector2Int, Chunk> Chunks = new();

    [Inject] private BlockRegistry _blocksRegistry;

    private void Start()
    {
        int xGlobalPosition;
        int zGlobalPosition;

        BlockId[,,] terrain;
        Vector2Int chunkPosition;

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                xGlobalPosition = x * Chunk.Width;
                zGlobalPosition = y * Chunk.Width;

                chunkPosition = new(x, y);

                terrain = TerrainGenerator.GenerateTerrain(xGlobalPosition, zGlobalPosition);

                var chunk = Instantiate(
                    _chunkPrefab,
                    new(xGlobalPosition, 0, zGlobalPosition),
                    Quaternion.identity,
                    parent: transform
                )
                .GetComponent<Chunk>();

                chunk.Initialize(chunkPosition, terrain, _blocksRegistry);

                Chunks.Add(chunk.ChunkPosition, chunk);
            }
        }
    }

    public void PlaceBlock(Vector3Int position, BlockId block)
    {
        Vector2Int chunkPosition = GlobalToChunkPosition(position);

        Vector3Int localPosition = new(
            position.x - chunkPosition.x * Chunk.Width,
            position.y,
            position.z - chunkPosition.y * Chunk.Width
        );

        Chunks[chunkPosition].SetBlock(localPosition, block);
    }

    public void BreakBlock(Vector3Int position)
    {
        Vector2Int chunkPosition = GlobalToChunkPosition(position);

        Vector3Int localPosition = new(
            position.x - chunkPosition.x * Chunk.Width,
            position.y,
            position.z - chunkPosition.y * Chunk.Width
        );

        Chunks[chunkPosition].RemoveBlock(localPosition);
    }

    private Vector2Int GlobalToChunkPosition(Vector3Int position)
    {
        return new(position.x / Chunk.Width, position.z / Chunk.Width);
    }
}