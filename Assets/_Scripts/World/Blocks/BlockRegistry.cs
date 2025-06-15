using System.Collections.Generic;
using UnityEngine;

public class BlockRegistry
{
    private const string BlocksFolderPath = "ScriptableObjects/Blocks";

    private Dictionary<BlockId, Block> _blocks = new();

    public BlockRegistry()
    {
        Block[] blocks = Resources.LoadAll<Block>(BlocksFolderPath);

        foreach (Block block in blocks)
        {
            _blocks.Add(block.Id, block);
        }
    }

    public Block GetInfo(BlockId blockId) => _blocks[blockId];
}