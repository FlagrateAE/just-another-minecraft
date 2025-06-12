using UnityEngine;

public class Chunk
{
    public const int Width = 16;
    public const int Height = 64;

    public readonly Vector2Int Position;
    public readonly BlockType[,,] Blocks;

    private readonly World _world;

    public Chunk(World world, Vector2Int position, BlockType[,,] blocks)
    {
        _world = world;
        Position = position;
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

            Vector2Int adjacentChunkPosition = Position;

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
}


