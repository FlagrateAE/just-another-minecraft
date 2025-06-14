using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public const int Width = 16;
    public const int Height = 64;

    public Vector2Int ChunkPosition;
    public BlockId[,,] Blocks;

    private Chunk _frontNeighbor;
    private Chunk _backNeighbor;
    private Chunk _leftNeighbor;
    private Chunk _rightNeighbor;

    private World _world;
    private ChunkMeshGenerator _generator;

    public void Initialize(
        Vector2Int chunkPosition,
        BlockId[,,] blocks,
        BlockRegistry blockRegistry
    )
    {
        ChunkPosition = chunkPosition;
        Blocks = blocks;

        _world = transform.parent.GetComponent<World>();
        _generator = GetComponent<ChunkMeshGenerator>();

        _generator.LoadData(blockRegistry);
    }

    public void LoadNeighbors(
        Chunk frontNeighbor,
        Chunk backNeighbor,
        Chunk leftNeighbor,
        Chunk rightNeighbor
    )
    {
        _frontNeighbor = frontNeighbor;
        _backNeighbor = backNeighbor;
        _leftNeighbor = leftNeighbor;
        _rightNeighbor = rightNeighbor;
    }

    public BlockId GetBlock(Vector3Int blockPosition)
    {
        return Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
    }

    public BlockId GetBlockChecked(Vector3Int blockPosition)
    {
        if (IsPositionInChunk(blockPosition))
        {
            return GetBlock(blockPosition);
        }
        else
        {
            if (blockPosition.y < 0 || blockPosition.y >= Height) return BlockId.Air;

            if (blockPosition.x < 0 && _leftNeighbor != null)
            {
                blockPosition.x += Width;
                return _leftNeighbor.GetBlock(blockPosition);
            }
            else if (blockPosition.x >= Width && _rightNeighbor != null)
            {
                blockPosition.x -= Width;
                return _rightNeighbor.GetBlock(blockPosition);
            }

            if (blockPosition.z < 0 && _backNeighbor != null)
            {
                blockPosition.z += Width;
                return _backNeighbor.GetBlock(blockPosition);
            }
            else if (blockPosition.z >= Width && _frontNeighbor != null)
            {
                blockPosition.z -= Width;
                return _frontNeighbor.GetBlock(blockPosition);
            }

            return BlockId.Air;

        }
    }

    private bool IsPositionInChunk(Vector3Int localPosition) =>
        localPosition.x >= 0 && localPosition.x < Width &&
        localPosition.y >= 0 && localPosition.y < Height &&
        localPosition.z >= 0 && localPosition.z < Width;


    public void SetBlock(Vector3Int position, BlockId block)
    {
        Blocks[position.x, position.y, position.z] = block;
        _generator.Regenerate();
    }

    public void RemoveBlock(Vector3Int position)
    {
        Blocks[position.x, position.y, position.z] = BlockId.Air;
        _generator.Regenerate();
    }
}


