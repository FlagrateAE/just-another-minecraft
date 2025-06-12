using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;

    public readonly Dictionary<Vector2Int, Chunk> Chunks = new();

    private void Start()
    {
        int xGlobalPosition;
        int zGlobalPosition;

        BlockType[,,] terrain;
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

                chunk.LoadData(chunkPosition, terrain);

                Chunks.Add(chunk.ChunkPosition, chunk);
            }
        }
    }

    public void SetBlock(Vector3Int position, BlockType blockType)
    {
        Chunk chunk = GetChunkFromGlobalPosition(position);
        chunk.SetBlock(position, blockType);
    }

    private Chunk GetChunkFromGlobalPosition(Vector3Int position)
    {
        return Chunks[
            new Vector2Int(
                position.x / Chunk.Width,
                position.z / Chunk.Width
            )
        ];
    }
}