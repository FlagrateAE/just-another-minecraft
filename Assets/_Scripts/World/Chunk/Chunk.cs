using UnityEngine;
using System.Linq;

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

    private static World _world;
    public ChunkMeshGenerator MeshGenerator { get; private set; }

    public void Initialize(
        Vector2Int chunkPosition,
        BlockId[,,] blocks,
        BlockRegistry blockRegistry
    )
    {
        ChunkPosition = chunkPosition;
        Blocks = blocks;

        if (_world == null) _world = transform.parent.GetComponent<World>();
        MeshGenerator = GetComponent<ChunkMeshGenerator>();

        MeshGenerator.LoadData(blockRegistry);
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

    public void PlaceBlock(Vector3Int position, BlockId block)
    {
        Blocks[position.x, position.y, position.z] = block;
        MeshGenerator.Regenerate();
    }

    public void BreakBlock(Vector3Int position)
    {
        Blocks[position.x, position.y, position.z] = BlockId.Air;

        if (position.x == 0 && _leftNeighbor != null)
            _leftNeighbor.MeshGenerator.Regenerate();
        if (position.x == Width - 1 && _rightNeighbor != null)
            _rightNeighbor.MeshGenerator.Regenerate();
        if (position.z == 0 && _backNeighbor != null)
            _backNeighbor.MeshGenerator.Regenerate();
        if (position.z == Width - 1 && _frontNeighbor != null)
            _frontNeighbor.MeshGenerator.Regenerate();

        MeshGenerator.Regenerate();
    }
}


