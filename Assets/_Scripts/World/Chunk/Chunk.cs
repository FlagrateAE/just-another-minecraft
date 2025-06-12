using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public const int Width = 16;
    public const int Height = 64;

    public Vector2Int ChunkPosition;
    public BlockType[,,] Blocks;

    private World _world;
    private ChunkMeshGenerator _generator;

    private void Start()
    {
        _world = transform.parent.GetComponent<World>();
        _generator = GetComponent<ChunkMeshGenerator>();
    }

    public void LoadData(Vector2Int chunkPosition, BlockType[,,] blocks)
    {
        ChunkPosition = chunkPosition;
        Blocks = blocks;
    }

    public BlockType GetBlock(Vector3Int blockPosition)
    {
        return Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
    }

    public BlockType GetBlockChecked(Vector3Int blockPosition)
    {
        if (IsPositionInChunk(blockPosition))
        {
            return GetBlock(blockPosition);
        }
        else
        {
            if (blockPosition.y < 0 || blockPosition.y >= Height) return BlockType.Air;

            Vector2Int adjacentChunkPosition = ChunkPosition;

            if (blockPosition.x < 0)
            {
                adjacentChunkPosition.x--;
                blockPosition.x += Width;
            }
            else if (blockPosition.x >= Width)
            {
                adjacentChunkPosition.x++;
                blockPosition.x -= Width;
            }

            if (blockPosition.z < 0)
            {
                adjacentChunkPosition.y--;
                blockPosition.z += Width;
            }
            else if (blockPosition.z >= Width)
            {
                adjacentChunkPosition.y++;
                blockPosition.z -= Width;
            }

            if (_world.Chunks.TryGetValue(adjacentChunkPosition, out Chunk adjacentChunk))
            {
                return adjacentChunk.GetBlock(blockPosition);
            }
            else
            {
                return BlockType.Air;
            }

        }
    }

    private bool IsPositionInChunk(Vector3Int localPosition) =>
        localPosition.x >= 0 && localPosition.x < Width &&
        localPosition.y >= 0 && localPosition.y < Height &&
        localPosition.z >= 0 && localPosition.z < Width;

    public void SetBlock(Vector3Int blockPosition, BlockType blockType)
    {
        Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = blockType;
        _generator.Regenerate();
    }
}


