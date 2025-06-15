using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class World : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;

    public readonly Dictionary<Vector2Int, Chunk> Chunks = new();

    [Inject] public BlockRegistry BlockRegistry { get; private set; }

    private void Start()
    {
        // PASS 1: generation
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int xGlobal = x * Chunk.Width;
                int zGlobal = y * Chunk.Width;
                Vector2Int pos = new(x, y);

                var chunkGO = Instantiate(
                    _chunkPrefab,
                    new Vector3(xGlobal, 0, zGlobal),
                    Quaternion.identity,
                    parent: transform
                );

                BlockId[,,] terrain = TerrainGenerator.GenerateTerrain(xGlobal, zGlobal);

                var chunk = chunkGO.GetComponent<Chunk>();
                chunk.Initialize(
                    pos,
                    terrain,
                    BlockRegistry
                );

                Chunks.Add(pos, chunk);
            }
        }

        // PASS 2: neighbor
        foreach (var kvp in Chunks)
        {
            Vector2Int pos = kvp.Key;
            Chunk chunk = kvp.Value;

            Chunks.TryGetValue(pos + Vector2Int.up, out Chunk frontNeighbor);
            Chunks.TryGetValue(pos + Vector2Int.down, out Chunk backNeighbor);
            Chunks.TryGetValue(pos + Vector2Int.left, out Chunk leftNeighbor);
            Chunks.TryGetValue(pos + Vector2Int.right, out Chunk rightNeighbor);

            chunk.LoadNeighbors(
                frontNeighbor,
                backNeighbor,
                leftNeighbor,
                rightNeighbor
            );
        }
    }

    // public void TryPlaceBlock(Vector3Int position, BlockId block) { }

    // public void BreakBlock(Vector3Int position)
    // {
    //     SetBlock(position, BlockId.Air);
    // }

    // private void SetBlock(Vector3Int position, BlockId block)
    // {
    //     Chunk chunk = Chunks[GetChunkPosition(position)];
    //     chunk.SetBlock(position, block);
    // }

    // private Vector2Int GetChunkPosition(Vector3Int globalBlockPosition)
    // {
    //     return new Vector2Int(globalBlockPosition.x / Chunk.Width, globalBlockPosition.z / Chunk.Width);
    // }
}